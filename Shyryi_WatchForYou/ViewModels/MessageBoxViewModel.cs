using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.ViewModels.AriaListViewModels;
using Shyryi_WatchForYou.Views;
using Shyryi_WatchForYou.Views.AriaListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shyryi_WatchForYou.ViewModels
{
    public class MessageBoxViewModel : ViewModelBase
    {

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public MessageBoxViewModel(string message)
        {
            Message = message;
        }
        public static void Show(string message)
        {
            // Використовуємо Dispatcher, щоб показати MessageBox в STA-потоці
            Application.Current.Dispatcher.Invoke(() =>
            {
                var MessageBoxViewModel = new MessageBoxViewModel(message);
                var MessageBoxView = new MessageBoxView
                {
                    DataContext = MessageBoxViewModel
                };

                MessageBoxView.ShowDialog();
            });
        }

    }
}
