namespace NativeFormsLabs.Core.Features.Login
{
	using ReactiveUI;

	public class LoginModel : ReactiveObject
	{
		private string token;
		private string error;

		public LoginModel()
		{
		}

		[Newtonsoft.Json.JsonProperty("token")]
		public string Token
		{
			get => token;
			set => this.RaiseAndSetIfChanged(ref token, value);
		}

		[Newtonsoft.Json.JsonProperty("error")]
		public string Error
		{
			get => error;
			set => this.RaiseAndSetIfChanged(ref error, value);
		}
	}
}
