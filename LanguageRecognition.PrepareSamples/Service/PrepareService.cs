using LanguageRecognition.Prepare.Interface;
using LanguageRecognition.PrepareSamples.ModuleException;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.Prepare.Service
{
    public class PrepareService : IPrepareService
    {
        #region Fields

        //fields set by public methods
        private string pathToSaveLangSamples;
        private string languageLabel;
        private StringBuilder textForPrepareSample;

        //private fields
        private int[] countedEachLetter;
        private double[] countedEachLetterInPercentage;
        private int totalLettersNumberInText;

        #endregion

        #region CreateSample

        public void CreateSample()
        {
            countedEachLetter = new int[26];
            countedEachLetterInPercentage = new double[26];
            totalLettersNumberInText = 0;

            CheckLanguageLabel(languageLabel);
            CheckSampleText(textForPrepareSample);
            CheckPathToSave(pathToSaveLangSamples);

            CountLettersInText();
            ConvertLetterToPercentage();
            Save();
        }

        #endregion

        #region SettersMethods

        public void SetLanguageLabel(string languageLabel)
        {
            CheckLanguageLabel(languageLabel);

            this.languageLabel = languageLabel;
        }

        public void SetPathToSaveSample(string pathToSave)
        {
            CheckPathToSave(pathToSave);

            this.pathToSaveLangSamples = pathToSave;
        }

        public void SetSampleTextToPrepareSample(StringBuilder sampleText)
        {
            CheckSampleText(sampleText);

            this.textForPrepareSample = sampleText;
        }

        #endregion

        #region Methods

        private void CountLettersInText()
        {
            string text = textForPrepareSample.ToString().ToLower();

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
            for (int i = 0; i < countedEachLetter.Length; i++)
            {
                double percent = countedEachLetter[i] / (double)totalLettersNumberInText;
                countedEachLetterInPercentage[i] = Math.Round(percent, 3);
            }
        }

        private void Save()
        {
            using (StreamWriter fileStream = File.AppendText(pathToSaveLangSamples))
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < countedEachLetterInPercentage.Length; i++)
                {
                    sb.Append(countedEachLetterInPercentage[i].ToString(CultureInfo.GetCultureInfo("en-US")));
                    sb.Append(";");
                }

                sb.Append(languageLabel);

                fileStream.WriteLineAsync(sb.ToString());
            }
        }

        #endregion

        #region CheckInputVariables

        private void CheckLanguageLabel(string languageLabel)
        {
            if (string.IsNullOrEmpty(languageLabel))
            {
                throw new InvalidLanguageLabelException("Language label is empty or null");
            }
        }

        private void CheckPathToSave(string pathToSave)
        {
            if (string.IsNullOrEmpty(pathToSave))
            {
                throw new InvalidPathException("Save path is empty or null");
            }
        }

        private void CheckSampleText(StringBuilder sampleText)
        {
            if (sampleText == null)
            {
                throw new InvalidTextToTrainException("Sample text is null");
            }
            if (sampleText.Length == 0)
            {
                throw new InvalidTextToTrainException("Sample text is empty");
            }
        }
        #endregion
    }
}
