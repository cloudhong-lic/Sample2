using Topshelf;
using Topshelf.Ninject;

namespace Sample2.Service
{
	class Program
	{
		static void Main(string[] args)
		{
			HostFactory.Run(hf =>
			{
				hf.UseNinject(
					new Sample2Module()
				);
				hf.Service<Sample2Service>(svc =>
				{
					svc.ConstructUsingNinject();
					svc.WhenStarted((service, control) => service.Start(control));
					svc.WhenStopped(ais => ais.Stop());
				});

				// Set up dependencies
				hf.UseNLog();
				hf.DependsOnMsmq();

				// Set up identification strings
				hf.SetDescription("Sample2 Service to handle messages");
				hf.SetDisplayName("Sample2 - Service");
				hf.SetServiceName("Sample2.Service");
			});
		}
	}
}
