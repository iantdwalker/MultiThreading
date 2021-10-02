using System;
using MultiThreading.Model;
using System.IO;
using System.Xml.Serialization;
using System.Windows;
using MultiThreading.Helpers;
using System.Threading;

namespace MultiThreading.Services
{
	public class PlantProviderService : IPlantProviderService
	{
		private readonly XmlSerializer _plantXmlSerializer;
		private readonly AutoResetEvent _autoResetEvent;

		public PlantProviderService()
		{
			_plantXmlSerializer = new XmlSerializer(typeof(Plant));
			_autoResetEvent = new AutoResetEvent(false);
		}

		public Plant LoadPlant(string plantXmlFile)
		{
			Plant plant = null;
			ThreadFactory.CreateAndStartThread(() => DeserializePlantXml(plantXmlFile, ref plant), "PlantProviderService");
			_autoResetEvent.WaitOne();
			return plant;
		}

		private void DeserializePlantXml(string plantXmlFilePath, ref Plant plant)
		{
			try
			{
				using (var plantXmlFileStream = new FileStream(plantXmlFilePath, FileMode.Open))
				{
					plant = (Plant)_plantXmlSerializer.Deserialize(plantXmlFileStream);
					_autoResetEvent.Set();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error occurred deserializing Plant XML in the plant provider service");
			}
		}
	}
}
