using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.Service
{
    /// <summary>
    /// Interface for injecting to ViewModel
    /// This interface is representing only Services for MainWindow
    /// Rest interfaces(for PreparePrepare, Recognize, Trein) are in libraries
    /// </summary>
    public interface IServices
    {
        void ShowPrepareWindow();
        void ShowTrainWindow();
        void ShowRecognizeWindow();
    }
}
