using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextMe.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUserPage : ContentPage
    {
        string _passwordFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "password.txt");
        public NewUserPage()
        {
            InitializeComponent();            
        }

        async void SavePasswordButton_Clicked(object sender, EventArgs e)
        {
            string systemPass = systemPassword.Text;
            string newPass = newPasswordEntry.Text;
            string confPass = confirmPasswordEntry.Text;
            if(newPass == confPass && systemPass == "admin123")
            {
                File.WriteAllText(_passwordFileName, newPasswordEntry.Text);
                await DisplayAlert(null, "Password Saved!", "OK");
                await Navigation.PushAsync(new LoginPage());
                Navigation.RemovePage(this);
            }
            else
            {
                await DisplayAlert(null, "Passwords do not match, please try again", "OK");
                await Navigation.PushAsync(new NewUserPage());
            }
        }
    }
}