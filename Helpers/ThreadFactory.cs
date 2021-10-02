using System.Threading;

namespace MultiThreading.Helpers
{
	public class ThreadFactory
	{
		public static void CreateAndStartThread(ThreadStart threadStart, string threadName)
		{
			var thread = new Thread(threadStart)
			{
				Name = threadName
			};
			thread.Start();
		}
	}
}
