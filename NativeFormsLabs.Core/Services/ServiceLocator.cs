namespace NativeFormsLabs.Core.Services
{
	using Autofac;
	using Core.Features.Login;
	using Core.Features.Main;
	using Core.Features.MapDetail;
	using Refit;
	using System.Net.Http;

	public class ServiceLocator
	{
		private static ServiceLocator currentInstance;
		private IContainer container;
		private ContainerBuilder builder;

		private ServiceLocator()
		{
			builder = new ContainerBuilder();

			builder.RegisterType<LoginViewModel>();
			builder.RegisterType<MainViewModel>();
			builder.RegisterType<MapDetailViewModel>();
			builder.RegisterType<LoginWebService>().As<ILoginWebService>();
			builder.RegisterType<StorageService>().As<IStorageService>();

			builder.Register(c =>
			{
				RefitSettings settings = new RefitSettings();
				settings.HttpMessageHandlerFactory = () => 
				{
					return container.Resolve<HttpMessageHandler>();
				};
				return RestService.For<IMainWebService>("https://reqres.in", settings);
			}).As<IMainWebService>();
		}

		public static ServiceLocator Current
		{
			get
			{
				if (currentInstance == null)
					currentInstance = new ServiceLocator();
				return currentInstance;
			}
		}

		public void RegisterDependency<TInterface, TClass>()
		{
			builder.RegisterType<TClass>().As<TInterface>();
		}

		public void BuildContainer()
		{
			container = builder.Build();
		}

		public T Resolve<T>()
		{
			return container.Resolve<T>();
		}
	}
}
