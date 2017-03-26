using Topshelf;
using Topshelf.Ninject;

namespace Sample2.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			// 在这里仍然使用Topshelf作为Console
			// 虽然显得繁琐了一点, 但得到了更多好处
			// TODO: 还是应该考虑做一个基本的例子
			HostFactory.Run(hf =>
			{
				hf.UseNinject(new ConsoleModule());

				hf.Service<ConsoleService>(svc =>
				{
					svc.ConstructUsingNinject();
					svc.WhenStarted((service, control) => service.Start(control));
					svc.WhenStopped(ais => ais.Stop());
				});

				// 在这里配置NLog可以将Topshelf的服务日志写入NLog
				// 否则只会在Console中显示
				hf.UseNLog();

				// Set up identification strings
				hf.SetDescription("Sample2 Console to get WebApi result from Sample WebApi client");
				hf.SetDisplayName("Sample2 - Console");
				hf.SetServiceName("Sample2.Console");
			});
		}
	}
}