using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.Prepare.Interface
{
    public interface IPrepareService
    {
        void SetPathToSaveSample(string pathToSave);
        void SetSampleTextToPrepareSample(StringBuilder sampleText);
        void SetLanguageLabel(string languageLabel);
        void CreateSample();
    }
}
