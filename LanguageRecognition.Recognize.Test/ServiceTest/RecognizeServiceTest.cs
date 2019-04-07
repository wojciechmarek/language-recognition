using LanguageRecognition.Recognize.Interface;
using LanguageRecognition.Recognize.ModuleException;
using LanguageRecognition.Recognize.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.Recognize.Test.ServiceTest
{
    [TestFixture]
    class RecognizeServiceTest
    {
        [Test]
        public void Service_WillObjectThrowExceptionWithEmptyPathWithModel_Exception()
        {
            IRecognizeService recognizeService = new RecognizeService();

            Assert.Throws<InvalidTrainedAnnPathException>(() =>
            {
                recognizeService.GetPathOfTrainedAnn(string.Empty);
            });
        }

        [Test]
        public void Service_WillObjectThrowExceptionWithNullPathWithModel_Exception()
        {
            IRecognizeService recognizeService = new RecognizeService();

            Assert.Throws<InvalidTrainedAnnPathException>(() =>
            {
                recognizeService.GetPathOfTrainedAnn(null);
            });
        }
    }
}
