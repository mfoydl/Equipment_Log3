using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment_Log2 {
    class JsonBuilder {
        public static void Save(List<Log> logs) {


            File.WriteAllText(@"c:\Users\Public\equipment_log.json", JsonConvert.SerializeObject(logs));
            /*
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(@"c:\Users\Public\equipment_log.json")) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, logs);
            }
            */
        }

        public static List<Log> Load() {
            return JsonConvert.DeserializeObject<List<Log>>(File.ReadAllText(@"c:\Users\Public\equipment_log.json"));
        }
    }
}
