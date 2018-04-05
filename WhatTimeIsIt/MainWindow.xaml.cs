using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WhatTimeIsIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Variables
        public static System.Timers.Timer timer = new System.Timers.Timer();
        private int currentTime = 1;
        private float arcValue;
        private bool isDay;
        private bool isPopUp = false;
        private Storyboard sb;
        private Storyboard sb2;
        public Window1 popup;
        public int lengthNight = 600, lengthCycle = 3600;
        public double lengthNightAngle = 60, lengthCycleAngle=360;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            sb = FindResource("Explode") as Storyboard;
            sb2 = FindResource("RebuildRing") as Storyboard;

            // Timer 
            timer.Enabled = false;
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(OnTick);

            // Text Boxes
            textNextDay.Visibility = Visibility.Hidden;
            textDay.Visibility = Visibility.Hidden;
            textTime.Visibility = Visibility.Hidden;
            textBlock.Visibility = Visibility.Hidden;

            // Arc
            progressArcNight.Visibility = Visibility.Hidden;
            //arc.Visibility = Visibility.Hidden;

            // Buttons
            buttonSunset.Click += ButtonHandler;
            buttonSunrise.Click += ButtonHandler;
            buttonMidnight.Click += ButtonHandler;
            buttonMidday.Click += ButtonHandler;

            buttonSunset.Visibility = Visibility.Visible;
            buttonSunrise.Visibility = Visibility.Visible;
            buttonMidnight.Visibility = Visibility.Visible;
            buttonMidday.Visibility = Visibility.Visible;

            buttonReset.Visibility = Visibility.Hidden;
            buttonAdd.Visibility = Visibility.Hidden;
            buttonMinus.Visibility = Visibility.Hidden;
            buttonShow.Visibility = Visibility.Hidden;
        }

        private void OnTick(object source, ElapsedEventArgs e)
        {
            currentTime--;
            if (currentTime < 1)
            {
                Dispatcher.Invoke(() => sb2.Begin());
                Dispatcher.Invoke(() => sb.Begin());
                currentTime = lengthCycle;
            }
            else
            {
                Dispatcher.Invoke(() => sb2.Stop());
            }

            if (currentTime == lengthNight)
                Dispatcher.Invoke(() => sb.Begin());

            
            Console.WriteLine(currentTime);
            Dispatcher.Invoke(() => Refresh());
        }

        private void Refresh()
        {
            if (currentTime > lengthNight)
            {
                isDay = true;
            }
            else
            {
                isDay = false;
            }

            if (isDay)
            {
                arcValue = ((float)(currentTime) / lengthCycle) * 360;
                Console.WriteLine("Arc: " + arcValue);
                progressArc.EndAngle = arcValue;

                if (lengthCycle > 3600)
                    textTime.Text = TimeSpan.FromSeconds(currentTime - lengthNight).ToString(@"h\:mm\:ss");
                else
                    textTime.Text = TimeSpan.FromSeconds(currentTime - lengthNight).ToString(@"m\:ss");

                textDay.Text = "Day";
                progressArcNight.EndAngle = lengthNightAngle;
                progressArc.Visibility = Visibility.Visible;

                if (popup != null)
                {
                    popup.progressArcPop.EndAngle = arcValue;

                    if (lengthCycle > 3600)
                        popup.textTimePop.Text = TimeSpan.FromSeconds(currentTime - lengthNight).ToString(@"h\:mm\:ss");
                    else
                        popup.textTimePop.Text = TimeSpan.FromSeconds(currentTime - lengthNight).ToString(@"m\:ss");

                    popup.textDayPop.Text = "DAY";
                    popup.progressArcNightPop.EndAngle = lengthNightAngle;
                    popup.progressArcPop.Visibility = Visibility.Visible;
                }
            }
            else
            {
                progressArcNight.EndAngle = ((float)(currentTime) / lengthCycle) * 360;

                if (lengthCycle > 3600)
                    textTime.Text = TimeSpan.FromSeconds(currentTime).ToString(@"h\:mm\:ss");
                else
                    textTime.Text = TimeSpan.FromSeconds(currentTime).ToString(@"m\:ss");

                textDay.Text = "Night";

                progressArc.Visibility = Visibility.Hidden;

                if (popup != null)
                {
                    popup.progressArcNightPop.EndAngle = ((float)(currentTime) / lengthCycle) * 360;
                    if (lengthCycle > 3600)
                        popup.textTimePop.Text = TimeSpan.FromSeconds(currentTime).ToString(@"h\:mm\:ss");
                    else
                        popup.textTimePop.Text = TimeSpan.FromSeconds(currentTime).ToString(@"m\:ss");

                    popup.textDayPop.Text = "NIGHT";
                    popup.progressArcPop.Visibility = Visibility.Hidden;
                }
            }
        }

        public void RefreshTime(int newNight, int newCycle)
        {
            lengthNight = newNight;
            lengthCycle = newCycle;
            lengthNightAngle = ((float)lengthNight / lengthCycle) * 360;

        }

        public void ButtonHandler(object sender, EventArgs e)
        {
            Button button = sender as Button;
            switch (button.Uid)
            {
                case "0":
                    //Sunset
                    currentTime = lengthNight;
                    isDay = false;
                    break;

                case "1":
                    // Midnight
                    currentTime = lengthNight/2;
                    isDay = false;
                    break;

                case "2":
                    //Sunrise
                    currentTime = lengthCycle;
                    isDay = true;
                    break;

                case "3":
                    // Midday
                    currentTime = (lengthCycle / 2) + (lengthNight / 2);
                    isDay = true;
                    break;
            }

            Refresh();
            timer.Enabled = true;
            
            HideSelection();
        }

        private void ShowSelection()
        {
            // Time status
            textNextDay.Visibility = Visibility.Hidden;
            //textNextTime.Visibility = Visibility.Hidden;
            buttonReset.Visibility = Visibility.Hidden;
            buttonAdd.Visibility = Visibility.Hidden;
            buttonMinus.Visibility = Visibility.Hidden;
            buttonShow.Visibility = Visibility.Hidden;

            textDay.Visibility = Visibility.Hidden;
            textTime.Visibility = Visibility.Hidden;
            textBlock.Visibility = Visibility.Hidden;


            // Progress Arc
            progressArcNight.Visibility = Visibility.Hidden;

            // Time set
            textSetTime.Visibility = Visibility.Visible;
            buttonSunset.Visibility = Visibility.Visible;
            buttonSunrise.Visibility = Visibility.Visible;
            buttonMidnight.Visibility = Visibility.Visible;
            buttonMidday.Visibility = Visibility.Visible;
            buttonSetCycle.Visibility = Visibility.Visible;
        }

        private void HideSelection()
        {
            // Time status
            textNextDay.Visibility = Visibility.Visible;
            //textNextTime.Visibility = Visibility.Visible;
            buttonReset.Visibility = Visibility.Visible;
            buttonAdd.Visibility = Visibility.Visible;
            buttonMinus.Visibility = Visibility.Visible;
            buttonShow.Visibility = Visibility.Visible;

            textDay.Visibility = Visibility.Visible;
            textTime.Visibility = Visibility.Visible;
            textBlock.Visibility = Visibility.Visible;

            // Progress Arc
            progressArcNight.Visibility = Visibility.Visible;
            
            // Time set
            textSetTime.Visibility = Visibility.Hidden;
            buttonSunset.Visibility = Visibility.Hidden;
            buttonSunrise.Visibility = Visibility.Hidden;
            buttonMidnight.Visibility = Visibility.Hidden;
            buttonMidday.Visibility = Visibility.Hidden;
            buttonSetCycle.Visibility = Visibility.Hidden;
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            progressArc.StartAngle = 0;
            progressArc.EndAngle = 360;
            timer.Stop();
            ShowSelection();
            progressArc.Visibility = Visibility.Visible;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            currentTime -= 15;
            Refresh();
        }

        private void buttonMinus_Click(object sender, RoutedEventArgs e)
        {
            currentTime += 15;
            Refresh();
        }

        private void buttonShow_Click(object sender, RoutedEventArgs e)
        {
            if (popup == null)
            {
                popup = new Window1();
            }
            if (isPopUp)
            {
                isPopUp = false;
                buttonShow.Content = "Pop Out";
                popup.Hide();
            }
            else
            {
                isPopUp = true;
                buttonShow.Content = "Hide";
                popup.Show();
            }

        }

        private void WhatTimeIsIt_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }

        private void WhatTimeIsIt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ButtonClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ButtonMini_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //this.WindowState = WindowState.Minimized;
            //SystemCommands.MinimizeWindow(this);
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.WindowState = WindowState.Minimized;
            this.WindowStyle = WindowStyle.None;
        }

        private void ButtonMini_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonMini.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void ButtonClose_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonClose.Fill = new SolidColorBrush(Color.FromRgb(255, 80, 80));
        }

        private void ButtonMini_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonMini.Fill = new SolidColorBrush(Color.FromRgb(25, 25, 25));
        }

        private void ButtonClose_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonClose.Fill = new SolidColorBrush(Color.FromRgb(25, 25, 25));
        }

        private void buttonSetCycle_Click(object sender, RoutedEventArgs e)
        {
            TimeWindow timeWin = new TimeWindow();
            timeWin.Top = this.Top + 250;
            timeWin.Left = this.Left + 250;
            timeWin.ShowDialog();

            if (timeWin.DialogResult.HasValue && timeWin.DialogResult.Value)
            {
                RefreshTime(timeWin.NewNight, timeWin.NewCycle);
            }
        }


    }
}
