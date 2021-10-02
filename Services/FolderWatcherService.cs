using MultiThreading.Helpers;
using System.IO;

namespace MultiThreading.Services
{
	public class FolderWatcherService : IFolderWatcherService
	{
		public event PlantFileAddedEvent PlantFileAdded;

		public void StartFolderWatch(string path)
		{
			var fileSystemWatcher = new FileSystemWatcher
			{
				Path = path,
				Filter = "*.xml"
			};
			fileSystemWatcher.Created += new FileSystemEventHandler(OnPlantFileAdded);
			fileSystemWatcher.EnableRaisingEvents = true;
		}

		private void OnPlantFileAdded(object sender, FileSystemEventArgs e)
		{
			if (this.PlantFileAdded != null)
			{
				PlantFileAdded(new PlantFileAddedEventArgs(e.FullPath));
			}
		}
	}
}
