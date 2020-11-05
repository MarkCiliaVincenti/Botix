using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace Botix.Common.API
{
    public static class SerializationSettings
    {
        public static void ConfigureJsonOptions(JsonOptions options)
        {
            options.JsonSerializerOptions.IgnoreNullValues = true;
            options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        }
    }
}
