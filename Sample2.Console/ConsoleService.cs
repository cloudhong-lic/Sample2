using System;
using System.Threading;
using Library.Logging.v0;
using Ninject;
using Sample2.Console.Handlers;
using Topshelf;

namespace Sample2.Console
{
	internal class ConsoleService
	{
		private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
		private readonly IKernel _kernel;
		private readonly ILog _logger;

		public ConsoleService(ILogFactory logFactory, IKernel kernel)
		{
			_kernel = kernel;
			_logger = logFactory.CreateLog(GetType());
		}

		public bool Start(HostControl hostControl)
		{
			try
			{
				_kernel.Get<AnimalHandler>().Handler();

				return true;
			}
			catch (Exception ex)
			{
				_logger.Fatal("Error starting service", ex);
				return false;
			}
		}

		public void Stop()
		{
			_logger.Info("Stopping service");
			_cancellationToken.Cancel();
			_logger.Info("Service stopped");
		}
	}
}