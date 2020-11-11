using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Steam.Utilities
{
    public class StringToIntJSONConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            if(reader.TokenType == JsonTokenType.String)
            {
                int number;

                bool success = int.TryParse(reader.GetString(), out number);

                if(success)
                {
                    return number;

                }

                return 0;

            }


            return reader.GetInt32();

        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
