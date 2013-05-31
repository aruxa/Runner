using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using runner.Controls;

namespace runner
{
    public class SettingsViewModel : ViewModelBase
    {
        public double MovementThreshold
        {
            get { return SettingsHelper.Instance.MovementThreshhold; }
            set
            {
                SettingsHelper.Instance.MovementThreshhold = value;
                NotifyPropertyChanged("MovementThreshold");
            }
        }
    }
}
