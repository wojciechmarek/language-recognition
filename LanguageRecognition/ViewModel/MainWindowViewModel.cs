using LanguageRecognition.Service;
using LanguageRecognition.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LanguageRecognition.ViewModel
{
    public class MainWindowViewModel
    {
        #region ButtonCommands

        public ICommand ButtonPress { get; set; }

        #endregion

        #region Constructor

        private IServices _service;

        public MainWindowViewModel(IServices services)
        {
            _service = services;

            ButtonPress = new RelayCommand(ShowWindow);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method connected to button command. It invokes appropriate window.
        /// </summary>
        /// <param name="obj">Value from CommandParameter in XAML</param>
        private void ShowWindow(object obj)
        {
            switch (obj.ToString())
            {
                case "prepare":
                    _service.ShowPrepareWindow();
                    break;

                case "train":
                    _service.ShowTrainWindow();
                    break;

                case "recognize":
                    _service.ShowRecognizeWindow();
                    break;

                default:
                    break;
            }
        }

        #endregion
    }
}
