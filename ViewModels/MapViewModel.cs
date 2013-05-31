using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Controls.Maps.Platform;
using PhoneApp1;
using runner.Bing.Route;

namespace runner
{
    public class MapViewModel : ViewModelBase
    {
        private GeoCoordinate _center;
        private CredentialsProvider _credentialsProvider = new ApplicationIdCredentialsProvider(App.Id);
        private static readonly GeoCoordinate defaultLocation = new GeoCoordinate(44.44779, 26.081745);
        private ObservableCollection<Pushpin> pushpins;
        private string _speed;
        private double _zoom;
        private ObservableCollection<RouteModel> _routes;
        private ObservableCollection<ItineraryItem> _itineraries;
        private ObservableCollection<GeoCoordinate> _locations;
        private RouteModel _route;
        private const double DefaultZoomLevel = 18.0;
        private const double MaxZoomLevel = 21.0;
        private const double MinZoomLevel = 1.0;
        private bool _isRecording;
        private string _altitude;
        private double _distance;


        public double Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                NotifyPropertyChanged("Distance");
            }
        }

        public bool IsRecording
        {
            get { return _isRecording; }
            set
            {
                _isRecording = value;
                NotifyPropertyChanged("IsRecording");
            }
        }


        public double Zoom
        {
            get { return _zoom; }
            set
            {
                var coercedZoom = Math.Max(MinZoomLevel, Math.Min(MaxZoomLevel, value));
                if (_zoom != coercedZoom)
                {
                    _zoom = value;
                    NotifyPropertyChanged("Zoom");
                }
            }
        }

        public GeoCoordinate Center
        {
            get { return _center; }
            set
            {
                if (_center != value)
                {
                    _center = value;
                    NotifyPropertyChanged("Center");
                }
            }
        }

        public CredentialsProvider CredentialsProvider
        {
            get { return _credentialsProvider; }
        }

        public ObservableCollection<Pushpin> Pushpins
        {
            get { return pushpins; }
            set
            {
                pushpins = value;
                NotifyPropertyChanged("Pushpins");
            }
        }

        /// <summary>
        /// Gets or sets the route destination location.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the route origin location.
        /// </summary>
        public string From { get; set; }

        public ObservableCollection<RouteModel> Routes { get { return _routes; } }

        public RouteModel Route
        {
            get
            {
                return _route;
            }
            set
            {
                _route = value;
                NotifyPropertyChanged("Route");
            }
        }

        public ObservableCollection<GeoCoordinate> RouteLocations { get { return _locations; } }

        public ObservableCollection<ItineraryItem> Itineraries
        {
            get { return _itineraries; }
        }

        public ICommand FindRouteCommand { get; private set; }

        public ICommand StartTrackingCommand { get; private set; }

        public ICommand EndTrackingCommand { get; private set; }

        public GeoCoordinateWatcher Watcher { get; private set; }

        public GeoCoordinate YouAreHere
        {
            get
            {
                if (Watcher.Position.Location.IsUnknown)
                {
                    return defaultLocation;
                }
                return Watcher.Position.Location;
            }
        }

        public string Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                NotifyPropertyChanged("Speed");
            }
        }


        public string StringDistance
        {
            get
            {
                if (Distance < 999)
                {
                    return Distance.ToString() + " m";
                }
                else
                {
                    return (Distance / 1000).ToString() + " km";
                }
            }
        }

        public string Altitude
        {
            get { return _altitude; }
            set
            {
                _altitude = value;
                NotifyPropertyChanged("Altitude");
            }
        }

        void CalculateRoute(object o)
        {
            try
            {
                var routeCalculator = new RouteCalculator(
                    CredentialsProvider,
                    To,
                    From,
                    Deployment.Current.Dispatcher,
                    result =>
                    {
                        // Clear the route collection to have only one route at a time.
                        //Routes.Clear();

                        //// Create a new route based on route calculator result, and add the new route to the route collection.
                        //var routeModel = new RouteModel(result.Result.RoutePath.Points);
                        //Routes.Add(routeModel);
                        //Route = routeModel;

                        //// Add new route itineraries to the itineraries collection.
                        //foreach (var itineraryItem in result.Result.Legs[0].Itinerary)
                        //{
                        //    Itineraries.Add(itineraryItem);
                        //}
                        NotifyPropertyChanged("Routes");
                    });

                routeCalculator.Error += r => MessageBox.Show(r.Reason);

                // Start the route calculation asynchronously.
                routeCalculator.CalculateAsync();
            }
            catch
            {

            }
        }


        private void WatcherOnStatusChanged(object sender, GeoPositionStatusChangedEventArgs geoPositionStatusChangedEventArgs)
        {

        }

        public MapViewModel()
        {
            Watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);

            if (Watcher.Permission == GeoPositionPermission.Granted)
            {

                Watcher.MovementThreshold = double.IsNaN(SettingsHelper.Instance.MovementThreshhold) || (SettingsHelper.Instance.MovementThreshhold == 0) ? 1 : SettingsHelper.Instance.MovementThreshhold;
            }
            Watcher.Position.Location.HorizontalAccuracy = 1;
            Watcher.PositionChanged += WatcherOnPositionChanged;
            Watcher.StatusChanged += WatcherOnStatusChanged;
            Watcher.Start();

            InitializeMapViewModelProperties();

            pushpins.Add(new Pushpin { Location = YouAreHere });
        }

        void InitializeMapViewModelProperties()
        {
            FindRouteCommand = new Command(CalculateRoute);
            StartTrackingCommand = new Command(StartWatcher, canExecute => IsRecording == false);
            EndTrackingCommand = new Command(StopWatcher, canExecute => IsRecording);

            _locations = new ObservableCollection<GeoCoordinate>();
            _itineraries = new ObservableCollection<ItineraryItem>();
            _routes = new ObservableCollection<RouteModel>();

            Center = YouAreHere;
            Zoom = DefaultZoomLevel;
            pushpins = new ObservableCollection<Pushpin>(new List<Pushpin>());
            IsRecording = false;
        }

        private void WatcherOnPositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> eventArgs)
        {
            if (eventArgs.Position == null || eventArgs.Position.Location == null || eventArgs.Position.Location.IsUnknown)
            {
                return;
            }
            var currentLoc = eventArgs.Position.Location;
            Deployment.Current.Dispatcher.BeginInvoke(() => UpdateMapElements(currentLoc));
        }

        void UpdateMapElements(GeoCoordinate currentLoc)
        {
            Center = currentLoc;
            Pushpins = new ObservableCollection<Pushpin>(new List<Pushpin> { new Pushpin { Location = currentLoc } });
            Speed = (currentLoc.Speed * 3.6).ToString();
            Altitude = currentLoc.Altitude.ToString();

            if (IsRecording == false)
            {
                return;
            }


            Routes.Clear();
            RouteLocations.Add(currentLoc);

            if (RouteLocations.Count - 2 >= 0)
            {
                Distance = Distance + RouteLocations[RouteLocations.Count - 2].GetDistanceTo(currentLoc);
            }
            var routeModel = new RouteModel(RouteLocations);
            Routes.Add(routeModel);
            Route = routeModel;

            NotifyPropertyChanged("Routes");
        }


        void StartWatcher(object o)
        {
            if (Watcher == null)
            {
                return;
            }
            Routes.Clear();

            Distance = 0;
            Route = new RouteModel(new ObservableCollection<GeoCoordinate>());
            IsRecording = true;
        }

        private void StopWatcher(object obj)
        {
            if (Watcher != null)
            {
                IsRecording = false;
            }
            SettingsHelper.Instance.MovementThreshhold = Watcher.MovementThreshold;
        }
    }
}
