using FeedbackAnalyzer.Helpers;
using Microsoft.ML;
using Microsoft.ML.TorchSharp;
using Microsoft.ML.TorchSharp.NasBert;
using PaperCorrection.Model;

namespace FeedbackAnalyzer
{
    public class FeedbackTrainer
    {
        private readonly ITransformer _transformer;
        private MLContext _context;
        private PredictionEngine<ModelInput, ModelOutput> _engine;

        public FeedbackTrainer()
        {
            var detail = GetTransformer();

            _transformer = detail.transformer;
            _context = detail.context;
            _engine = detail.engine;
        }

        public Intents Predict(string feedback)
        {
            // Generate a prediction engine
            //PredictionEngine<ModelInput, ModelOutput> engine = _context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_transformer);

            ModelInput sampleData = new(feedback);
            Intents result = PredictionHelper.Predict(feedback);

            return result;
        }

        private (ITransformer transformer, MLContext context, PredictionEngine<ModelInput, ModelOutput> engine) GetTransformer()
        {
            // Initialize MLContext
            _context = new()
            {
                GpuDeviceId = 0,
                FallbackToCpu = true
            };

            // Load your data
            IDataView dataView = _context.Data.LoadFromTextFile<ModelInput>(
            "TrainingData/feedbackTrainingData.tsv",
            separatorChar: '\t',
            hasHeader: false);

            DataOperationsCatalog.TrainTestData dataSplit = _context.Data.TrainTestSplit(dataView, testFraction: 0.2, seed: 1234);
            IDataView trainData = dataSplit.TrainSet;
            IDataView testData = dataSplit.TestSet;

            //Define your training pipeline
            var pipeline = _context.Transforms.Conversion.MapValueToKey(
                            outputColumnName: "Label",
                            inputColumnName: "Label")
                        .Append(_context.MulticlassClassification.Trainers.TextClassification(
                            labelColumnName: "Label",
                            sentence1ColumnName: "Sentence",
                            architecture: BertArchitecture.Roberta))
                        .Append(_context.Transforms.Conversion.MapKeyToValue(
                            outputColumnName: "PredictedLabel",
                            inputColumnName: "PredictedLabel"));

            // Train the model
            ITransformer model = pipeline.Fit(trainData);

            _engine = _context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

            return (model, _context, _engine);
        }
    }
}
