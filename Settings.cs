using System;
using System.IO.IsolatedStorage;

namespace runner
{
    public class SettingsHelper
    {
        double defaultMovementThreshold = 1;

        public double MovementThreshhold
        {
            get { return GetValueOrDefault("MovementThreshold", defaultMovementThreshold); }
            set
            {
                if (AddOrUpdateValue("MovementThreshold", value))
                {
                    Save();
                }
            }
        }

        public double HorizontalAccuracy { get; set; }

        private static SettingsHelper _instance = new SettingsHelper();

        public static SettingsHelper Instance
        {
            get { return _instance; }
        }

        static SettingsHelper()
        {
        }

        void Save()
        {
            Settings.Save();
        }

        private IsolatedStorageSettings Settings { get; set; }

        private SettingsHelper()
        {
            Settings = IsolatedStorageSettings.ApplicationSettings;
        }

        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (Settings.Contains(Key))
            {
                // If the value has changed
                if (Settings[Key] != value)
                {
                    // Store the new value
                    Settings[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                Settings.Add(Key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;
            if (Settings.Contains(Key))
            {
                value = (T)Settings[Key];
            }
            else
            {
                value = defaultValue;
            }
            return value;
        }
    }
}
