using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    /// <summary>
    /// Defines methods for navigation within the CryptoTracker application.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigates to a specific view or page asynchronously.
        /// </summary>
        /// <param name="parameter">The parameter to be passed during navigation.</param>
        /// <returns>A task representing the asynchronous navigation operation.</returns>
        Task Navigate(object? parameter);

        /// <summary>
        /// Navigates to an error view with a specified error message and optional parameter.
        /// </summary>
        /// <param name="errorMessage">The error message to be displayed in the error view.</param>
        /// <param name="parameter">The optional parameter to be passed to the error view.</param>
        void NavigateToErrorView(string errorMessage, object? parameter);
    }
}
