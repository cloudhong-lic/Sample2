using Library.Logging;
using Library.Logging.v0;
using Sample.WebApi.Client.Interfaces.v0;

namespace Sample2.Console.Handlers
{
	internal class AnimalHandler
	{
		private readonly IAnimalsProvider _animalsProvider;
		private readonly ILog _logger;

		public AnimalHandler(IAnimalsProvider animalsProvider, ILogFactory logFactory)
		{
			_animalsProvider = animalsProvider;
			_logger = logFactory.CreateLog(GetType());
		}

		public void Handler()
		{
			_logger.Info("Calling IAnimalsProvider WebApi");

			var animal = _animalsProvider.Get(1).Result;

			_logger.Info(() => LoggingConvention.ForLogging("Get IAnimalsProvider WebApi", animal));
		}
	}
}