using Shyryi_WatchForYou.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.Services
{
    public static class ExceptionService
    {
        public static T HandleDataBaseException<T>(Exception ex) where T : new()
        {
            if (ex is TypeInitializationException)
            {
                MessageBoxViewModel.Show("Cannot connect to the server!");
                return new();
            } else if (ex is IOException)
            {
                MessageBoxViewModel.Show("Cannot connect to the server!" +
                    "\nProgram will be closed." +
                    "\nSorry for the inconvenience.");
                App.Current.Dispatcher.Invoke(() =>
                { App.Current.Shutdown(); });
                return new();
            }
            else
            {
                MessageBoxViewModel.Show(ex.Message);
                return default(T);
            }
        }
    }
}
