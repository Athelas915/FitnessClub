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
        public static string getDatabaseUrl(int dev) //returns heroku database url stored in database-url.json. int dev: 0 for production, 1 for development.
        {
            string databaseUrl;

            dynamic jsonObj = JsonConvert.DeserializeObject(File.ReadAllText("Data/DAL/DatabaseURL/database-url.json"));

            if (dev == 1)
            {
                databaseUrl = jsonObj["DATABASE_URL"]["Development"];
            }
            else
            {
                databaseUrl = jsonObj["DATABASE_URL"]["Production"];
            }
            return databaseUrl;
        }
        public static string ConvertDbURL(string databaseUrl) //returns correct connection string when given databseUrl from Heroku
        {
            string[] urlList;

            urlList = databaseUrl.Split(new char[] { ':', '@', '/' });

            return "Host=" + urlList[5] + ";Database=" + urlList[7] + ";Username=" + urlList[3] + ";Password=" + urlList[4] + ";SSL Mode=Require;Trust Server Certificate=true";
        }

        public static void EditJson()
        {

            string json = File.ReadAllText("appsettings.Development.json");
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["ConnectionStrings"]["FCContext"] = ConvertDbURL(getDatabaseUrl(1));
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText("appsettings.Development.json", output);

            json = File.ReadAllText("appsettings.json");
            jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["ConnectionStrings"]["FCContext"] = ConvertDbURL(getDatabaseUrl(0));
            output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText("appsettings.json", output);
        }
    }
}
