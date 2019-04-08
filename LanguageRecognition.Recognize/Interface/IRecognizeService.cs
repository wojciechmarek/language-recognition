using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.Recognize.Interface
{
    public interface IRecognizeService
    {
        void SetPathOfTrainedAnn(string pathToGet);
        string Recognize(string textToRecognize);
    }
}
