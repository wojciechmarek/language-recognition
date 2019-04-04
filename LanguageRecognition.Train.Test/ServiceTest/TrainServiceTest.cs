using LanguageRegognizion.Train.Interface;
using LanguageRegognizion.Train.ModuleException;
using LanguageRegognizion.Train.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.Train.Test.ServiceTest
{
    [TestFixture]
    public class TrainServiceTest
    {
        [Test]
        public void Service_WillObjectThrowExceptionWithNullPathForSample_Exception()
        {
            ITrainService trainService = new TrainService();

            Assert.Throws<InvalidGetSamplesPathException>(() =>
            {
                trainService.GetPathOfLanguageSample(null);
            });
        }

        [Test]
        public void Service_WillObjectThrowExceptionWithEmptyPathForSample_Exception()
        {
            ITrainService trainService = new TrainService();

            Assert.Throws<InvalidGetSamplesPathException>(() =>
            {
                trainService.GetPathOfLanguageSample(string.Empty);
            });
        }

        [Test]
        public void Service_WillObjectThrowExceptionWithNullPathForAnn_Exception()
        {
            ITrainService trainService = new TrainService();

            Assert.Throws<InvalidSaveAnnPathException>(() =>
            {
                trainService.SetPathToSaveAnn(null);
            });
        }

        [Test]
        public void Service_WillObjectThrowExceptionWithEmptyPathForAnn_Exception()
        {
            ITrainService trainService = new TrainService();

            Assert.Throws<InvalidSaveAnnPathException>(() =>
            {
                trainService.SetPathToSaveAnn(string.Empty);
            });
        }
    }
}
