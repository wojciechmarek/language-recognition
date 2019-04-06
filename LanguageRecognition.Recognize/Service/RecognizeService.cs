using LanguageRecognition.Recognize.Interface;
using LanguageRecognition.Recognize.ModuleException;
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
        double predictedLanguage;
        #endregion

        #region Methods

        public void GetPathOfTrainedAnn(string pathToGet)
        {
            CheckGetTrainedAnnPath(pathToGet);

            pathToGetTrainedAnn = pathToGet;

            LoadAnn();
        }

        public string Recognize(string textToRecognize)
        {
            this.textToRecognize = textToRecognize;

            CountLetterInText();

            ConvertLetterToPercentage();

            RecognizeByModel();

            return predictedLanguage.ToString();
        }

        private void LoadAnn()
        {
            if (!isAnnLoaded)
            {
                try
                {
                    annModel = ClassificationNeuralNetModel.Load(() => new StreamReader(pathToGetTrainedAnn));
                    isAnnLoaded = true;
                }
                catch (Exception ex)
                {
                    isAnnLoaded = false;
                    throw new Exception(ex.Message);
                }
            } 
        }

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

        private void ConvertLetterToPercentage()
        {
            countedEachLetterInPercentage = new double[26];

            for (int i = 0; i < countedEachLetter.Length; i++)
            {
                double percent = countedEachLetter[i] / (double)totalLettersNumberInText;
                countedEachLetterInPercentage[i] = Math.Round(percent, 3);
            }
        }

        private void RecognizeByModel()
        {
            predictedLanguage = annModel.Predict(countedEachLetterInPercentage);
        }
        #endregion

        #region CheckInputVariables

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
