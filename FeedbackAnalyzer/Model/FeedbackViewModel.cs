using PaperCorrection.Model;

namespace FeedbackAnalyzer.Model
{
    public class Pair
    {
        public Pair(Intents intent, string value)
        {
            Intent = intent;
            Value = value;
        }

        public Intents Intent { get; set; }

        public string Value { get; set; }
    }

    public class FeedbackViewModel
    {
        public string DateTime { get; set; }

        public Pair ExperienceInParking { get; set; }

        public Pair ExperienceInDining { get; set; }

        public Pair ExperienceWithWaiter { get; set; }

        public Pair HowWasFood { get; set; }

        public Pair HowIsPriceRange { get; set; }
    }
}
