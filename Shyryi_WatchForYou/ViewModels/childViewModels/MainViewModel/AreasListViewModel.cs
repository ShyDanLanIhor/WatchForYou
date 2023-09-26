using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.ViewModels.AriaListViewModels;
using Shyryi_WatchForYou.Views.AriaListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shyryi_WatchForYou.ViewModels.childViewModels.MainViewModel
{
    public class AreasListViewModel : ViewModelBase
    {
        private ObservableCollection<AreaDto> areas;
       
        public ObservableCollection<AreaDto> Areas
        {
            get { return areas; }
            set
            {
                areas = value;
                OnPropertyChanged(nameof(Areas));
            }
        }

        private AreaDto selectedArea;
        public AreaDto SelectedArea
        {
            get { return selectedArea; }
            set
            {
                selectedArea = value;
                OnPropertyChanged(nameof(SelectedArea));
            }
        }

        public ICommand ShowDetailsCommand { get; }
        public ICommand DeleteCommand { get; }

        public AreasListViewModel()
        {
            ShowDetailsCommand = new RelayCommand(ShowDetails);
            DeleteCommand = new RelayCommand(Delete);

            areas = new ObservableCollection<AreaDto>(AreaService.GetAreasByCurrentUser());
        }

        private async void ShowDetails(object parameter)
        {
            if (parameter is AreaDto area)
            {
                var mainAreaViewModel = new MainAriaViewModel(area.Id);
                var areaAreaWindow = new MainAriaView
                {
                    DataContext = mainAreaViewModel
                };

                await ShowWindowAsync(areaAreaWindow);
            }
        }

        private void Delete(object parameter)
        {
            if (parameter is AreaDto area)
            {
                AreaService.RemoveArea(area.Id);
                areas.Remove(area);
            }
        }

        private async Task ShowWindowAsync(Window window)
        {
            var tcs = new TaskCompletionSource<bool>();

            EventHandler handler = null;
            handler = (s, e) =>
            {
                window.Closed -= handler;
                tcs.SetResult(true);
            };

            window.Closed += handler;

            window.Show();

            await tcs.Task;
        }
    }
}
