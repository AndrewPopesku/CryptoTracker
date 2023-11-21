using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoTracker.Commands
{
    /// <summary>
    /// Represents a command used to open hyperlinks.
    /// </summary>
    public class HyperLinkCommand : CommandBase
    {
        /// <summary>
        /// Executes the command to open a hyperlink.
        /// </summary>
        /// <param name="parameter">The URL string representing the hyperlink.</param>
        public override void Execute(object? parameter)
        {
            if (parameter is string url)
            {
                try
                {
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening link: {ex.Message}");
                }
            }
        }
    }
}
