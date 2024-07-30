using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountLibrary.Model
{
    public class SerialDesrial
    {
        public static void SerializeData(List<Account> accounts)
        {
            File.WriteAllText("AccountData.json", JsonConvert.SerializeObject(accounts));
        }

        public static List<Account> DeserializeData()
        {
            List<Account> accounts = new List<Account>();
            string fileName = "AccountData.json";
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                accounts = JsonConvert.DeserializeObject<List<Account>>(json);
            }
            else
            {
                File.WriteAllText(fileName, JsonConvert.SerializeObject(accounts));
            }
            return accounts;
        }
    }
}
