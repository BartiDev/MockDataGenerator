using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meziantou.Framework.Win32;
using MockDataGenerator.MVVM.Model;

namespace MockDataGenerator.Api.CredantialManager
{
    /** Class uses Meziantou.Framework.Win32.CredentialManager Nuget **/
    public class WindowsCredantialManager
    {
        public static void SaveCredantials(string username, string apiKey)
        {
            if (ReadCredantials() != null)
                CredentialManager.DeleteCredential("BartiMock");

            CredentialManager.WriteCredential(
                applicationName: "BartiMock",
                userName: username,
                secret: apiKey,
                persistence: CredentialPersistence.LocalMachine);
        }

        public static UserModel ReadCredantials()
        {
            Credential credential = CredentialManager.ReadCredential("BartiMock");

            if (credential != null)
            {
                return new UserModel()
                {
                    Username = credential.UserName,
                    ApiKey = credential.Password
                };
            }
            else
                return null;
        }

        public static void DeleteCredantials() => CredentialManager.DeleteCredential("BartiMock");
    }
}
