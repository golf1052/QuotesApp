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
            if (displayNameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("You have to have a display name. Sorry it's one of the few rules we have here.");
                return;
            }

            if (passwordTextBox.Password == doublePasswordTextBox.Password)
            {
                if (passwordTextBox.Password.Length < 6)
                {
                    MessageBox.Show("Hold up, wait a minute, your password is way too short (must be 6 characters or greater)");
                    return;
                }

                createButton.IsHitTestVisible = false;
                createButton.Text = "";
                progressBar.Visibility = System.Windows.Visibility.Visible;
                ParseUser newUser = new ParseUser();
                newUser.Email = emailTextBox.Text;
                newUser.Username = emailTextBox.Text;
                newUser["displayName"] = displayNameTextBox.Text;
                newUser["favorites"] = new List<ParseObject>();
                newUser.Password = passwordTextBox.Password;

                try
                {
                    progressBar.Visibility = System.Windows.Visibility.Collapsed;
                    await newUser.SignUpAsync();
                    NavigationService.GoBack();
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
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Your passwords don't match. Fix it.");
            }
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            createButton.Text = "that's your email?";
        }

        private void displayNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            createButton.Text = "best name ever";
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