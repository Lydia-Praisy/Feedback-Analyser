namespace Prediction.Model
{
    public class StudentPerformanceFullModel
    {
        public int Year { get; set; }

        public int Semester { get; set; }

        public string SubjectCode { get; set; }

        public int TotalStudents { get; set; }

        public float StudentsAppearedForExam { get; set; }

        public float NumberOfIats { get; set; }

        public float NumberOfClasses { get; set; }

        public float AttendancePercentage { get; set; }

        public float PassPercentage { get; set; }

        public bool IsPredictedResult { get; set; } = false;

        public static StudentPerformanceFullModel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            StudentPerformanceFullModel dailyValues = new StudentPerformanceFullModel();
            dailyValues.Year = Convert.ToInt32(values[0]);
            dailyValues.Semester = Convert.ToInt32(values[1]);
            dailyValues.SubjectCode = values[2];
            dailyValues.TotalStudents = Convert.ToInt32(values[3]);
            dailyValues.StudentsAppearedForExam = Convert.ToInt32(values[4]);
            dailyValues.NumberOfIats = Convert.ToInt32(values[5]);
            dailyValues.NumberOfClasses = Convert.ToInt32(values[6]);
            dailyValues.AttendancePercentage = Convert.ToInt32(values[7]);
            dailyValues.PassPercentage = Convert.ToInt32(values[8]);

            return dailyValues;
        }
    }
}
