using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Commands
{
    public class SearchCommand : AsyncCommandBase
    {
        private readonly Func<Task> _execute;

        public SearchCommand(Func<Task> execute)
        {
            _execute = execute;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            await _execute?.Invoke();
        }
    }
}
