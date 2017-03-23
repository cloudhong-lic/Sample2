using Library.Logging.NLog;
using Library.Logging.v0;
using Library.WebApi.Helpers;
using Library.WebApi.Interfaces;
using Ninject.Modules;
using Sample.WebApi.Client.Interfaces.v0;
using Sample.WebApi.Client.v0;

namespace Sample2.Console
{
	internal class ConsoleModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IHttpServiceHelper>().To<HttpServiceHelper>();
			Bind<IAnimalsProvider>().To<AnimalsProvider>();
			Bind<ILogFactory>().To<LogFactory>();
		}
	}
}