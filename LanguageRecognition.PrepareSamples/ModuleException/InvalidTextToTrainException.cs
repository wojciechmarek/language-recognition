using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.PrepareSamples.ModuleException
{
    public class InvalidTextToTrainException : Exception
    {
        public InvalidTextToTrainException()
        { }

        public InvalidTextToTrainException(string message) : base(message)
        { }

        public InvalidTextToTrainException(string message, Exception exception) : base(message, exception)
        { }
    }
}
