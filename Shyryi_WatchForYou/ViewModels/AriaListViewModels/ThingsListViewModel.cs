using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Exceptions;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Services;
using Shyryi_WatchForYou.Services.ModelServices;
using Shyryi_WatchForYou.Views.AriaListView;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shyryi_WatchForYou.ViewModels.AriaListViewModels
{
    public class ThingsListViewModel : ViewModelBase
    {
        public static event Action<int> ThingIsDeleted;

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
            ShowCommand = new RelayCommand(ExecuteShowCommand, CanExecuteShowCommand);
            ChangeCommand = new RelayCommand(ExecuteChangeCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);

            things = new ObservableCollection<ThingDto>(ThingService.GetThingsByArea(areaId));
        }

        private async void ExecuteShowCommand(object parameter)
        {
            if (parameter is ThingDto thing)
            {
                var mainAreaViewModel = new CameraViewModel(thing);

                var areaAreaWindow = new CameraView(thing)
                {
                    DataContext = mainAreaViewModel
                };

                await ShowWindowAsync(areaAreaWindow);
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

        private bool CanExecuteShowCommand(object obj)
        {
            if (obj is ThingDto thing)
            {
                return thing.IsVideo == true;
            }
            return false;
        }
        private void ExecuteChangeCommand(object parameter)
        {
            if (parameter is ThingDto thing)
            {
                try
                {
                    ThingService.UpdateThing(thing.Id, ThingMapper.MapToDto(new ThingModel(
                        thing.Id, thing.Name, thing.Ip, thing.IsVideo, thing.IsAlerted, thing.Description, thing.AreaId)));
                }
                catch (Exception ex)
                {
                    ExceptionService.HandleDataBaseException<bool>(ex);
                }
            }
        }

        private void ExecuteDeleteCommand(object parameter)
        {
            if (parameter is ThingDto thing)
            {
                ThingIsDeleted?.Invoke(thing.Id);
                ThingService.RemoveThing(thing.Id);
                things.Remove(thing);
            }
        }
    }
}
