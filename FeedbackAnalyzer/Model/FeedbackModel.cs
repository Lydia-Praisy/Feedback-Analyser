namespace FeedbackAnalyzer.Model
{
    public class FeedbackModel
    {
        public string DateTime { get; set; }

        public string ExperienceInParking { get; set; }

        public string ExperienceInDining { get; set; }

        public string ExperienceWithWaiter { get; set; }

        public string HowWasFood { get; set; }

        public string HowIsPriceRange { get; set; }

        public static FeedbackModel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            FeedbackModel dailyValues = new FeedbackModel();
            
            dailyValues.DateTime = values[0];
            dailyValues.ExperienceInParking = values[1];
            dailyValues.ExperienceInDining = values[2];
            dailyValues.ExperienceWithWaiter = values[3];
            dailyValues.HowWasFood = values[4];
            dailyValues.HowIsPriceRange = values[5];

            return dailyValues;
        }
    }
}
