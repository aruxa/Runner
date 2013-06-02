using System.Windows;
using System.Windows.Controls;

namespace runner.Controls
{
    public partial class MapControl : UserControl
    {
        private readonly VisualStates _visualStates;

        public MapControl()
        {
            InitializeComponent();
        }

         #region Visual States
        /// <summary>
        /// An internal helper class for handling MainPage visual state transitions.
        /// </summary>
        private class VisualStates
        {
            #region Predefined Visual States
            // Route States
            public const string ShowRoute = "ShowRoute";
            public const string HideRoute = "HideRoute";
            
            // Directions States
            public const string ShowDirections = "ShowDirections";
            public const string HideDirections = "HideDirections";

            // Pushpins States
            public const string ShowPushpins = "ShowPushpins";
            public const string HidePushpins = "HidePushpins";
            #endregion

            #region Fields
            private readonly Control _control;
            private string _routeState = HideRoute;
            private string _directionsState = HideDirections;
            private string _pushpinsState = HidePushpins;
            #endregion

            #region Properties
            /// <summary>
            /// Change the route panel visual state.
            /// </summary>
            public string RouteState
            {
                get
                {
                    return _routeState;
                }
                set
                {
                    if (_routeState != value)
                    {
                        _routeState = value;
                        VisualStateManager.GoToState(_control, value, true);
                    }
                }
            }

            /// <summary>
            /// Change the directions panel visual state.
            /// </summary>
            public string DirectionsState
            {
                get { return _directionsState; }
                set
                {
                    if (_directionsState != value)
                    {
                        _directionsState = value;
                        VisualStateManager.GoToState(_control, value, true);
                    }
                }
            }

            /// <summary>
            /// Change the pushpins popup visual state.
            /// </summary>
            public string PushpinsState
            {
                get { return _pushpinsState; }
                set
                {
                    if (_pushpinsState != value)
                    {
                        _pushpinsState = value;
                        VisualStateManager.GoToState(_control, value, true);
                    }
                }
            }
            #endregion

            #region Ctor

            public VisualStates(Control control)
            {
                _control = control;
            }

            #endregion
        }
        #endregion
    }

}