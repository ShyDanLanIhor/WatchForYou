using Shyryi_WatchForYou.Exceptions;
using Shyryi_WatchForYou.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Shyryi_WatchForYou.Services
{
    public static class ExceptionService
    {
        public static T HandleDataBaseException<T>(Exception ex) where T : new()
        {
            if (ex is TypeInitializationException ||
                ex is IOException)
            {
                MessageBoxViewModel.Show(TcpClientService.Connect());
                return new();
            }
            else
            {
                MessageBoxViewModel.Show(ex.Message);
                return default(T);
            }
        }
        public static (Brush Brush, string Text) HandleGUIException(Exception ex)
        {
            if (ex is TypeInitializationException ||
                ex is IOException)
            {
                return ex is InputDataException
                    ? ((Brush)App.Current.FindResource("ErrorMessageColor"), TcpClientService.Connect())
                    : ((Brush)App.Current.FindResource("ErrorMessageColor"), TcpClientService.Connect());
            }
            else
            {
                return ex is InputDataException
                    ? ((Brush)App.Current.FindResource("ErrorMessageColor"), ex.Message)
                    : ((Brush)App.Current.FindResource("ErrorMessageColor"), ex.Message);
            }
        }

    }
}
