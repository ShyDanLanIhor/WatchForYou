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
using System.Windows.Input;

namespace Shyryi_WatchForYou.ViewModels.AriaListViewModels
{
    public class ThingsListViewModel : ViewModelBase
    {
        private ObservableCollection<ThingDto> things;

        private AreaService areaService =
            new AreaService();
        private ThingService thingService =
            new ThingService();
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

        public ICommand ChangeCommand { get; }
        public ICommand DeleteCommand { get; }

        public ThingsListViewModel(int areaId)
        {
            ChangeCommand = new RelayCommand(ExecuteChangeCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);

            things = new ObservableCollection<ThingDto>(thingService.GetThingsByArea(areaId));
        }

        private void ExecuteChangeCommand(object parameter)
        {
            if (parameter is ThingDto thing)
            {
                thingService.UpdateThing(thing.Id, ThingMapper.MapToDto(new ThingModel(
                    thing.Id, thing.Name, thing.Ip, thing.IsVideo, thing.IsAlerted, thing.Description, thing.AreaId,
                    AreaMapper.MapToModel(areaService.GetAreaById(thing.AreaId)))));
            }
        }

        private void ExecuteDeleteCommand(object parameter)
        {
            if (parameter is ThingDto thing)
            {
                thingService.RemoveThing(thing.Id);
                things.Remove(thing);
            }
        }
    }
}
