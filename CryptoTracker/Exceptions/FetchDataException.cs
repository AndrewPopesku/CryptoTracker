using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Exceptions
{
    public class FetchDataException : Exception
    {
        public FetchDataException() { }

        public FetchDataException(string message) : base(message) { }

        public FetchDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
