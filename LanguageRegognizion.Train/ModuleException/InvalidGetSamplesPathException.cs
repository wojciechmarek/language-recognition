using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRegognizion.Train.ModuleException
{
    public class InvalidNumberOfCategoriesException : Exception
    {
        public InvalidNumberOfCategoriesException()
        { }

        public InvalidNumberOfCategoriesException(string message) : base(message)
        { }

        public InvalidNumberOfCategoriesException(string message, Exception exception) : base(message, exception)
        { }
    }
}
