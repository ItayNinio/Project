using DataBaseProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixel_Rambo.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LeaderboardPage : Page
    {
        public LeaderboardPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            leaderboardList.SelectionMode = ListViewSelectionMode.None;
            List<KeyValuePair<string, int>> topUsers = Server.GetTop5RichestUsers();

            leaderboardList.Items.Clear();

            for (int i = 0; i < topUsers.Count; i++)
            {
                string username = topUsers[i].Key;
                int money = topUsers[i].Value;

                var item = new ListViewItem
                {
                    Content = $"{username} - ${money:N0}", // format with thousands separator
                    FontSize = 24,
                    FontWeight = Windows.UI.Text.FontWeights.Bold,
                    HorizontalContentAlignment = HorizontalAlignment.Center
                };

                // Set color by rank
                switch (i)
                {
                    case 0:
                        item.Foreground = new SolidColorBrush(Windows.UI.Colors.Gold);
                        break;
                    case 1:
                        item.Foreground = new SolidColorBrush(Windows.UI.Colors.Silver);
                        break;
                    case 2:
                        item.Foreground = new SolidColorBrush(Windows.UI.Colors.Peru); // bronze-like color
                        break;
                    default:
                        item.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                        break;
                }

                leaderboardList.Items.Add(item);
            }
        }


        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }
    }
}
