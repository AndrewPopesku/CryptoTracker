using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Commands
{
    /// <summary>
    /// Represents a base class for asynchronous commands that can be executed asynchronously.
    /// </summary>
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool _isExecuting;

        /// <summary>
        /// Gets or sets a value indicating whether the command is currently executing.
        /// </summary>
        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                OnCanExecutedChanged();
            }
        }

        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <param name="parameter">The parameter to be passed to the command.</param>
        /// <returns>True if the command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        /// <summary>
        /// Executes the command asynchronously.
        /// </summary>
        /// <param name="parameter">The parameter to be passed to the command.</param>
        public override async void Execute(object? parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            finally
            {
                IsExecuting = false;
            }
        }

        /// <summary>
        /// Executes the command asynchronously.
        /// </summary>
        /// <param name="parameter">The parameter to be passed to the command.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public abstract Task ExecuteAsync(object? parameter);
    }
}
