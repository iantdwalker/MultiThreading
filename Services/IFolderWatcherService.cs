using MultiThreading.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading.Services
{
	public interface IFolderWatcherService
	{
		void StartFolderWatch(string path);
		event PlantFileAddedEvent PlantFileAdded;
	}
}
