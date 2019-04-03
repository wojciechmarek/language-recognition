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
            InvokeWindow(prepareWindow);
        }

        public void ShowTrainWindow()
        {
            InvokeWindow(learnWindow);
        }

        public void ShowRecognizeWindow()
        {
            InvokeWindow(recognizeWindow);
        }

        /// <summary>
        /// Helper method to reduce redundance
        /// </summary>
        /// <param name="window">Window type</param>
        public void InvokeWindow(Window window)
        {
            bool isActuallyVisible = window.IsVisible;

            if (!isActuallyVisible)
            {
                window.Show();
            }
            else
            {
                window.Visibility = Visibility.Visible;
            }
        }

        #endregion
    }
}
