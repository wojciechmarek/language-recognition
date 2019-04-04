using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRegognizion.Train.ModuleException
{
    public class InvalidGetSamplesPathException : Exception
    {
        public InvalidGetSamplesPathException()
        { }

        public InvalidGetSamplesPathException(string message) : base(message)
        { }

        public InvalidGetSamplesPathException(string message, Exception exception) : base(message, exception)
        { }
    }
}
