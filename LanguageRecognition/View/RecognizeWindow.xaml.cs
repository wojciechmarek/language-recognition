using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LanguageRecognition.View
{
    /// <summary>
    /// Interaction logic for RecognizeWindow.xaml
    /// </summary>
    public partial class RecognizeWindow : Window
    {
        public RecognizeWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Used to capture and cancel window close
        /// </summary>
        /// <remarks>
        /// When we close window, window's instance can't invokes again .Show() because we closed window
        /// of particular instance. Every try will throw exception.
        /// So we must change visibility of window to hidden and to call again our window, we have to
        /// check that window exists (is hidden) and then we can change it to visible.
        /// The change is visible beetwen first and next window calls. First call is longer and animated.
        /// </remarks>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            //base.OnClosing(e);
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
