using LanguageRegognizion.Train.Interface;
using LanguageRegognizion.Train.ModuleException;
using Newtonsoft.Json;
using SharpLearning.Containers.Matrices;
using SharpLearning.InputOutput.Csv;
using SharpLearning.Neural;
using SharpLearning.Neural.Layers;
using SharpLearning.Neural.Learners;
using SharpLearning.Neural.Loss;
using SharpLearning.Neural.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRegognizion.Train.Service
{
    public class TrainService : ITrainService
    {
        #region Fields

        //field set by public methods
        private string pathToGetSamples;
        private string pathToSaveAnn;

        //private object fields
        private CsvParser parsedExamples;
        private NeuralNet neuralNetwork;
        ClassificationNeuralNetModel annModel;
        F64Matrix observations;
        string[] dependentVariableAsName;
        double[] dependentVariableAsNumber;
        string dependentVariableColumn = "language";
        int outputCategories;
        string[] uniqueValues;


        #endregion

        #region Train

        /// <summary>
        /// Method which is invoked in MVVM. This method contains all workflow to train network.
        /// </summary>
        public void TrainNetwork()
        {
            CheckGetSamplePath(pathToGetSamples);
            CheckSaveAnnPath(pathToSaveAnn);

            ParseDataFromSampleFile();
            CreateObservationsMatrix();
            CreateTargetsVector();
            ConvertStringVectorToDoubleVector();
            CountDependentVariables();
            CreateNeuralNetwork();
            LearnAnn();

            SaveAnn();
            AddLabelsToExportedAnnModel();
        }

        #endregion

        #region SetterMethods

        public void GetPathOfLanguageSample(string pathToGet)
        {
            CheckGetSamplePath(pathToGet);

            pathToGetSamples = pathToGet;
        }

        public void SetPathToSaveAnn(string pathToSave)
        {
            CheckSaveAnnPath(pathToSave);

            pathToSaveAnn = pathToSave;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method reads all languages sample from file.
        /// </summary>
        private void ParseDataFromSampleFile()
        {
            try
            {
                parsedExamples = new CsvParser(() => new StreamReader(pathToGetSamples), separator: ';', hasHeader: true);
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException(ex.Message);
            }
        }

        /// <summary>
        /// Method converts independent variables (all a-z, without language) to matrix of observations.
        /// </summary>
        private void CreateObservationsMatrix()
        {
            try
            {
                var featureNames = parsedExamples.EnumerateRows(c => c != dependentVariableColumn).First().ColumnNameToIndex.Keys.ToArray();

                observations = parsedExamples.EnumerateRows(featureNames).ToF64Matrix();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Method converts dependent variables (language) to vector of string.
        /// </summary>
        private void CreateTargetsVector()
        {
            try
            {
                dependentVariableAsName = parsedExamples.EnumerateRows(dependentVariableColumn).ToStringVector();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method convert language labels to unique numbers
        /// </summary>
        /// <remarks>
        /// Learn Method in ANN can not parse strings to output neurals, so we must send numbers to it.
        /// </remarks>
        private void ConvertStringVectorToDoubleVector()
        {
            uniqueValues = dependentVariableAsName.Distinct().ToArray();
            var numberOfDependentItems = dependentVariableAsName.Count();
            var dependentAsNumber = new double[numberOfDependentItems];
            int helper = 0;

            for (int i = 0; i < numberOfDependentItems; i++)
            {
                for (int k = 0; k < uniqueValues.Length; k++)
                {
                    if (string.Equals(dependentVariableAsName[i], uniqueValues[k]))
                    {
                        helper = k;
                    }
                }

                dependentAsNumber[i] = helper;
            }

            dependentVariableAsNumber = dependentAsNumber;
        }

        /// <summary>
        /// Method count how many dirrent languages we have in samples. It removes duclicates.
        /// </summary>
        private void CountDependentVariables()
        {
            outputCategories = dependentVariableAsName.Distinct().Count();
        }

        /// <summary>
        /// Method creates neural network.
        /// </summary>
        /// <remarks>
        /// Neural network has 26 input neurals, 10 neurals in hidden layer, and various number of output neurals.
        /// Dropout is a regularization technique patented by Google for reducing overfitting in neural networks by preventing complex co-adaptations on training data.
        /// Default activation function in neurals in ReLU.
        /// </remarks>
        private void CreateNeuralNetwork()
        {
            neuralNetwork = new NeuralNet();

            neuralNetwork.Add(new InputLayer(26));
            neuralNetwork.Add(new DropoutLayer(0.2));

            neuralNetwork.Add(new DenseLayer(10));
            neuralNetwork.Add(new DropoutLayer(0.2));

            neuralNetwork.Add(new SoftMaxLayer(outputCategories));
        }

        /// <summary>
        /// Method trains created earlier network. Learner requires minimum 5 samples of languages(=batchSize)[more=better]
        /// </summary>
        private void LearnAnn()
        {
            var learner = new ClassificationNeuralNetLearner(neuralNetwork, iterations: 300, loss: new AccuracyLoss(), batchSize: 5);
            annModel = learner.Learn(observations, dependentVariableAsNumber);
        }

        /// <summary>
        /// Method saves trained model to file.
        /// </summary>
        private void SaveAnn()
        {
            annModel.Save(() => new StreamWriter(pathToSaveAnn));
        }

        /// <summary>
        /// Additional functionality, it append on the end of trained model file text version of unique labels.
        /// Model can return only number on output, so we have not any information about names of languages in recognition module.
        /// </summary>
        private void AddLabelsToExportedAnnModel()
        {
            using (StreamWriter swriter = File.AppendText(pathToSaveAnn))
            {
                var header = JsonConvert.SerializeObject(uniqueValues);
                swriter.WriteLine();
                swriter.Write(header);
            }
        }

        #endregion

        #region CheckInputVariables

        /// <summary>
        /// Additioanal method to check content of pathToGet, throws: InvalidGetSamplesPathException.
        /// </summary>
        /// <param name="pathToGet">Valid content</param>
        private void CheckGetSamplePath(string pathToGet)
        {
            if (string.IsNullOrEmpty(pathToGet))
            {
                throw new InvalidGetSamplesPathException("Languages sample path is empty or null");
            }
        }

        /// <summary>
        /// Additioanal method to check content of pathToSave, throws: InvalidSaveAnnPathException.
        /// </summary>
        /// <param name="pathToSave">Valid content</param>
        private void CheckSaveAnnPath(string pathToSave)
        {
            if (string.IsNullOrEmpty(pathToSave))
            {
                throw new InvalidSaveAnnPathException("Save ANN path is empty or null");
            }
        }

        /// <summary>
        /// Additioanal method to check number of categories, throws: InvalidNumberOfCategoriesException.
        /// </summary>
        /// <param name="categories">Valid amount</param>
        private void CheckNumberOfCategories(int categories)
        {
            if (categories <= 1)
            {
                throw new InvalidNumberOfCategoriesException($"Number of categories: {categories} is too small");
            }
        }

        #endregion
    }
}
