using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    public interface INavigationService
    {
        Task Navigate(object? parameter);
        void NavigateToErrorView(string errorMessage, object? parameter);
    }
}
