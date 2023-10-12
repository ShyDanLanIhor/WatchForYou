using Shyryi_WatchForYou.Views;
using System.Windows;

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
