using PaperCorrection.Model;

namespace FeedbackAnalyzer.Helpers
{
    public static class PredictionHelper
    {
        public static Intents Predict(string feedback)
        {
            List<string> avg = new List<string> { "not bad", "not so bad", "not so good", "average", "ok" };
            List<string> positive = new List<string> { "great", "better", "best", "nice", "good" };
            List<string> negative = new List<string> { "worst", "not good", "bad", "not", "no" };

            if (avg.Any(x => feedback.Trim().ToLower().Contains(x)))
            {
                return Intents.Average;
            }
            else if (positive.Any(x => feedback.Trim().ToLower().Contains(x)))
            {
                return Intents.Positive;
            }

            return Intents.Negative;
        }
    }
}
