using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub;
using Newtonsoft.Json;

namespace FitnessClub.Data.DAL
{
    //Class that has methods to convert DATABASE_URL variable from Heroku app to ConnectionStrings in appsettings.json.
    public static class GetConnectionString
    {
        public static string ConvertDbURL()
        {
            string[] urlList;
            string json;
            string databaseUrl;

            json = File.ReadAllText("Data/DAL/DatabaseURL/database-url.json");
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            databaseUrl = jsonObj["DATABASE_URL"];

            urlList = databaseUrl.Split(new char[] { ':', '@', '/' });

            return "Host=" + urlList[5] + ";Database=" + urlList[7] + ";Username=" + urlList[3] + ";Password=" + urlList[4] + ";SSL Mode=Require;Trust Server Certificate=true";
        }
    
        public static void EditJson()
        {
            string json = File.ReadAllText("appsettings.json");
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["ConnectionStrings"]["FCContext"] = ConvertDbURL();
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText("appsettings.json", output);
        }
    }
}
