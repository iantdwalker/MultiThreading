using MultiThreading.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiThreading.ViewModel
{
	public interface IMainWindowViewModel
	{
		ObservableCollection<Plant> Plants { get; }
		int TotalPlants { get; }
		ICommand LoadPlantsCommand { get; }
		ICommand ClearPlantsCommand { get; }
		ICommand TrickleFeedPlantsCommand { get; }
		Plant SelectedPlant { get; set; }		 
	}
}
