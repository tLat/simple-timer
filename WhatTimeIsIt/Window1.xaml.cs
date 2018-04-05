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

namespace WhatTimeIsIt
{
    
    public partial class Window1 : Window
    {

        private bool resize = false;

        public Window1()
        {
            InitializeComponent();
        }

        private void PopUp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

            if (e.RightButton == MouseButtonState.Pressed)
            {
                textDayPop.FontFamily = new FontFamily("Roboto Condensed");
                textDayPop.Margin = new Thickness(0, 70, 0, 0);
                textDayPop.FontSize = 24;
                textDayPop.FontWeight = FontWeights.Bold;

                textTimePop.FontFamily = new FontFamily("Roboto Condensed");
                textTimePop.Margin = new Thickness(0, 100, 0, 0);
                textTimePop.FontSize = 24;
                textTimePop.FontWeight = FontWeights.Bold;


                this.Width = 125;
                this.Height = 125;

                resize = true;
            }
        }

        private void PopUp_Loaded(object sender, RoutedEventArgs e)
        {
            //EnableBlur();
        }

        private void PopUp_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //if (resize)
            //{
            //    // If the mouse wheel delta is positive, increase window size.
            //    if (e.Delta > 0)
            //    {
            //        this.Width += 2;
            //        this.Height += 2;
            //    }
            //
            //    // If the mouse wheel delta is negative, move the box down.
            //    if (e.Delta < 0)
            //    {
            //        this.Width -= 2;
            //        this.Height -= 2;
            //    }
            //}

        }
    }
}
