using System.IO;
using System.Text.Json;

namespace Micro.Service.Base.UnitTests
{
    public class ServiceUnitTestsConfiguration
    {
        public static T ReadJson<T>(string file)
        {
            using (StreamReader reader = new StreamReader($"../../../Inputs/{file}.json"))
            {
                var json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<T>(json);
            }
        }
    }
}
