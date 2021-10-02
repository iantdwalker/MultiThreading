using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading.Services
{
	public interface ITrickleFeedPlantsService
	{
		void Start(string sourceDirectory, string targetDirectory);
	}
}
