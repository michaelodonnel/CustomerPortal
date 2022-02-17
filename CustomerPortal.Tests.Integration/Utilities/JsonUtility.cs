using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CustomerPortal.Tests.Integration.Utilities
{
    internal static class JsonUtility
    {
        public static StringContent ConvertToJsonContent(object obj)
        {
            return new StringContent(ConvertToJsonString(obj), Encoding.UTF8, "application/json");
        }

        private static string ConvertToJsonString(object obj)
        {
            return (obj != null) ? JsonConvert.SerializeObject(obj) : string.Empty;
        }
    }
}
