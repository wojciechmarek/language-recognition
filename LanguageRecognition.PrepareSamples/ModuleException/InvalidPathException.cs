using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.PrepareSamples.ModuleException
{
    public class InvalidPathException : Exception
    {
        public InvalidPathException()
        { }

        public InvalidPathException(string message) : base(message)
        { }

        public InvalidPathException(string message, Exception exception) : base(message, exception)
        { }
    }
}
