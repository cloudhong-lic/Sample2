using Library.Logging;
using Library.WebApi.Helpers;
using NLog;
using Sample.WebApi.Client.Interfaces.v0;
using Sample.WebApi.Client.v0;

namespace Sample2.Console
{
	internal class Program
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private static void Main(string[] args)
		{
			_logger.Info("Calling IAnimalsProvider WebApi");

			IAnimalsProvider animalsProvider = new AnimalsProvider(new HttpServiceHelper());
			var animal = animalsProvider.Get(1).Result;

			//TODO: 没有写入日志或打印到屏幕, 原因不详
			_logger.Info(() => LoggingConvention.ForLogging("Get IAnimalsProvider WebApi", animal));

			System.Console.WriteLine(animal.AnimalKey);
		}
	}
}
