using Microsoft.AspNetCore.Mvc;

namespace Prediction.Model
{
    public class StudentPerformanceViewModel
    {
        [BindProperty]
        public int Year { get; set; }

        [BindProperty]
        public int Semester { get; set; }

        [BindProperty]
        public string SubjectCode { get; set; }

        [BindProperty]
        public int TotalStudents { get; set; }

        [BindProperty]
        public float StudentsAppearedForExamRatio { get; set; }

        [BindProperty]
        public float NumberOfIatsRatio { get; set; }

        [BindProperty]
        public float NumberOfClassesRatio { get; set; }

        [BindProperty]
        public float AttendanceRatio { get; set; }
    }
}
