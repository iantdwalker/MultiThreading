using MultiThreading.Helpers;
using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace MultiThreading.Services
{
	public class TrickleFeedPlantsService : ITrickleFeedPlantsService
	{
		public void Start(string sourceDirectory, string targetDirectory)
		{
			ThreadFactory.CreateAndStartThread(() => PerformTrickleFeed(sourceDirectory, targetDirectory), "TrickleFeedPlantsService");			
		}

		private void PerformTrickleFeed(string sourceDirectory, string targetDirectory)
		{
			try
			{
				var sourcePlantXmlFiles = Directory.GetFiles(sourceDirectory, "*.xml", SearchOption.AllDirectories);
				foreach (var sourcePlantXmlFile in sourcePlantXmlFiles)
				{
					var sourceFileInfo = new FileInfo(sourcePlantXmlFile);
					var destPlantXmlFileName = targetDirectory + "\\" + sourceFileInfo.Name;
					File.Copy(sourcePlantXmlFile, destPlantXmlFileName);

					Thread.Sleep(4000);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "A error ocurred trickle feeding plants");
			}
		}
	}
}
