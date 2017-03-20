using System;
using System.Threading;
using Library.Configuration.MassTransit;
using MassTransit;
using MassTransit.NLogIntegration;
using Ninject;
using NLog;
using Sample2.Handlers;
using Topshelf;

namespace Sample2.Service
{
	public class Sample2Service
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
		private readonly IKernel _kernel;
		private IBusControl _busControl;

		public Sample2Service(IKernel kernel)
		{
			_kernel = kernel;
		}

		public bool Start(HostControl hostControl)
		{
			_logger.Info("Starting service");
			hostControl.RequestAdditionalTime(TimeSpan.FromSeconds(60));

			try
			{
				_logger.Info("Start MassTransit");
				_busControl = ConfigureMassTransit();
				_busControl.Start();

				_kernel.Get<Sample2Handler>().Handle(1).Wait();
				//_logger.Info($"AnimalKey:{animal.AnimalKey}, Sex:{animal.Sex}, Species:{animal.Species}");

				_logger.Info("Service started");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Fatal(ex, "Error starting service");
				return false;
			}
		}

		public void Stop()
		{
			_logger.Info("Stopping service");
			_busControl?.Stop();
			_cancellationToken.Cancel();
			_logger.Info("Service stopped");
		}

		private IBusControl ConfigureMassTransit()
		{
			var busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
			{
				sbc.SetupRabbitMqHostFromConfig();
				sbc.UseInMemoryOutbox();
				sbc.UseNLog();
			});

			_kernel.Bind<IBusControl>().ToConstant(busControl);
			_kernel.Bind<IBus>().ToConstant(busControl);

			return busControl;
		}
	}
}
