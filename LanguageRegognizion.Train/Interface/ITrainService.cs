using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRegognizion.Train.Interface
{
    public interface ITrainService
    {
        void GetPathOfLanguageSample(string pathToGet);
        void SetPathToSaveAnn(string pathToSave);
        string TrainNetwork();
    }
}
