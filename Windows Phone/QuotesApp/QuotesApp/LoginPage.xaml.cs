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

            //if (ParseUser.CurrentUser != null)
            //{
            //    if ((bool)ParseUser.CurrentUser["emailVerified"] == false)
            //    {
            //        // User must verify email
            //    }
            //    else
            //    {
            //        NavigationService.Navigate(new Uri("/RoomsListPage.xaml", UriKind.Relative));
            //    }
            //}
        }

        private async void loginButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (emailTextBox.Text != "" && passwordTextBox.Password != "")
            {
                loginButton.IsHitTestVisible = false;
                loginButton.Text = "";
                progressBar.Visibility = System.Windows.Visibility.Visible;
                try
                {
                    ParseUser user;
                    user = await ParseUser.LogInAsync(emailTextBox.Text, passwordTextBox.Password);
                    AppConstants.user = user;
                    if ((bool)user["emailVerified"] == true || user.CreatedAt + TimeSpan.FromDays(1) > DateTime.UtcNow)
                    {
                        if (user.CreatedAt + TimeSpan.FromDays(1) > DateTime.UtcNow && (bool)user["emailVerified"] == false)
                        {
                            MessageBox.Show("Note you have to verify your email within 24 hours of signing up or you will not be able to login.");
                        }
                        NavigationService.Navigate(new Uri("/RoomsListPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("We need to make sure you actually exist. Please verify your email!");
                        loginButton.IsHitTestVisible = true;
                        loginButton.Text = "Login";
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
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }

                    loginButton.IsHitTestVisible = true;
                    loginButton.Text = "Login";
                }
            }
        }

        private void signUpButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SignUpPage.xaml", UriKind.Relative));
        }
    }
}