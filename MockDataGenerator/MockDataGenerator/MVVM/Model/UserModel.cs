using MockDataGenerator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.MVVM.Model
{
    public class UserModel : ObservableObject
    {
        public string Username { get; set; }
        public string ApiKey { get; set; }
        public bool RememberMe { get; set; }
    }
}
