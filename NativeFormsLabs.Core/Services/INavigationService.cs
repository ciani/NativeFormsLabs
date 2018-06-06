namespace NativeFormsLabs.Core.Services
{
	/// <summary>
	/// Navigation service contract definition
	/// </summary>
	public interface INavigationService
	{
		void NavigateToMainScreenFromLogin();

		void NavigateToUserDetail();
	}
}
