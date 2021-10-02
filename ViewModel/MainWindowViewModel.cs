using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MultiThreading.Model;
using MultiThreading.Helpers;
using MultiThreading.Services;
using System.IO;
using System.Windows;
using System.Windows.Data;

namespace MultiThreading.ViewModel
{
	public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
	{
		private readonly ObservableCollection<Plant> _plants;
		private int _totalPlants = 0;
		private readonly ICommand _loadPlantsCommand;
		private readonly ICommand _clearPlantsCommand;
		private readonly ICommand _trickleFeedPlantsCommand;
		private Plant _selectedPlant;
		private readonly IPlantProviderService _plantProviderService;
		private IFolderWatcherService _folderWatcherService;
		private readonly ITrickleFeedPlantsService _trickleFeedPlantsService;
		private readonly object _plantsLock = new object();
		private readonly string _plantsDirectory;
		private readonly string _morePlantsDirectory;

		public MainWindowViewModel()
		{
			_plants = new ObservableCollection<Plant>();
			_loadPlantsCommand = new RelayCommand(LoadPlants);
			_clearPlantsCommand = new RelayCommand(ClearPlants);
			_trickleFeedPlantsCommand = new RelayCommand(TrickleFeedPlants);
			_plantProviderService = new PlantProviderService();
			_folderWatcherService = new FolderWatcherService();
			_trickleFeedPlantsService = new TrickleFeedPlantsService();
			
			BindingOperations.EnableCollectionSynchronization(Plants, _plantsLock);

			_plantsDirectory = Environment.CurrentDirectory + @"\Plants";
			_morePlantsDirectory = Environment.CurrentDirectory + @"\MorePlants";

			_folderWatcherService.PlantFileAdded += OnPlantFileAdded;
			_folderWatcherService.StartFolderWatch(_plantsDirectory);
		}

		#region IMainWindowViewModel Members

		public ObservableCollection<Plant> Plants
		{
			get
			{
				return _plants;
			}
		}

		public int TotalPlants
		{
			get
			{
				return _totalPlants;
			}
		}

		public ICommand LoadPlantsCommand
		{
			get
			{
				return _loadPlantsCommand;
			}
		}

		public ICommand ClearPlantsCommand
		{
			get
			{
				return _clearPlantsCommand;
			}
		}

		public ICommand TrickleFeedPlantsCommand
		{
			get
			{
				return _trickleFeedPlantsCommand;
			}
		}

		public Plant SelectedPlant
		{
			get
			{
				return _selectedPlant;
			}

			set
			{
				if (_selectedPlant != value)
				{
					_selectedPlant = value;
					OnPropertyChanged(nameof(SelectedPlant));
				}				
			}
		}

		#endregion		

		#region Private Methods

		private void LoadPlants(object parameter)
		{
			try
			{
				var plantXmlFiles = Directory.GetFiles(_plantsDirectory, "*.xml", SearchOption.AllDirectories);
				foreach (var plantXmlFile in plantXmlFiles)
				{
					LoadPlant(plantXmlFile);					
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "A error ocurred loading the plants from the plant provider service");
			}						
		}

		private void LoadPlant(string plantXmlFile)
		{
			try
			{
				var plant = _plantProviderService.LoadPlant(plantXmlFile);
				AddPlantToList(plant);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "A error ocurred loading a plant from the plant provider service");
			}
		}

		private void AddPlantToList(Plant plant)
		{
			if (plant != null)
			{
				lock (_plantsLock)
				{
					_totalPlants++;
					plant.Number = _totalPlants;
					Plants.Add(plant);
				}
			}
		}

		private void TrickleFeedPlants(object parameter)
		{
			_trickleFeedPlantsService.Start(_morePlantsDirectory, _plantsDirectory);
		}

		private void OnPlantFileAdded(PlantFileAddedEventArgs args)
		{
			LoadPlants(args.FileName);
		}

		private void ClearPlants(object parameter)
		{
			Plants.Clear();
			_totalPlants = 0;
		}		

		#endregion

		#region PropertyChanged Event

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
