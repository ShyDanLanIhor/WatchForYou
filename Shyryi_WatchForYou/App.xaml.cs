using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou.Stores;
using Shyryi_WatchForYou.ViewModels;
using Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel;
using Shyryi_WatchForYou.Views;
using Shyryi_WatchForYou.Views.UserControlViews.EnterView;

namespace Shyryi_WatchForYou
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateSignInViewModel();

            MainWindow = new EnterView()
            {
                DataContext = new EnterViewModel(_navigationStore)
            };
            MainWindow.Show();

            MainWindow.IsVisibleChanged += (s, ev) =>
            {
                if (MainWindow.Visibility != Visibility.Visible && MainWindow.IsLoaded)
                {
                    MainWindow = new MainView()
                    {
                        DataContext = new MainViewModel()
                    };
                    MainWindow.Show();
                }
            };

            base.OnStartup(e);
        }

        private SignUpViewModel CreateSignUpViewModel()
        { return new SignUpViewModel(new NavigationService(_navigationStore, CreateSignInViewModel)); }

        private SignInViewModel CreateSignInViewModel()
        { return new SignInViewModel(new NavigationService(_navigationStore, CreateSignUpViewModel)); }
    }
}