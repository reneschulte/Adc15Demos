using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Microsoft.Band;
using Microsoft.Band.Sensors;

namespace Win10DxSample
{
    public sealed partial class MainPage
    {
        private bool _continue;

        private const double Pi = Math.PI;
        private const double PiHalf = Pi * 0.5;

        private double _roll;
        private double _pitch;

        public MainPage()
        {
            InitializeComponent();

            Loaded += async (s, e) =>
            {
                await CheckOptions();
                DxPanel.Scale = 0.8;
            };
        }

        private double NormalizeRoll(double roll)
        {
            if ((_pitch < Pi && _pitch >= PiHalf - 0.2)
                || (_pitch > Pi && _pitch <= 3 * PiHalf + 0.2))
            {
                return _roll;
            }

            _roll = roll + PiHalf;

            // Constrain value to avoid side effects
            if (_roll < -PiHalf + 0.2
                || _roll > Pi)
            {
                _roll = -PiHalf + 0.2;
            }
            else
            {
                if (_roll > PiHalf - 0.2)
                {
                    _roll = PiHalf - 0.2;
                }
            }

            return _roll;
        }

        private double NormalizePitch(double pitch)
        {
            if (_roll >= PiHalf - 0.2
                || _roll <= -PiHalf + 0.2)
            {
                return _pitch;
            }

            _pitch = pitch + Pi;

            // Constrain value to avoid side effects
            if (_pitch > Pi
                && _pitch < 3 * PiHalf + 0.2)
            {
                _pitch = 3 * PiHalf + 0.2;
            }
            else
            {
                if (_pitch > PiHalf - 0.2
                    && _pitch < Pi)
                {
                    _pitch = PiHalf - 0.2;
                }
            }

            return _pitch;
        }

        private double NormalizeAngle(double originalAngle)
        {
            // Constrain the pitch between -90 and 90 degrees
            // (90 degrees == Pi / 2 radians)

            return Math.Max(
                -PiHalf,
                Math.Min(
                    PiHalf,
                    originalAngle));
        }

        #region Options

        private async void OptionChanged(object sender, RoutedEventArgs e)
        {
            await CheckOptions();
        }

        private async Task CheckOptions()
        {
            if (Root == null)
            {
                return;
            }

            if (BandRadioButton.IsChecked != null
                && BandRadioButton.IsChecked.Value)
            {
                Root.ManipulationDelta -= RootManipulationDelta;
                Root.ManipulationMode = ManipulationModes.None;

                await InitializeBand();
            }
            else
            {
                if (TouchRadioButton.IsChecked != null
                    && TouchRadioButton.IsChecked.Value)
                {
                    _continue = false; // Stop listening to Band
                    Root.ManipulationMode = ManipulationModes.All;
                    Root.ManipulationDelta += RootManipulationDelta;
                    StatusText.Text = "Tap & hold to rotate. Pinch to zoom.";
                }
            }
        }

        #endregion
    }
}
