using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMe.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace TextMe.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {        
        public LoginPage()
        {
            InitializeComponent();            
        }

        async void LoginButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string passwordFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "password.txt");
                string password = File.ReadAllText(passwordFile);
                string enterPassword = passwordEntry.Text;
                if (enterPassword == password)
                {
                    await Navigation.PushAsync(new SettingsPage());
                    Navigation.RemovePage(this);
                }
                else
                {
                    await DisplayAlert(null, "Incorrect password entered, please try again", "OK");
                }
            }
            catch
            {
                await DisplayAlert(null, "You do not have a password set up yet. Redirecting you to the New User page.", "OK");
                await Navigation.PushAsync(new NewUserPage());
                Navigation.RemovePage(this);
            }
        }

        async void NewUserButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewUserPage());
        }
    }
}