using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Parse;

namespace QuotesApp
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemTray.IsVisible = false;
        }

        private async void loginButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (usernameTextBox.Text != "" && passwordTextBox.Password != "")
            {
                loginButton.IsHitTestVisible = false;
                loginButton.Text = "";
                progressBar.Visibility = System.Windows.Visibility.Visible;
                try
                {
                    ParseUser user;
                    user = await ParseUser.LogInAsync(usernameTextBox.Text, passwordTextBox.Password);
                    if ((bool)user["emailVerified"] == true)
                    {
                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("We need to make sure you actually exist. Please verify your email!");
                    }
                    progressBar.Visibility = System.Windows.Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    progressBar.Visibility = System.Windows.Visibility.Collapsed;
                    if (ex.Message == "invalid login parameters")
                    {
                        MessageBox.Show("Something's up...\nLooks like you entered the wrong info");
                    }
                    loginButton.IsHitTestVisible = true;
                    loginButton.Text = "Login";
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private void signUpButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SignUpPage.xaml", UriKind.Relative));
        }
    }
}