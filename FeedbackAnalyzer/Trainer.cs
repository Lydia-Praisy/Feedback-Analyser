using Microsoft.ML;
using Newtonsoft.Json;
using Prediction.Model;

namespace Prediction
{
    public class Trainer
    {
        static readonly string _trainDataPath = Path.Combine(Environment.CurrentDirectory, "TrainingData", "student-mark-data.csv");
        
        public float Predict(StudentPerformance studentPerformance)
        {
            MLContext mlContext = new MLContext(seed: 0);
            var model = SetStudentMarkPipeline(mlContext);

            return PredictPassPercent(mlContext, model, studentPerformance);
        }

        public async Task<DashboardViewModel> GetDashboardSourceAsync(StudentPerformanceViewModel model)
        {
            var tableHeaderTask = await File.ReadAllLinesAsync(_trainDataPath);
            string tableHeader = tableHeaderTask.FirstOrDefault();
            var tableHeaders = tableHeader != null ? tableHeader.Split(',').ToList() : new List<string>();

            List<StudentPerformanceFullModel> values = File.ReadAllLines(_trainDataPath)
                                                           .Skip(1)
                                                           .Select(v => StudentPerformanceFullModel.FromCsv(v))
                                                           .ToList();
            var currentPerformance = new StudentPerformance()
            {
                AttendanceRatio = model.AttendanceRatio,
                NumberOfClassesRatio = model.NumberOfClassesRatio,
                NumberOfIatsRatio = model.NumberOfIatsRatio,
                StudentsAppearedForExamRatio = model.StudentsAppearedForExamRatio,
            };
            var predictedResult = Predict(currentPerformance);

            values.Add(new StudentPerformanceFullModel
            {
                Year = model.Year,
                Semester = model.Semester,
                SubjectCode = model.SubjectCode,
                TotalStudents = model.TotalStudents,
                StudentsAppearedForExam = model.StudentsAppearedForExamRatio,
                NumberOfIats = model.NumberOfIatsRatio,
                NumberOfClasses = model.NumberOfClassesRatio,
                AttendancePercentage = model.AttendanceRatio,
                PassPercentage = predictedResult,
                IsPredictedResult = true
            });

            return new DashboardViewModel
            { 
                StudentPerformanceDetails = values,
                TableHeaders = tableHeaders,
                Semesters = values.Select(x=>$"{x.Year}-{x.Semester}"),
                SemesterResults = values.Select(x => x.PassPercentage)
            };
        }

        public static ITransformer SetStudentMarkPipeline(MLContext mlContext)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<StudentPerformance>(_trainDataPath, hasHeader: true, separatorChar: ',');

            var pipeline = mlContext.Transforms.Concatenate("Features", new[] {
                                                            "AttendanceRatio", 
                                                            "NumberOfClassesRatio", 
                                                            "NumberOfIatsRatio", 
                                                            "StudentsAppearedForExamRatio" })
                .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "PassPercentage", maximumNumberOfIterations: 100));

            var model = pipeline.Fit(dataView);

            return model;
        }

        private static float PredictPassPercent(MLContext mlContext, ITransformer model, StudentPerformance studentPerformance)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<StudentPerformance, PassPercentPrediction>(model);
            var prediction = predictionFunction.Predict(studentPerformance);

            return prediction.PassPercent;
        }
    }
}
