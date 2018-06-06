namespace NativeFormsLabs.Core.Features.MapDetail
{
	using Core.Models;
	using System.Threading.Tasks;

	public interface IGpsService
	{
		Task<CoordinateModel> GetCurrentPositionAsync();
	}
}
