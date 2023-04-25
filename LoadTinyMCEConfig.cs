using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace RTEStyleFormatter
{
    public class LoadTinyMCEConfig : IHostedService
    {
        private readonly IWebHostEnvironment _env;
        public LoadTinyMCEConfig(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            string settingsFile = "App_Plugins/TinyMCEStyleFormatter/RTE_style_formats.json";
            //does the json file exist?
            //if yes, minify it, escape the " and set the value in appsettings.json
            var styleSettings = _env.ContentRootFileProvider.GetFileInfo(settingsFile);
            if (styleSettings.Exists)
            {
                var fs = styleSettings.CreateReadStream();
                long _TotalBytes = fs.Length;

                // attach filestream to binary reader
                // convert stream to string
                StreamReader reader = new StreamReader( fs );
                string text = reader.ReadToEnd();

                //read the json from the file
                object obj = JsonConvert.DeserializeObject(text);
                //serialize to a minified jsonstring
                var jsonString = JsonConvert.SerializeObject(obj, Formatting.None);

                new AppSettingsUpdater(_env).UpdateAppSetting("Umbraco:CMS:RichTextEditor:CustomConfig:style_formats", jsonString);

            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
