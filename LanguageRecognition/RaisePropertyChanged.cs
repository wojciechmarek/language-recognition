using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LanguageRecognition
{
    public class RaisePropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseChange([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void SetValue<T>(ref T back, T value, [CallerMemberName] string name = null)
        {
            if (EqualityComparer<T>.Default.Equals(back, value))
            {
                return;
            }
            else
            {
                back = value;
                RaiseChange(name);
            }
        }
    }
}
