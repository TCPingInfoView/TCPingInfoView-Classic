using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TCPingInfoView.JsonConverters
{
	class IPAddressConverter : JsonConverter<IPAddress>
	{
		public override bool CanConvert(Type typeToConvert)
		{
			return typeToConvert == typeof(IPAddress);
		}

		public override IPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.String)
			{
				throw new JsonException();
			}
			return IPAddress.Parse(reader.GetString());
		}

		public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}
	}
}
