using Microsoft.ML.Data;

namespace Prediction.Model
{
    public class PassPercentPrediction
    {
        [ColumnName("Score")]
        public float PassPercent;
    }
}
