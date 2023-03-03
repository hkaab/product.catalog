using System;
using System.IO;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EOS.Infrastructure.Extensions
{
    public static class JsonExtensions
    {
        public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static string ToJson<T>(this T input)
        {
            return JsonSerializer.Serialize(input, SerializerOptions);
        }

        public static async Task<Stream> ToStream<T>(this T input)
        {
            var streamPayload = new MemoryStream();
            await JsonSerializer.SerializeAsync(streamPayload, input, SerializerOptions);
            streamPayload.Position = 0;
            return streamPayload;
        }

        public static async Task<T> FromStream<T>(this Stream streamPayload)
        {
            if (streamPayload == null)
            {
                return default;
            }

            var t = default(T);
            try
            {
                streamPayload.Position = 0;
                t = await JsonSerializer.DeserializeAsync<T>(streamPayload, SerializerOptions);
            }
            catch (JsonException)
            {
                // For empty array scenario
                streamPayload.Position = 0;
                var temp = await JsonSerializer.DeserializeAsync<string>(streamPayload, SerializerOptions); 
                t = JsonSerializer.Deserialize<T>(temp, SerializerOptions);
            }
            
            return t;
        }
    }
}