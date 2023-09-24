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
        
        private AreaService areaService = 
            new AreaService();
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

            areas = new ObservableCollection<AreaDto>(areaService.GetAreasByCurrentUser());
        }

        private async void ShowDetails(object parameter)
        {
            if (parameter is AreaDto area)
            {
                // Створюємо ViewModel для нового вікна
                var mainAreaViewModel = new MainAriaViewModel(area);

                // Створюємо нове вікно та встановлюємо його ViewModel
                var areaAreaWindow = new MainAriaView
                {
                    DataContext = mainAreaViewModel
                };

                // Асинхронно показуємо вікно
                await ShowWindowAsync(areaAreaWindow);
            }
        }

        private void Delete(object parameter)
        {
            if (parameter is AreaDto area)
            {
                areaService.RemoveArea(area.Id);
                areas.Remove(area);
            }
        }

        private async Task ShowWindowAsync(Window window)
        {
            // Використовуємо TaskCompletionSource для очікування закриття вікна
            var tcs = new TaskCompletionSource<bool>();

            // Додаємо обробник події закриття вікна
            EventHandler handler = null;
            handler = (s, e) =>
            {
                window.Closed -= handler;
                tcs.SetResult(true);
            };

            window.Closed += handler;

            // Показуємо вікно
            window.Show();

            // Очікуємо закриття вікна
            await tcs.Task;
        }
    }
}
