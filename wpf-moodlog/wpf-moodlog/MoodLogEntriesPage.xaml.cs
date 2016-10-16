﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_moodlog
{
    /// <summary>
    /// Interaction logic for MoodLogEntriesPage.xaml
    /// </summary>
    public partial class MoodLogEntriesPage : Page
    {
        public static DateTime Today { get; }

        public MoodLogEntriesPage()
        {
            InitializeComponent();

            setDateTodayLabel();
            // loadPreviousEntries();
        }

        private void setDateTodayLabel()
        {
            // Get the current date.
            DateTime thisDay = DateTime.Today;

            dateTodayLabel.Content = thisDay.ToString("MMM d");
        }


        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            // View Profile page
            MoodLogProfilePage moodLogProfilePage = new MoodLogProfilePage();
            this.NavigationService.Navigate(moodLogProfilePage);
        }

        private void hashtagButton_Click(object sender, RoutedEventArgs e)
        {
            entryTextBox.Text += "#";
        }
    }
}
