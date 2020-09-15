using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schema.NET;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchemaNet_SystemTextJson
{
	public class CustomType : Thing, IThing
	{
		[JsonPropertyName("@type")]
		public override string Type => "CustomType";

		[JsonPropertyName("number")]
		[JsonConverter(typeof(ValuesJsonConverter))]
		public OneOrMany<int?> Number { get; set; }

		[JsonPropertyName("uri")]
		[JsonConverter(typeof(ValuesJsonConverter))]
		public Values<string, Uri> Uri { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			var serializerOptions = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
				Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			};
			serializerOptions.Converters.Add(new JsonStringEnumConverter());


			var rawJson = @"{""@type"":""CustomType"",""number"":123,""@context"":""https://schema.org""}";
			var inputObj = new CustomType
			{
				Number = 123
			};

			var result = JsonSerializer.Serialize(inputObj, serializerOptions);

			Assert.AreEqual(rawJson, result);
		}
	}
}
