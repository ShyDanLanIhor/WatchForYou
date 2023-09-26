using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Repositories;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.Views.AriaListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shyryi_WatchForYou.ViewModels.AriaListViewModels
{
    public class ThingsListViewModel : ViewModelBase
    {
        private ObservableCollection<ThingDto> things;

        public ObservableCollection<ThingDto> Things
        {
            get { return things; }
            set
            {
                things = value;
                OnPropertyChanged(nameof(Things));
            }
        }

        private ThingDto selectedThing;
        public ThingDto SelectedThing
        {
            get { return selectedThing; }
            set
            {
                selectedThing = value;
                OnPropertyChanged(nameof(SelectedThing));
            }
        }

        public ICommand ShowCommand { get; }
        public ICommand ChangeCommand { get; }
        public ICommand DeleteCommand { get; }

        public ThingsListViewModel(int areaId)
        {
            ShowCommand = new RelayCommand(ExecuteShowCommand);
            ChangeCommand = new RelayCommand(ExecuteChangeCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);

            things = new ObservableCollection<ThingDto>(ThingService.GetThingsByArea(areaId));
        }

        private async void ExecuteShowCommand(object parameter)
        {
            if (parameter is ThingDto thing)
            {
                // Створюємо ViewModel для нового вікна
                CameraViewModel.currentWebViewUrl = thing.Ip;
                var mainAreaViewModel = new CameraViewModel();

                // Створюємо нове вікно та встановлюємо його ViewModel
                var areaAreaWindow = new CameraView()
                {
                    DataContext = mainAreaViewModel
                };

                // Асинхронно показуємо вікно
                await ShowWindowAsync(areaAreaWindow);
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

            window.Show();

            await tcs.Task;
        }

        private void ExecuteChangeCommand(object parameter)
        {
            if (parameter is ThingDto thing)
            {
                ThingService.UpdateThing(thing.Id, ThingMapper.MapToDto(new ThingModel(
                    thing.Id, thing.Name, thing.Ip, thing.IsVideo, thing.IsAlerted, thing.Description, thing.AreaId,
                    AreaMapper.MapToModel(AreaService.GetAreaById(thing.AreaId)))));
            }
        }

        private void ExecuteDeleteCommand(object parameter)
        {
            if (parameter is ThingDto thing)
            {
                ThingService.RemoveThing(thing.Id);
                things.Remove(thing);
            }
        }
    }
}
