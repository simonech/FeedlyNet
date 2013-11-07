using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Feedly.NET.JsonSerialization
{
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType,
                             object existingValue, JsonSerializer serializer)
        {
            Type t = ReflectionUtils.IsNullableType(objectType)
                      ? Nullable.GetUnderlyingType(objectType) : objectType;
            if (reader.TokenType == JsonToken.Null)
            {
                if (!ReflectionUtils.IsNullableType(objectType))
                {
                    throw new Exception(String.Format("Cannot convert null value to {0}.",
                                       CultureInfo.InvariantCulture, new object[] { objectType }));
                }
                return null;
            }
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception(String.Format("Unexpected token parsing date. Expected Integer, got {0}.",
                                             CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
            }
            long ticks = (long)reader.Value;
            DateTime d = ticks.ConvertFromUnixTimestampMilliSec();
            if (t == typeof(DateTimeOffset))
            {
                return new DateTimeOffset(d);
            }
            return d;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long ticks = 0;
            if (value is DateTime)
            {
                ticks = ((DateTime)value).ToUniversalTime().ConvertToUnixTimestampMilliSec();
            }
            else
            {
                if (!(value is DateTimeOffset))
                {
                    throw new Exception("Expected date object value.");
                }
                DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
                ticks = dateTimeOffset.ToUniversalTime().UtcDateTime.ConvertToUnixTimestampMilliSec();
            }
            writer.WriteValue(ticks);
        }
    }
}
