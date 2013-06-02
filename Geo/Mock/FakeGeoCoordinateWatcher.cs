using System;
using System.Device.Location;

namespace runner.Geo.Mock
{
    public class FakeGeoCoordinateWatcher : IGeoCoordinateWatcher
    {
        public FakeGeoCoordinateWatcher(GeoPositionAccuracy accuracy)
        {

        }

        //public  void Start()
        //{
        //    OnPositionStatusChanged(new GeoPositionStatusChangedEventArgs(GeoPositionStatus.Ready));

        //    OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.448074, 26.081837))));
        //    OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.447959, 26.082315))));
        //    OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.447924, 26.082518))));
        //    OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.448231, 26.082679))));
        //    OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.450681, 26.084096))));   
        //    OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.451187, 26.084364))));
        //}

        #region Implementation of IGeoPositionWatcher<GeoCoordinate>

        public void Start()
        {
            Start(false);
        }

        public void Start(bool suppressPermissionPrompt)
        {
            Status = GeoPositionStatus.Ready;
        }

        public bool TryStart(bool suppressPermissionPrompt, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {

        }

        public GeoPosition<GeoCoordinate> Position { get; private set; }
        public GeoPositionStatus Status { get; private set; }
        public event EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>> PositionChanged;
        public event EventHandler<GeoPositionStatusChangedEventArgs> StatusChanged;

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {

        }

        #endregion

        #region Implementation of IGeoCoordinateWatcher

        public GeoPositionAccuracy DesiredAccuracy { get; private set; }
        public double MovementThreshold { get; set; }
        public GeoPositionPermission Permission { get; private set; }

        #endregion

        public void ChangePosition(DateTimeOffset offset, GeoCoordinate coordinate)
        {
            Position = new GeoPosition<GeoCoordinate>(offset, coordinate);
            if (PositionChanged != null)
            {
                PositionChanged(null, new GeoPositionChangedEventArgs<GeoCoordinate>(Position));
            }
        }
    }
}
