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

        #region Methods

        /// <summary>
        /// Method connected to button command.
        /// Method responsible for create & take path from load file window.
        /// </summary>
        /// <param name="obj">Not used</param>
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

            //with every load new path we update path in services
            if (result == true)
            {
                LoadAnnPath = dialog.FileName;
                UpdatePathWithAnn();
            }
        }

        /// <summary>
        /// Update path to trained ANN model in services
        /// </summary>
        private void UpdatePathWithAnn()
        {
            try
            {
                _recognizeService.SetPathOfTrainedAnn(LoadAnnPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load ANN issue", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }

        /// <summary>
        /// Every change in field to recognize inkoves method to recognize in services.
        /// </summary>
        /// <param name="textToRecognition">Valid text from TextBox</param>
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

        #endregion
    }
}
