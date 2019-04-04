using LanguageRecognition.Prepare.Interface;
using LanguageRecognition.Prepare.Service;
using LanguageRecognition.PrepareSamples.ModuleException;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.Prepare.Tests.ServiceTest
{
    [TestFixture]
    public class PrepareServiceTest
    {
        [Test]
        public void Service_WillObjectThrowExceptionWithNullPath_Exception()
        {
            IPrepareService prepareService = new PrepareService();

            Assert.Throws<InvalidPathException>(()=>
            {
                prepareService.SetPathToSaveSample(null);
            });
        }

        [Test]
        public void Service_WillObjectThrowExceptionWithEmptyPath_Exception()
        {
            IPrepareService prepareService = new PrepareService();

            Assert.Throws<InvalidPathException>(() =>
            {
                prepareService.SetPathToSaveSample(string.Empty);
            });
        }

        [Test]
        public void Service_WillObjectThrowExceptionWithNullText_Exception()
        {
            IPrepareService prepareService = new PrepareService();

            Assert.Throws<InvalidTextToTrainException>(() =>
            {
                prepareService.SetSampleTextToPrepareSample(null);
            });
        }

        [Test]
        public void Service_WillObjectThrowExceptionWithEmptyText_Exception()
        {
            IPrepareService prepareService = new PrepareService();

            Assert.Throws<InvalidTextToTrainException>(() =>
            {
                prepareService.SetSampleTextToPrepareSample(new StringBuilder(""));
            });
        }

        [Test]
        public void Service_WillObjectThrowExceptionWithNullLanguageLabel_Exception()
        {
            IPrepareService prepareService = new PrepareService();

            Assert.Throws<InvalidLanguageLabelException>(() =>
            {
                prepareService.SetLanguageLabel(null);
            });
        }

        [Test]
        public void Service_WillObjectThrowExceptionWithEmptyLanguageLabel_Exception()
        {
            IPrepareService prepareService = new PrepareService();

            Assert.Throws<InvalidLanguageLabelException>(() =>
            {
                prepareService.SetLanguageLabel(string.Empty);
            });
        }

        [Test]
        public void Servce_WillObjectThrowAnyExceptionAtFirstCreateSampleInvoke_AnyException()
        {
            IPrepareService prepareService = new PrepareService();

            Assert.That(() => prepareService.CreateSample(), Throws.Exception);
        }
    }
}
