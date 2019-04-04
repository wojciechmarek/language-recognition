using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRegognizion.Train.ModuleException
{
    public class InvalidSaveAnnPathException : Exception
    {
        public InvalidSaveAnnPathException()
        { }

        public InvalidSaveAnnPathException(string message) : base(message)
        { }

        public InvalidSaveAnnPathException(string message, Exception exception) : base(message, exception)
        { }
    }
}
