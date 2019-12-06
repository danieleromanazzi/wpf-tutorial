using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wpf_challenge_itemscontrol.Model
{
    public class Settings : List<Setting>
    {
        internal static Settings LoadFromResource()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("wpf_challenge_itemscontrol.Resources.Source.json");
            using (StreamReader sr = new StreamReader(stream))
            {
                JsonSerializer serializer = new JsonSerializer();
                Settings settings = (Settings)serializer.Deserialize(sr, typeof(Settings));
                return settings;
            }
        }
    }
}
