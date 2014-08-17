using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Parse;

namespace QuotesApp
{
    public partial class SignUpPage : PhoneApplicationPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void createButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (passwordTextBox.Password == doublePasswordTextBox.Password)
            {
                if (passwordTextBox.Password.Length < 6)
                {
                    MessageBox.Show("Hold up, wait a minute, your password is way too short (must be 6 characters or greater)");
                }
                createButton.IsHitTestVisible = false;
                createButton.Text = "";
                progressBar.Visibility = System.Windows.Visibility.Visible;
                ParseUser newUser = new ParseUser();
                newUser.Email = emailTextBox.Text;
                newUser.Username = usernameTextBox.Text;
                newUser["firstname"] = firstNameTextBox.Text;
                newUser["lastname"] = lastNameTextBox.Text;
                newUser.Password = passwordTextBox.Password;

                try
                {
                    progressBar.Visibility = System.Windows.Visibility.Collapsed;
                    await newUser.SignUpAsync();
                    NavigationService.Navigate(new Uri("/LoginPage.xaml", UriKind.Relative));

                }
                catch (Exception ex)
                {
                    createButton.IsHitTestVisible = true;
                    createButton.Text = "OK";
                    progressBar.Visibility = System.Windows.Visibility.Collapsed;

                    if (ex.Message.Contains("the email address"))
                    {
                        MessageBox.Show("Look's like somebody else is using that email address...");
                    }
                    else if (ex.Message.Contains("invalid email"))
                    {
                        MessageBox.Show("That's not even an email address!");
                    }
                    else if (ex.Message == "username " + usernameTextBox.Text + " already taken")
                    {
                        Random random = new Random();
                        int randomNumber = random.Next(0, 2);
                        if (randomNumber == 0)
                        {
                            MessageBox.Show("Too bad so sad that username is taken ;_;");
                        }
                        else
                        {
                            MessageBox.Show("Too bad so sad that username is taken\n¯\\_(ツ)_/¯");
                        }
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void firstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            createButton.Text = "collecting names";

            if (firstNameTextBox.Text != "")
            {
                lastNameTextBox.IsEnabled = true;
            }
            else
            {
                lastNameTextBox.IsEnabled = false;
            }
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            createButton.Text = "that's your email?";
        }

        private void usernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            createButton.Text = "best username ever";
        }

        private void passwordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            createButton.Text = "almost there";
        }

        private void doublePasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            createButton.Text = "OK";
        }
    }
}