namespace NativeFormsLabs.Core.Features.Main
{
	using NativeFormsLabs.Core.Models;
	using Refit;
	using System.Threading.Tasks;

	public interface IMainWebService
	{
		[Get("/api/unknown")]
		Task<ResponseModel> GetItemsAsync(int per_page = 10, int delay = 5);
	}
}
