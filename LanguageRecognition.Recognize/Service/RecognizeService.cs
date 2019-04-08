using LanguageRecognition.Recognize.Interface;
using LanguageRecognition.Recognize.ModuleException;
using Newtonsoft.Json;
using SharpLearning.Neural.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.Recognize.Service
{
    public class RecognizeService : IRecognizeService
    {
        #region Fields

        //fields set by public methods
        private string pathToGetTrainedAnn;
        private string textToRecognize;

        //private class fields
        private bool isAnnLoaded = false;
        ClassificationNeuralNetModel annModel;
        int[] countedEachLetter;
        int totalLettersNumberInText;
        double[] countedEachLetterInPercentage;
        string predictedLanguage;
        string[] languagesLabels;
        string allAnnModel;

        #endregion

        #region Recognize

        /// <summary>
        /// Method which is invoked in MVVM. This method contains all workflow to recognize text.
        /// </summary>
        /// <param name="textToRecognize">Text to recognize language</param>
        /// <returns>Recognized language</returns>
        public string Recognize(string textToRecognize)
        {
            this.textToRecognize = textToRecognize;

            CountLetterInText();

            ConvertLetterToPercentage();

            RecognizeByModel();

            if (string.IsNullOrWhiteSpace(textToRecognize))
            {
                return "";
            }
            return predictedLanguage;
        }

        #endregion

        #region SetterMethod

        public void SetPathOfTrainedAnn(string pathToGet)
        {
            CheckGetTrainedAnnPath(pathToGet);

            pathToGetTrainedAnn = pathToGet;

            GetDataFromImportedFile();
            LoadAnn();
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Methods reads all lines of trained network file, saves it to private field and gets&remove line with languages labes.
        /// </summary>
        private void GetDataFromImportedFile()
        {
            try
            {
                var allLines = new List<string>(System.IO.File.ReadAllLines(pathToGetTrainedAnn));
                var readLabels = allLines.Last();
                var numOfLines = allLines.Count;
                allLines.RemoveAt(numOfLines-1);

                allAnnModel = string.Join("", allLines);               

                languagesLabels = JsonConvert.DeserializeObject<string[]>(readLabels);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method reads trained model from pravate field and load it to ClassificationNeuralNetModel object.
        /// </summary>
        private void LoadAnn()
        {
            if (!isAnnLoaded)
            {
                try
                {
                    annModel = ClassificationNeuralNetModel.Load(() => new StringReader(allAnnModel));
                    isAnnLoaded = true;
                }
                catch (Exception ex)
                {
                    isAnnLoaded = false;
                    throw new Exception(ex.Message);
                }
            } 
        }

        /// <summary>
        /// Same methods as in PrepareService. Count letters in input text.
        /// </summary>
        private void CountLetterInText()
        {
            countedEachLetter = new int[26];
            totalLettersNumberInText = 0;
            string text = textToRecognize.ToString().ToLower();

            if (text.Length > 0)
            {
                foreach (var letter in text)
                {
                    if (letter >= 'a' && letter <= 'z')
                    {
                        countedEachLetter[letter - 'a']++;
                        totalLettersNumberInText++;
                    }
                }
            }
        }

        /// <summary>
        /// Same methods as in PrepareService. Converts letters to percentages.
        /// </summary>
        private void ConvertLetterToPercentage()
        {
            countedEachLetterInPercentage = new double[26];

            for (int i = 0; i < countedEachLetter.Length; i++)
            {
                double percent = countedEachLetter[i] / (double)totalLettersNumberInText;
                countedEachLetterInPercentage[i] = Math.Round(percent, 3);
            }
        }

        /// <summary>
        /// Methods puts array with letters in percentages to .Predict() method.
        /// </summary>
        /// <remarks>
        /// On output we it returns double number of language.
        /// So we must retrieve label value from languagesLabels. Labels we added on the end .xml file.
        /// </remarks>
        private void RecognizeByModel()
        {
            var predictAsNumber = (int)annModel.Predict(countedEachLetterInPercentage);
            predictedLanguage = languagesLabels[predictAsNumber];
        }

        #endregion

        #region CheckInputVariables

        /// <summary>
        /// Additioanal method to check content of trainedAnn path, throws: InvalidTrainedAnnPathException.
        /// </summary>
        /// <param name="trainedAnn">Valid content</param>
        private void CheckGetTrainedAnnPath(string trainedAnn)
        {
            if (string.IsNullOrEmpty(trainedAnn))
            {
                throw new InvalidTrainedAnnPathException("Trained ANN Model is empty or null");
            }
        }

        #endregion
    }
}
