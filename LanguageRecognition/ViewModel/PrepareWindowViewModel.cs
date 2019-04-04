using LanguageRecognition.Prepare.Interface;
using LanguageRecognition.View.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LanguageRecognition.ViewModel
{
    public class PrepareWindowViewModel : RaisePropertyChanged
    {
        #region ButtonCommands

        public ICommand PathToSaveSampleButtonPress { get; set; }
        public ICommand CreateSampleButtonPress { get; set; }

        #endregion

        #region BindingProperties

        private string languageSampleInput;
        public string LanguageSampleInput
        {
            get { return languageSampleInput; }
            set { languageSampleInput = value; }
        }

        private string languageSampleLabel;
        public string LanguageSampleLabel
        {
            get { return languageSampleLabel; }
            set { languageSampleLabel = value; }
        }

        private string savePath;
        public string SavePath
        {
            get { return savePath; }
            set
            {
                SetValue(ref savePath, value);
            }
        }

        #endregion

        #region Constructor

        IPrepareService _prepareService;

        public PrepareWindowViewModel(IPrepareService prepareService)
        {
            _prepareService = prepareService;

            PathToSaveSampleButtonPress = new RelayCommand(ChangeSavePath);
            CreateSampleButtonPress = new RelayCommand(CreateSample);

            SavePath = Path.Combine(Environment.CurrentDirectory, "languages_set.csv");
        }

        #endregion

        #region Methods

        private void CreateSample(object obj)
        {
            bool correctLabel = CheckLanguageLabel(LanguageSampleLabel);
            bool correctSample = CheckLanguageSample(LanguageSampleInput);
            bool correntPath = CheckSavePath(SavePath);

            if (correctLabel && correctSample && correntPath)
            {
                try
                {
                    _prepareService.SetLanguageLabel(LanguageSampleLabel);
                    _prepareService.SetSampleTextToPrepareSample(new StringBuilder(LanguageSampleInput));
                    _prepareService.SetPathToSaveSample(savePath);

                    _prepareService.CreateSample();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Prepare Errpr", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }


            }
        }

        private void ChangeSavePath(object obj)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV(*.csv)|*.csv";
            dialog.InitialDirectory = SavePath;
            dialog.AddExtension = true;
            dialog.Title = "Save language samples to";
            dialog.FileName = "languages_set.csv";

            var result = dialog.ShowDialog();

            if (result == true)
            {
                SavePath = dialog.FileName;
            }
        }

        private bool CheckLanguageLabel(string languageLabel)
        {
            if (string.IsNullOrEmpty(languageSampleLabel))
            {
                MessageBox.Show("Language label can not be empty", "Language Label", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }

        private bool CheckLanguageSample(string sampleText)
        {
            if (string.IsNullOrEmpty(languageSampleLabel))
            {
                MessageBox.Show("Input text can not be empty", "Input Text", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }

        private bool CheckSavePath(string savePath)
        {
            if (string.IsNullOrEmpty(savePath))
            {
                MessageBox.Show("Save path can not be empty", "Save Parh", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }
        #endregion
    }
}
