namespace NativeFormsLabs.Core.Features.Login
{
	using ModernHttpClient;
	using Newtonsoft.Json;
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Threading.Tasks;

	public class LoginWebService : ILoginWebService
	{
		public async Task<LoginModel> LoginUser(string username, string password)
		{
			LoginModel login = new LoginModel();
			HttpClient client = new HttpClient(new NativeMessageHandler());
			var listOfParams = new List<KeyValuePair<string, string>>()
			{
				new KeyValuePair<string, string>("email",username),
			};
			if (!string.IsNullOrWhiteSpace(password))
				listOfParams.Add(new KeyValuePair<string, string>("password", password));

			FormUrlEncodedContent content = new FormUrlEncodedContent(listOfParams);
			HttpResponseMessage response = await client.PostAsync("http://reqres.in/api/login", content);

			string responseJson = await response.Content.ReadAsStringAsync();
			if (!string.IsNullOrEmpty(responseJson))
				login = JsonConvert.DeserializeObject<LoginModel>(responseJson);

			return login;
		}
	}
}
