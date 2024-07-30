using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections;
using AccountLibrary;
using AccountLibrary.Model;


namespace AccountProjectWithList
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            AccountManager.account = SerialDesrial.DeserializeData();

            AccountManager.UserSelection();

            SerialDesrial.SerializeData(AccountManager.account);

        }

    }

}