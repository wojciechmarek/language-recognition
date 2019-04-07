using LanguageRecognition.View.ViewModel;
using LanguageRegognizion.Train.Interface;
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
    public class LearnWindowViewModel : RaisePropertyChanged
    {
        #region ButtonCommands

        public ICommand PathToGetSamplesButtonPress { get; set; }
        public ICommand PathToSaveAnnButtonPress { get; set; }
        public ICommand TrainButtonPress { get; set; }

        #endregion

        #region BindingProperties

        private string loadSamples;
        public string LoadSamples
        {
            get { return loadSamples; }
            set
            {
                SetValue(ref loadSamples, value);
            }
        }

        private string saveAnn;
        public string SaveAnn
        {
            get { return saveAnn; }
            set
            {
                SetValue(ref saveAnn, value);
            }
        }

        #endregion

        #region Constructor

        ITrainService _trainService;

        public LearnWindowViewModel(ITrainService trainService)
        {
            _trainService = trainService;

            PathToGetSamplesButtonPress = new RelayCommand(GetSamplePath);
            PathToSaveAnnButtonPress = new RelayCommand(GetAnnSavePath);
            TrainButtonPress = new RelayCommand(LearnNetwork);

            LoadSamples = Path.Combine(Environment.CurrentDirectory, "languages_set.csv");
            SaveAnn = Path.Combine(Environment.CurrentDirectory, "trained_ann.xml");
        }

        #endregion

        #region Methods

        private void LearnNetwork(object obj)
        {
            bool correctGetPath = CheckGetSamplePath(LoadSamples);
            bool correntSavePath = CheckSaveAnnPath(SaveAnn);

            if (correctGetPath && correntSavePath)
            {
                _trainService.GetPathOfLanguageSample(loadSamples);
                _trainService.SetPathToSaveAnn(saveAnn);

                try
                {
                    Task.Run(() => _trainService.TrainNetwork());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Train ANN issue", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void GetAnnSavePath(object obj)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML(*.xml)|*.xml";
            dialog.InitialDirectory = SaveAnn;
            dialog.AddExtension = true;
            dialog.Title = "Save trainer ANN to";
            dialog.FileName = "trained_ann.xml";

            var result = dialog.ShowDialog();

            if (result == true)
            {
                SaveAnn = dialog.FileName;
            }
        }

        private void GetSamplePath(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV(*.csv)|*.csv";
            dialog.InitialDirectory = LoadSamples;
            dialog.AddExtension = true;
            dialog.Multiselect = false;
            dialog.Title = "Load language samples from";
            dialog.FileName = "languages_set.csv";

            var result = dialog.ShowDialog();

            if (result == true)
            {
                LoadSamples = dialog.FileName;
            }
        }

        private bool CheckSaveAnnPath(string savePath)
        {
            if (string.IsNullOrEmpty(savePath))
            {
                MessageBox.Show("Save path can not be empty", "Save Path", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }

        private bool CheckGetSamplePath(string savePath)
        {
            if (string.IsNullOrEmpty(savePath))
            {
                MessageBox.Show("Get sample path can not be empty", "Get Path", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }

        #endregion
    }
}
