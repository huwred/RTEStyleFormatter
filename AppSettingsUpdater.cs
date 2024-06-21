using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace RTEStyleFormatter
{
    /// <summary>
    /// Utility class to update/create values in appsettings.json file
    /// </summary>
    public class AppSettingsUpdater
    {
        private const string emptyJson = "{}";
        private const string settinsgFileName = "appsettings.json";
        private readonly IWebHostEnvironment _env;

        public AppSettingsUpdater(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }
        /// <summary>
        /// This method will update the value for a given key in the json file
        /// </summary>
        /// <param name="key">Name of the json key</param>
        /// <param name="value">New value for the setting</param>
        /// <exception cref="ArgumentException"></exception>
        public void UpdateAppSetting(string key, object value)
        {
            if (key == null)
            {
                throw new ArgumentException("Json property key cannot be null", nameof(key));
            }
            
            var config = File.ReadAllText(settinsgFileName);
            
            var updatedConfigDict = UpdateJson(key, value, config);
            // After receiving the dictionary with updated key value pair, we serialize it back into json.
            var updatedJson = JsonSerializer.Serialize(updatedConfigDict, new JsonSerializerOptions { WriteIndented = true ,});

            string bJsonString = System.Text.Json.JsonSerializer.Serialize(
             value: updatedConfigDict,
             options: new System.Text.Json.JsonSerializerOptions
             { 
                 WriteIndented = true,
                   Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
              });

            File.WriteAllText(settinsgFileName, bJsonString);
        }


        /// <summary>
        /// This method will recursively read json segments separated by semicolon (firstObject:nestedObject:someProperty)
        /// until it reaches the desired property that needs to be updated,
        /// it will update the property and return json document represented by dictonary of dictionaries of dictionaries and so on.
        /// This dictionary structure can be easily serialized back into json
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="jsonSegment"></param>
        /// <returns></returns>
        private Dictionary<string, object> UpdateJson(string key, object value, string jsonSegment)
        {
            const char keySeparator = ':';

            var config = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonSegment);
            if (config != null)
            {
                var keyParts = key.Split(keySeparator);
                if (keyParts.Length > 1)
                {
                    var firstKeyPart = keyParts[0];
                    var remainingKey = string.Join(keySeparator, keyParts.Skip(1));

                    // If the key does not exist already, we will create a new key and append it to the json
                    var newJsonSegment = config.ContainsKey(firstKeyPart) && config[firstKeyPart] != null
                        ? config[firstKeyPart].ToString()
                        : emptyJson;
                    config[firstKeyPart] = UpdateJson(remainingKey, value, newJsonSegment);
                }
                else
                {
                    config[key] = value;
                }
            }

            return config;
        }

    }
}
