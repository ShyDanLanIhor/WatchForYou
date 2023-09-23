using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Shyryi_WatchForYou.Models.Repositories;
using Shyryi_WatchForYou.Repositories.IRepositories;
using Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel;
using Shyryi_WatchForYou.Stores;

namespace Shyryi_WatchForYou.ViewModels
{
    public class EnterViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentChildView => _navigationStore.CurrentViewModel;

        // constructors
        public EnterViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentChildView));
        }
    }
}
