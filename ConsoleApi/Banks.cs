using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConsoleApi
{

       public partial class Banks
        {
            [JsonProperty("Код банку")]
            public long КодБанку { get; set; }

            [JsonProperty("Ліцензії")]
            public List<Ліцензії> Ліцензії { get; set; }

            [JsonProperty("МФО")]
            public long Мфо { get; set; }

            [JsonProperty("Структура власності")]
            public List<СтруктураВласності> СтруктураВласності { get; set; }

            [JsonProperty("Структурні підрозділи по датах")]
            public List<СтруктурніПідрозділиПоДатах> СтруктурніПідрозділиПоДатах { get; set; }

            [JsonProperty("Структурні підрозділи по регіонах")]
            public List<СтруктурніПідрозділиПоРегіонах> СтруктурніПідрозділиПоРегіонах { get; set; }
        }

        public partial class Ліцензії
        {
            [JsonProperty("Адреса")]
            public string Адреса { get; set; }

            [JsonProperty("Банківська ліцензія")]
            public БанківськаЛіцензія БанківськаЛіцензія { get; set; }

            [JsonProperty("Дозволи")]
            public List<Дозволи> Дозволи { get; set; }

            [JsonProperty("Код ЄДРПОУ")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long КодЄдрпоу { get; set; }

            [JsonProperty("Назва банку")]
            public string НазваБанку { get; set; }

            [JsonProperty("Номер банку (МФО)")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long НомерБанкуМфо { get; set; }
        }

        public partial class БанківськаЛіцензія
        {
            [JsonProperty("Перелік операцій ліцензії")]
            public ПерелікОпераційЛіцензії ПерелікОпераційЛіцензії { get; set; }

            [JsonProperty("від")]
            public string Від { get; set; }

            [JsonProperty("номер бланка")]
            public string НомерБланка { get; set; }

            [JsonProperty("номер ліцензії")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long НомерЛіцензії { get; set; }
        }

        public partial class ПерелікОпераційЛіцензії
        {
            [JsonProperty("1", NullValueHandling = NullValueHandling.Ignore)]
            public string The1 { get; set; }

            [JsonProperty("2", NullValueHandling = NullValueHandling.Ignore)]
            public string The2 { get; set; }

            [JsonProperty("3", NullValueHandling = NullValueHandling.Ignore)]
            public string The3 { get; set; }

            [JsonProperty("1Л", NullValueHandling = NullValueHandling.Ignore)]
            public string The1Л { get; set; }

            [JsonProperty("1Н", NullValueHandling = NullValueHandling.Ignore)]
            public string The1Н { get; set; }

            [JsonProperty("2Л", NullValueHandling = NullValueHandling.Ignore)]
            public string The2Л { get; set; }

            [JsonProperty("2Н", NullValueHandling = NullValueHandling.Ignore)]
            public string The2Н { get; set; }

            [JsonProperty("3Л", NullValueHandling = NullValueHandling.Ignore)]
            public string The3Л { get; set; }

            [JsonProperty("3Н", NullValueHandling = NullValueHandling.Ignore)]
            public string The3Н { get; set; }

            [JsonProperty("4Н", NullValueHandling = NullValueHandling.Ignore)]
            public string The4Н { get; set; }

            [JsonProperty("5Н", NullValueHandling = NullValueHandling.Ignore)]
            public string The5Н { get; set; }

            [JsonProperty("6Н", NullValueHandling = NullValueHandling.Ignore)]
            public string The6Н { get; set; }

            [JsonProperty("7Н", NullValueHandling = NullValueHandling.Ignore)]
            public string The7Н { get; set; }
        }

        public partial class Дозволи
        {
            [JsonProperty("Перелік операцій дозвола")]
            public Dictionary<string, string> ПерелікОпераційДозвола { get; set; }

            [JsonProperty("від")]
            public string Від { get; set; }

            [JsonProperty("номер дозволу")]
            public string НомерДозволу { get; set; }
        }

        public partial class СтруктураВласності
        {
            [JsonProperty("Дата")]
            public string Дата { get; set; }

            [JsonProperty("Посилання")]
            public Uri Посилання { get; set; }
        }

        public partial class СтруктурніПідрозділиПоДатах
        {
            [JsonProperty("Дата")]
            public string Дата { get; set; }

            [JsonProperty("Кількість")]
            public long Кількість { get; set; }
        }

        public partial class СтруктурніПідрозділиПоРегіонах
        {
            [JsonProperty("Кількість")]
            public long Кількість { get; set; }

            [JsonProperty("Регіон")]
            public string Регіон { get; set; }
        }

        public partial class Banks
        {
        public static Banks FromJson(string json) => JsonConvert.DeserializeObject<Banks>(json, Converter.Settings);
    }

        public static class Serialize
        {
        public static string ToJson(this Banks self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }

        internal class ParseStringConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                long l;
                if (Int64.TryParse(value, out l))
                {
                    return l;
                }
                throw new Exception("Cannot unmarshal type long");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (long)untypedValue;
                serializer.Serialize(writer, value.ToString());
                return;
            }

            public static readonly ParseStringConverter Singleton = new ParseStringConverter();
        }
    }


