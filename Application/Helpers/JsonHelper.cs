using Newtonsoft.Json;

namespace Application.Helpers
{
    /// <summary>
    /// Class to handle json serialization, deserialization
    /// and json task in general
    /// handle 
    /// </summary>
    static public class JsonHelper
    {
        /// <summary>
        /// Read and deserialize a Json file
        /// </summary>
        /// <typeparam name="T">Format type template</typeparam>
        /// <param name="JsonFileName">Filename (or FileRoute)</param>
        /// <returns>T Object</returns>
        static public T ReadJsonFile<T>(string JsonFileName) 
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(JsonFileName));
            }
            catch (Exception)
            {
                return JsonConvert.DeserializeObject<T>(string.Empty);
            }
        }
    }
}
