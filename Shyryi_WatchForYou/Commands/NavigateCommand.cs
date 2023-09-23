using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou.Stores;
using Shyryi_WatchForYou.ViewModels;
using Shyryi_WatchForYou.ViewModels.childViewModels.EnterViewModel;

namespace Shyryi_WatchForYou.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationService _navigationService;

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
