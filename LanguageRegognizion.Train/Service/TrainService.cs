using LanguageRegognizion.Train.Interface;
using LanguageRegognizion.Train.ModuleException;
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

        
        #endregion

        #region Methods

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

        #region Train

        public string TrainNetwork()
        {
            CheckGetSamplePath(pathToGetSamples);
            CheckSaveAnnPath(pathToSaveAnn);

            ParseDataFromSampleFile();
            CreateObservationsMatrix();
            CreateTargetsVector();

            CreateNeuralNetwork();
            LearnAnn();

            SaveAnn();

            return "";
        }

        private void ParseDataFromSampleFile()
        {
            AddHeaderToSamples();

            try
            {
                parsedExamples = new CsvParser(() => new StreamReader(pathToGetSamples), separator: ';', hasHeader: true);
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException(ex.Message);
            }
        }
    
        private void AddHeaderToSamples()
        {
            string str;

            using (StreamReader sreader = new StreamReader(pathToGetSamples))
            {
                str = sreader.ReadToEnd();
            }

            File.Delete(pathToGetSamples);

            using (StreamWriter swriter = new StreamWriter(pathToGetSamples, false))
            {
                str = "a;b;c;d;e;f;g;h;i;j;k;l;m;n;o;p;q;r;s;t;u;v;w;x;y;z;language" + Environment.NewLine + str;
                swriter.Write(str);
            }
        }

        private void CreateObservationsMatrix()
        {
            try
            {
                var featureNames = parsedExamples.EnumerateRows(c => c != dependentVariableColumn).First().ColumnNameToIndex.Keys.ToArray();

                var observations = parsedExamples.EnumerateRows(featureNames).ToF64Matrix();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void CreateTargetsVector()
        {
            try
            {
                dependentVariableAsName = parsedExamples.EnumerateRows(dependentVariableColumn).ToStringVector();
                dependentVariableAsNumber = parsedExamples.EnumerateRows(dependentVariableColumn).ToF64Vector()
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void CountDependentVariables()
        {
            var outputCategories = dependentVariableAsName.Distinct().Count();
        }

        private void CreateNeuralNetwork()
        {
            neuralNetwork = new NeuralNet();

            neuralNetwork.Add(new InputLayer(26));
            neuralNetwork.Add(new DropoutLayer(0.2));

            neuralNetwork.Add(new DenseLayer(10));
            neuralNetwork.Add(new DropoutLayer(0.2));

            neuralNetwork.Add(new SoftMaxLayer(outputCategories));
        }

        private void LearnAnn()
        {
            var learner = new ClassificationNeuralNetLearner(neuralNetwork, iterations: 300, loss: new AccuracyLoss(), batchSize: 10);
            annModel = learner.Learn(observations, dependentVariableAsNumber);
        }

        private void SaveAnn()
        {
            annModel.Save(() => new StreamWriter(pathToSaveAnn));
        }
        
        #endregion

        #region CheckInputVariables

        private void CheckGetSamplePath(string pathToGet)
        {
            if (string.IsNullOrEmpty(pathToGet))
            {
                throw new InvalidGetSamplesPathException("Languages sample path is empty or null");
            }
        }

        private void CheckSaveAnnPath(string pathToSave)
        {
            if (string.IsNullOrEmpty(pathToSave))
            {
                throw new InvalidSaveAnnPathException("Save ANN path is empty or null");
            }
        }

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
