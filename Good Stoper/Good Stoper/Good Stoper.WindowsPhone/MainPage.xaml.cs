using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Szablon elementu Pusta strona jest udokumentowany pod adresem http://go.microsoft.com/fwlink/?LinkId=234238

namespace Good_Stoper
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie, lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int miliseconds = 0;
        int seconds = 0;
        int minutes = 0;

        public MainPage()
        {
            this.InitializeComponent();
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            this.NavigationCacheMode = NavigationCacheMode.Required;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 79);
            dispatcherTimer.Tick += dispatcherTimer_Tick;

            string contentOfBtn = (btnStartStop.Content ?? String.Empty).ToString();
            if (string.Equals("Stop", contentOfBtn, StringComparison.OrdinalIgnoreCase))
            {
                resetBtn.IsEnabled = false;
            }
            else
            {
                lapBtn.IsEnabled = false;
            }
            

            /*listView.Items.Add(new Laps(2));
            listView.Items.Add(new Laps(4));
            listView.Items.Add(new Laps(6));*/
            //listView.ItemsSource = listView.Items;
        }
        
        private void dispatcherTimer_Tick(object sender, object e)
        {
            if (miliseconds > 9)
            {
                miliseconds = 0;
                seconds++;
            }

            if (seconds > 59)
            {
                seconds = 0;
                minutes++;
            }
            displayTime.Text = minutes.ToString("00") + ":" + seconds.ToString("00") + "." + miliseconds++.ToString();
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            displayTime.Text = "00:00.0";
            miliseconds = 0;
            seconds = 0;
            minutes = 0;
            listView.Items.Clear();
            Laps laps = new Laps();
            laps.startNextIdFrom1();
        }

        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
            string contentOfBtn = (btnStartStop.Content ?? String.Empty).ToString();
            if (string.Equals("Start", contentOfBtn, StringComparison.OrdinalIgnoreCase))
            {
                dispatcherTimer.Start();
                btnStartStop.Content = "Stop";
                resetBtn.IsEnabled = false;
                lapBtn.IsEnabled = true;
            }

            if (string.Equals("Stop", contentOfBtn, StringComparison.OrdinalIgnoreCase))
            {
                dispatcherTimer.Stop();
                btnStartStop.Content = "Start";
                resetBtn.IsEnabled = true;
                lapBtn.IsEnabled = false;
            }
        }

        private void lapBtn_Click(object sender, RoutedEventArgs e)
        {
            listView.Items.Add(new Laps(displayTime.Text));
       }
        /// <summary>
        /// Wywoływane, gdy ta strona ma być wyświetlona w ramce.
        /// </summary>
        /// <param name="e">Dane zdarzenia, opisujące, jak została osiągnięta ta strona.
        /// Ten parametr jest zazwyczaj używany do konfigurowania strony.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Tutaj przygotuj stronę do wyświetlenia.

            // TODO: Jeśli aplikacja zawiera wiele stron, upewnij się, że jest
            // obsługiwany przycisk Wstecz sprzętu, rejestrując
            // zdarzenie Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Jeśli używasz obiektu NavigationHelper dostarczanego w niektórych szablonach,
            // to zdarzenie jest już obsługiwane.
        }
    }

    public class Laps
    {
        private static int nextId = 0;
        public int Id { get; private set; }
        public string Lap { get; set; }

        public Laps(string lap)
        {
            this.Lap = lap;
            nextId++;
            Id = nextId;
        }

        public Laps()
        {

        }

        public void startNextIdFrom1()
        {
            nextId = 0;
        }
    }
}