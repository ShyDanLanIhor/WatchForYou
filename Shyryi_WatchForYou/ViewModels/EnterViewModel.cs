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
