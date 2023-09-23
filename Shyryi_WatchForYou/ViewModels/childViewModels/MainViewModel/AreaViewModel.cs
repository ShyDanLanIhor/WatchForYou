using Shyryi_WatchForYou.Commands;
using Shyryi_WatchForYou.Data;
using Shyryi_WatchForYou.DTOs;
using Shyryi_WatchForYou.Mappers;
using Shyryi_WatchForYou.Models;
using Shyryi_WatchForYou.Services;
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
    public class AreasViewModel : ViewModelBase
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

        public AreasViewModel()
        {
            ShowDetailsCommand = new RelayCommand(ShowDetails, CanShowDetails);
            DeleteCommand = new RelayCommand(Delete, CanDelete);

            areas = new ObservableCollection<AreaDto>(areaService.GetAreasByCurrentUser());
        }

        private void ShowDetails(object parameter)
        {
            if (parameter is AreaDto area)
            {
                MessageBox.Show("Так");
            }
        }

        private bool CanShowDetails(object parameter)
        {
            return true;
        }

        private void Delete(object parameter)
        {
            if (parameter is AreaDto area)
            {
                MessageBox.Show("Так я " + area.Id);
            }
        }

        private bool CanDelete(object parameter)
        {
            return true;
        }
    }
}
