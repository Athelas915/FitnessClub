using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub;
using Newtonsoft.Json;

namespace FitnessClub.Data.DAL
{
    //Because Heroku provides database credentials in "Database_URL" form, this class was made to convert that into ConnectionString that can be used by .NET app. 
    public static class GetConnectionString
    {
        public static string GetDatabaseUrl(int dev) //reads heroku database url stored in database-url.json and returns it as string. int dev: 0 for production, 1 for development.
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
        public static string ConvertDbURL(string databaseUrl) //converts Heroku Database_URL into ConnectionString
        {
            string[] urlList;

            urlList = databaseUrl.Split(new char[] { ':', '@', '/' });

            return "Host=" + urlList[5] + ";Database=" + urlList[7] + ";Username=" + urlList[3] + ";Password=" + urlList[4] + ";SSL Mode=Require;Trust Server Certificate=true";
        }

        public static void EditJson() //Edits appsettings.json and appsettings.Development.json, inserting new connection strings.
        {

            string json = File.ReadAllText("appsettings.Development.json");
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["ConnectionStrings"]["FCContext"] = ConvertDbURL(GetDatabaseUrl(1));
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText("appsettings.Development.json", output);

            json = File.ReadAllText("appsettings.json");
            jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj["ConnectionStrings"]["FCContext"] = ConvertDbURL(GetDatabaseUrl(0));
            output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText("appsettings.json", output);
        }
    }
}
