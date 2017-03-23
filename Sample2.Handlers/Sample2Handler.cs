using System;
using System.Threading.Tasks;
using Library.Logging;
using Library.Messaging.MassTransit;
using MassTransit;
using NLog;
using Sample2.Messages.Events.v0;

namespace Sample2.Handlers
{
	public class Sample2Handler
	{
		private readonly IBus _mtBus;
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public Sample2Handler(IBus mtBus)
		{
			_mtBus = mtBus;
		}

		public async Task Handle(int animalKey)
		{
			Action<INewFarmerEvent> action = h =>
			{
				h.Id = 1;
				h.Name = "A farmer";
			};

			await _mtBus.PublishAnonymous<INewFarmerEvent>(action).ConfigureAwait(false);

			_logger.Info(() => LoggingConvention.ForLogging("Publish a INewFarmerEvent message", new { Id = 1, Name = "A farmer" }));
		}
	}
}
