using MockDataGenerator.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.Store
{
    public class ModalNavigationStore
    {
        public event Action CurrentModalViewChangedEvent;
        private BaseViewModel _currentModalView;

        public bool IsModalViewOpen
        {
            get { return CurrentModalView != null; }
            set 
            {
                if (value == false)
                    CurrentModalView = null;
            }
        }

        public BaseViewModel CurrentModalView
        {
            get { return _currentModalView; }
            set 
            { 
                _currentModalView = value;
                OnCurrentModalViewChanged();
            }
        }


        public void CloseModalView()
        {
            CurrentModalView = null;
        }
        private void OnCurrentModalViewChanged()
        {
            CurrentModalViewChangedEvent?.Invoke();
        }
    }
}
