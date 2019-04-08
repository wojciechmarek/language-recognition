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

        /// <summary>
        /// Method which is invoked in MVVM. This method contains all workflow to create language sample
        /// </summary>
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

        /// <summary>
        /// Method changes text to onlt small letters, next 
        /// method counts number of letters a-z in sample text.
        /// It also counts total number of a-z letters in text.
        /// Spaces and other chars are not includes in result.
        /// </summary>
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

        /// <summary>
        /// Method divides number of particular letter by total number of letters in text.
        /// Next it rounds to 3 number of fractional digits.
        /// </summary>
        private void ConvertLetterToPercentage()
        {
            for (int i = 0; i < countedEachLetter.Length; i++)
            {
                double percent = countedEachLetter[i] / (double)totalLettersNumberInText;
                countedEachLetterInPercentage[i] = Math.Round(percent, 3);
            }
        }

        /// <summary>
        /// Method saves samples to file.
        /// It file not exist it first adds header and sample values.
        /// If file exist it append only sample values.
        /// </summary>
        private void Save()
        {
            if (!File.Exists(pathToSaveLangSamples))
            {
                AddHeaderToSamples();
                AddSampleToSamples();
            }
            else
            {
                AddSampleToSamples();
            }
           
        }

        /// <summary>
        /// Method creates and append header to output file.
        /// </summary>
        private void AddHeaderToSamples()
        {
            using (StreamWriter swriter = File.AppendText(pathToSaveLangSamples))
            {
                var header = "a;b;c;d;e;f;g;h;i;j;k;l;m;n;o;p;q;r;s;t;u;v;w;x;y;z;language";
                swriter.WriteLineAsync(header);
            }
        }

        /// <summary>
        /// Methods append samples to output file.
        /// </summary>
        /// <remarks>
        /// It uses en-US culture info to force dot(.) as decimal separator. It also works with invariant culture.
        /// </remarks>
        private void AddSampleToSamples()
        {
            using (StreamWriter swriter = File.AppendText(pathToSaveLangSamples))
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < countedEachLetterInPercentage.Length; i++)
                {
                    sb.Append(countedEachLetterInPercentage[i].ToString(CultureInfo.GetCultureInfo("en-US")));
                    sb.Append(";");
                }

                sb.Append(languageLabel);

                swriter.WriteLineAsync(sb.ToString());
            }
        }

        #endregion

        #region CheckInputVariables

        /// <summary>
        /// Additioanal method to check content of languageLabel, throws: InvalidLanguageLabelException.
        /// </summary>
        /// <param name="languageLabel">Valid content</param>
        private void CheckLanguageLabel(string languageLabel)
        {
            if (string.IsNullOrEmpty(languageLabel))
            {
                throw new InvalidLanguageLabelException("Language label is empty or null");
            }
        }

        /// <summary>
        /// Additioanal method to check content of pathToSave, throws: InvalidPathException.
        /// </summary>
        /// <param name="pathToSave">Valid content</param>
        private void CheckPathToSave(string pathToSave)
        {
            if (string.IsNullOrEmpty(pathToSave))
            {
                throw new InvalidPathException("Save path is empty or null");
            }
        }

        /// <summary>
        /// Additioanal method to check content of sampleText, throws: InvalidTextToTrainException.
        /// </summary>
        /// <param name="sampleText">Valid content</param>
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
