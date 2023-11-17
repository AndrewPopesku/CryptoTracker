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
        private readonly Func<bool> _canExecute;

        public SearchCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public override async Task ExecuteAsync(object? parameter)
        {
            await _execute?.Invoke();
        }
    }
}
