using FeedbackAnalyzer;
using FeedbackAnalyzer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaperCorrection.Model;
using Prediction.Model;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Prediction.Pages
{
    public class IndexModel : PageModel
    {
        public DashboardViewModel DataSource { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        private readonly IHostingEnvironment _environment;

        static readonly string _customerFeedbackFilePath = Path.Combine(Environment.CurrentDirectory, "FeedbackFile", "CustomerFeedback.csv");

        double positiveFeedbackCount = 0;
        double negativeFeedbackCount = 0;
        double averageFeedbackCount = 0;

        public IndexModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<IActionResult> OnGet()
        {
            var tableHeaderTask = await System.IO.File.ReadAllLinesAsync(_customerFeedbackFilePath);
            string tableHeader = tableHeaderTask.FirstOrDefault();
            List<string> tableHeaders = tableHeader != null ? tableHeader.Split(',').ToList() : new List<string>();

            List<FeedbackModel> values = System.IO.File.ReadAllLines(_customerFeedbackFilePath)
                                                           .Skip(1)
                                                           .Select(v => FeedbackModel.FromCsv(v))
                                                           .ToList();

            FeedbackTrainer feedbackTrainer = new FeedbackTrainer();
            List<FeedbackViewModel> feedbacks = new List<FeedbackViewModel>();

            foreach (var row in values)
            {
                var feedbackViewModel = new FeedbackViewModel();
                feedbackViewModel.DateTime = row.DateTime;

                var intent = feedbackTrainer.Predict(row.ExperienceInParking);
                UpdateFeedbackCount(intent);
                feedbackViewModel.ExperienceInParking = new Pair(intent, row.ExperienceInParking);

                intent = feedbackTrainer.Predict(row.ExperienceInDining);
                UpdateFeedbackCount(intent);
                feedbackViewModel.ExperienceInDining = new Pair(intent, row.ExperienceInDining);

                intent = feedbackTrainer.Predict(row.ExperienceWithWaiter);
                UpdateFeedbackCount(intent);
                feedbackViewModel.ExperienceWithWaiter = new Pair(intent, row.ExperienceWithWaiter);

                intent = feedbackTrainer.Predict(row.HowWasFood);
                UpdateFeedbackCount(intent);
                feedbackViewModel.HowWasFood = new Pair(intent, row.HowWasFood);

                intent = feedbackTrainer.Predict(row.HowIsPriceRange);
                UpdateFeedbackCount(intent);
                feedbackViewModel.HowIsPriceRange = new Pair(intent, row.HowIsPriceRange);

                feedbacks.Add(feedbackViewModel);
            }

            var model = new StudentPerformanceViewModel
            {
                AttendanceRatio = 12,
                NumberOfClassesRatio = 30,
                NumberOfIatsRatio = 23,
                Semester = 3,
                StudentsAppearedForExamRatio = 40,
                SubjectCode = "ABC123",
                TotalStudents = 120,
                Year = 2023,
            };

            var trainer = new Trainer();
            double totalComments = (values.Count * 5);
            DataSource = await trainer.GetDashboardSourceAsync(model);
            DataSource.PositiveFeedbackCount = positiveFeedbackCount;
            DataSource.NegativeFeedbackCount = negativeFeedbackCount;
            DataSource.AverageFeedbackCount = averageFeedbackCount;
            DataSource.PositiveFeedbackPercent = (positiveFeedbackCount / totalComments) * 100;
            DataSource.NegativeFeedbackPercent = (negativeFeedbackCount / totalComments) * 100;
            DataSource.AverageFeedbackPercent = (averageFeedbackCount / totalComments) * 100;
            DataSource.NewTableHeaders = tableHeaders;
            DataSource.FeedbackCategories = new List<string> { "Positive", "Negative", "Average" };
            DataSource.FeedbackCategoriesResult = new List<double> 
            {
                DataSource.PositiveFeedbackPercent,
                DataSource.NegativeFeedbackPercent,
                DataSource.AverageFeedbackPercent
            };
            DataSource.Feedbacks = feedbacks;

            return this.Page();
        }

        private void UpdateFeedbackCount(Intents intent)
        {
            if (intent == Intents.Positive)
            {
                positiveFeedbackCount++;
            }
            else if (intent == Intents.Negative)
            {
                negativeFeedbackCount++;
            }
            else
            { 
                averageFeedbackCount++;
            }
        }

        public async Task OnPostAsync()
        {
            var file = Path.Combine(_environment.ContentRootPath, "uploads", Upload.FileName);

            //var package = new ExcelPackage(new FileInfo()"sample.xlsx"));


            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
        }
    }
}