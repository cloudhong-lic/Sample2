using Ninject.Modules;

namespace Sample2.Service
{
	internal class Sample2Module : NinjectModule
	{
		public override void Load()
		{
			//Bind<Sample2Handler>().ToSelf();
		}
	}
}
