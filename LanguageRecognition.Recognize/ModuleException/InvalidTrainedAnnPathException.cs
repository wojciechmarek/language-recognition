using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition.Recognize.ModuleException
{
    public class InvalidTrainedAnnPathException : Exception
    {
        public InvalidTrainedAnnPathException()
        { }

        public InvalidTrainedAnnPathException(string message) : base(message)
        { }

        public InvalidTrainedAnnPathException(string message, Exception exception) : base(message, exception)
        { }
    }
}
