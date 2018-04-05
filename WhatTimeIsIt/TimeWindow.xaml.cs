using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace WhatTimeIsIt
{
    /// <summary>
    /// Interaction logic for TimeWindow.xaml
    /// </summary>
    public partial class TimeWindow : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        //MainWindow win = new MainWindow();

        public int NewNight { get; set; }
        public int NewCycle { get; set; }

        public TimeWindow()
        {
            InitializeComponent();
        }

        private void TimeDialog_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BlankCheck(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box.Text == "")
                box.Text = "00";
        }

        private void TimeDialog_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonApply_Click(object sender, RoutedEventArgs e)
        {
            NewNight = (Convert.ToInt16(InputHoursNight.Text) * 3600) + (Convert.ToInt16(InputMinutesNight.Text) * 60) + Convert.ToInt16(InputSecondsNight.Text);
            NewCycle = NewNight + (Convert.ToInt16(InputHoursDay.Text) * 3600) + (Convert.ToInt16(InputMinutesDay.Text) * 60) + Convert.ToInt16(InputSecondsDay.Text);

            DialogResult = true;
            this.Close();
            
        }
    }
}
