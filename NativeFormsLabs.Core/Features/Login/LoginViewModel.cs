namespace NativeFormsLabs.Core.Features.Login
{
	using ReactiveUI;
	using Core.Services;
	using System;
	using System.Reactive.Linq;
	using System.Threading.Tasks;

	public class LoginViewModel : ReactiveObject
	{
		private INavigationService navService;
		private ILoginWebService loginService;
		private IStorageService storageService;

		private string username = string.Empty;
		private string password = string.Empty;
		private bool isLoading;
		private ReactiveCommand doLoginCommand;

		private readonly Interaction<string, bool> errorOnLogin;

        public LoginViewModel()
        {
            this.navService = ServiceLocator.Current.Resolve<INavigationService>();
            this.loginService = ServiceLocator.Current.Resolve<ILoginWebService>();
            this.storageService = ServiceLocator.Current.Resolve<IStorageService>();
            errorOnLogin = new Interaction<string, bool>();
            SetupCommands();
        }

		public LoginViewModel(INavigationService navService, ILoginWebService loginService, IStorageService storageService)
		{
			this.navService = navService;
			this.loginService = loginService;
			this.storageService = storageService;
			errorOnLogin = new Interaction<string, bool>();
			SetupCommands();
		}

		public string Username
		{
			get => username;
			set => this.RaiseAndSetIfChanged(ref username, value);
		}

		public string Password
		{
			get => password;
			set => this.RaiseAndSetIfChanged(ref password, value);
		}

		public bool IsLoading
        {
            get => isLoading;
            set => this.RaiseAndSetIfChanged(ref isLoading, value);
        }

		public Interaction<string, bool> ErrorOnLogin => errorOnLogin;

		public ReactiveCommand DoLoginCommand => doLoginCommand;

		private void SetupCommands()
		{
			doLoginCommand = ReactiveCommand.CreateFromTask(ExecuteLogin, CanExecuteLogin());
		}

		private async Task ExecuteLogin()
		{
            IsLoading = true;
			var loginResult = await loginService.LoginUser(username, password);
			if (string.IsNullOrWhiteSpace(loginResult.Error))
			{
                await Task.Delay(5000);
				navService.NavigateToMainScreenFromLogin();
			}
			else
			{
				var response = await errorOnLogin.Handle(loginResult.Error);
			}
            IsLoading = false;
        }

		private IObservable<bool> CanExecuteLogin()
		{
			return this.WhenAnyValue(vm => vm.Username, vm => vm.Password, vm => vm.IsLoading, (usr, pwd, ldg) =>
			{
				return !ldg && !string.IsNullOrWhiteSpace(usr) && !string.IsNullOrWhiteSpace(pwd);
            });
		}
	}
}
