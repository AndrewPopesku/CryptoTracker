using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Commands
{
    /// <summary>
    /// Represents a command used for initiating search functionality.
    /// </summary>
    public class SearchCommand : AsyncCommandBase
    {
        private readonly Func<Task> _execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand"/> class with the specified asynchronous search operation.
        /// </summary>
        /// <param name="execute">The asynchronous function representing the search operation.</param>
        public SearchCommand(Func<Task> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Executes the search operation asynchronously.
        /// </summary>
        /// <param name="parameter">Optional parameter for the search operation.</param>
        public override async Task ExecuteAsync(object? parameter)
        {
            await _execute?.Invoke();
        }
    }
}
