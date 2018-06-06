namespace NativeFormsLabs.Core.Models
{
	using Newtonsoft.Json;
	using ReactiveUI;

	public class ItemModel : ReactiveObject
    {
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("pantone_value")]
		public string PantoneValue { get; set; }

		[JsonProperty("color")]
		public string Color { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("year")]
		public long Year { get; set; }
	}
}
