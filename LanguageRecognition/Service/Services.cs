using Castle.MicroKernel.Registration;
using Castle.Windsor;
using LanguageRecognition.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LanguageRecognition.Service
{
    /// <summary>
    /// Implementation of interface
    /// This class is something like: Repository Pattern
    /// </summary>
    public class Services : IServices
    {
        #region Properties

        //This fields are needed to invoke .Show() for each window
        private readonly PrepareWindow prepareWindow;
        private readonly LearnWindow learnWindow;
        private readonly RecognizeWindow recognizeWindow;

        #endregion

        #region Constructor

        public Services(PrepareWindow prepareWindow, LearnWindow learnWindow, RecognizeWindow recognizeWindow)
        {
            this.prepareWindow = prepareWindow;
            this.learnWindow = learnWindow;
            this.recognizeWindow = recognizeWindow;
        }

        #endregion

        #region Methods

        public void ShowPrepareWindow()
        {
            prepareWindow.Show();
        }

        public void ShowTrainWindow()
        {
            learnWindow.Show();
        }

        public void ShowRecognizeWindow()
        {
            recognizeWindow.Show();
        }

        #endregion
    }
}
