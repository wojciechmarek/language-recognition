using LanguageRecognition.Recognize.Interface;
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
    public class RecognizeWindowViewModel : RaisePropertyChanged
    {
        #region ButtonCommand

        public ICommand LoadAnnButtonPress { get; set; }

        #endregion

        #region BindingProperties

        private string loadAnnPath;
        public string LoadAnnPath
        {
            get { return loadAnnPath; }
            set
            {
                SetValue(ref loadAnnPath, value);
            }
        }

        private string resultLanguage;
        public string ResultLanguage
        {
            get { return resultLanguage; }
            set
            {
                SetValue(ref resultLanguage, value);
            }
        }

        private string textToRecognize;
        public string TextToRecognize
        {
            get
            {
                return textToRecognize;
            }
            set
            {
                textToRecognize = value;
                InvokeRecognition(value);
            }
        }


        #endregion

        #region Constructor

        IRecognizeService _recognizeService;

        public RecognizeWindowViewModel(IRecognizeService recognizeService)
        {
            _recognizeService = recognizeService;

            LoadAnnButtonPress = new RelayCommand(LoadAnn);

            LoadAnnPath = Path.Combine(Environment.CurrentDirectory, "trained_ann.xml");
        }

        #endregion

        private void LoadAnn(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML(*.xml)|*.xml";
            dialog.InitialDirectory = LoadAnnPath;
            dialog.AddExtension = true;
            dialog.Multiselect = false;
            dialog.Title = "Load language samples from";
            dialog.FileName = "trained_ann.xml";

            var result = dialog.ShowDialog();

            if (result == true)
            {
                LoadAnnPath = dialog.FileName;
                UpdatePathWithAnn();
            }
        }

        private void UpdatePathWithAnn()
        {
            try
            {
                _recognizeService.GetPathOfTrainedAnn(LoadAnnPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load ANN issue", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }

        private void InvokeRecognition(string textToRecognition)
        {
            try
            {
                ResultLanguage = _recognizeService.Recognize(textToRecognition);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Recognition issue", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }

    }
}
