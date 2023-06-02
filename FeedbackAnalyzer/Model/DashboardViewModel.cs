using FeedbackAnalyzer.Model;

namespace Prediction.Model
{
    public class DashboardViewModel
    {
        public double PositiveFeedbackCount { get; set; }

        public double NegativeFeedbackCount { get; set; }

        public double AverageFeedbackCount { get; set; }

        public double PositiveFeedbackPercent { get; set; }

        public double NegativeFeedbackPercent { get; set; }

        public double AverageFeedbackPercent { get; set; }

        public IEnumerable<string> FeedbackCategories { get; set; }

        public IEnumerable<double> FeedbackCategoriesResult { get; set; }

        public List<FeedbackViewModel> Feedbacks { get; set; }

        public List<StudentPerformanceFullModel> StudentPerformanceDetails { get; set; }

        public List<string> TableHeaders { get; set; }

        public List<string> NewTableHeaders { get; set; }

        public IEnumerable<string> Semesters { get; set; }

        public IEnumerable<float> SemesterResults { get; set; }
    }
}
