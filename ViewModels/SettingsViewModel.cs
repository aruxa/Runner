namespace runner
{
    public class SettingsViewModel : ViewModelBase
    {
        public double MovementThreshold
        {
            get
            {
                return SettingsHelper.Instance.MovementThreshhold;
            }
            set
            {
                SettingsHelper.Instance.MovementThreshhold = value;
                NotifyPropertyChanged(() => MovementThreshold);
            }
        }

        public bool IsMock
        {
            get
            {
                return SettingsHelper.Instance.IsMock;
            }
            set
            {
                SettingsHelper.Instance.IsMock = value;
                NotifyPropertyChanged(() => IsMock);
            }
        }
    }

    public enum GeoLocationType
    {
        Mock,
        Live
    }
}
