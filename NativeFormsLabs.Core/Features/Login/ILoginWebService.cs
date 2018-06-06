namespace NativeFormsLabs.Core.Features.Login
{
	using System.Threading.Tasks;

	public interface ILoginWebService
    {
		Task<LoginModel> LoginUser(string username, string password);
    }
}
