using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Exceptions
{
    /// <summary>
    /// Represents an exception thrown when there is an issue with fetching data.
    /// </summary>
    public class FetchDataException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FetchDataException"/> class.
        /// </summary>
        public FetchDataException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FetchDataException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public FetchDataException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FetchDataException"/> class with a specified error message and a reference to the inner exception that caused this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public FetchDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
