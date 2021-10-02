using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading.Helpers
{
	public delegate void PlantFileAddedEvent(PlantFileAddedEventArgs args);

	public class PlantFileAddedEventArgs : EventArgs
	{
		public PlantFileAddedEventArgs(string fileName)
		{
			FileName = fileName;
		}

		public string FileName { get; set; }
	}
}
