using Microsoft.ML.Data;

namespace Prediction.Model
{
    public class StudentPerformance
    {
        [LoadColumn(4)]
        public float StudentsAppearedForExamRatio;

        [LoadColumn(5)]
        public float NumberOfIatsRatio;

        [LoadColumn(6)]
        public float NumberOfClassesRatio;

        [LoadColumn(7)]
        public float AttendanceRatio;

        [LoadColumn(8)]
        public float PassPercentage;
    }
}
