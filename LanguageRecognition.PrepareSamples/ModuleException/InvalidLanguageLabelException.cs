using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.PrepareSamples.ModuleException
{
    public class InvalidLanguageLabelException : Exception
    {
        public InvalidLanguageLabelException()
        { }

        public InvalidLanguageLabelException(string message) : base(message)
        { }

        public InvalidLanguageLabelException(string message, Exception exception) : base(message, exception)
        { }
    }
}
