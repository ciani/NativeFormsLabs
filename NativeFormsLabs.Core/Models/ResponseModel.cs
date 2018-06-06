namespace NativeFormsLabs.Core.Models
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	public class ResponseModel
    {
		[JsonProperty("page")]
		public long Page { get; set; }

		[JsonProperty("total")]
		public long Total { get; set; }

		[JsonProperty("data")]
		public List<ItemModel> Data { get; set; }

		[JsonProperty("per_page")]
		public long PerPage { get; set; }

		[JsonProperty("total_pages")]
		public long TotalPages { get; set; }
	}
}
