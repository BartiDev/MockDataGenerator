using MockDataGenerator.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.MVVM.ViewModel
{
    public class WelcomeViewModel
    {
        public RelayCommand GoToMockarooCommand { get; set; }
        public WelcomeViewModel()
        {
            GoToMockarooCommand = new RelayCommand(o =>
            {
                string link = o.ToString();

                var prs = new ProcessStartInfo("explorer.exe");
                prs.Arguments = link;
                prs.UseShellExecute = true;
                Process.Start(prs);
            });
        }
    }
}
