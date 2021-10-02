using MultiThreading.Model;

namespace MultiThreading.Services
{
	public interface IPlantProviderService
	{
		Plant LoadPlant(string plantXmlFile);
	}
}
