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
    public partial class SettingsPage : ContentPage
    {
        string _messageFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "message.txt");
        string _phoneFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "phone.txt");
        string _sidFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sid.txt");
        string _authFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "auth.txt");
        string _passwordFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "password.txt");
        public SettingsPage()
        {
            InitializeComponent();

            if (File.Exists(_messageFileName))
            {
                messageEditor.Text = File.ReadAllText(_messageFileName);
            }

            if (File.Exists(_phoneFileName))
            {
                phoneEntry.Text = File.ReadAllText(_phoneFileName);
            }

            if (File.Exists(_sidFileName))
            {
                sidEntry.Text = File.ReadAllText(_sidFileName);
            }

            if (File.Exists(_authFileName))
            {
                authTokenEntry.Text = File.ReadAllText(_authFileName);
            }

            if(File.Exists(_passwordFileName))
            {
                passwordEntry.Text = File.ReadAllText(_passwordFileName);
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(_messageFileName, messageEditor.Text);
                File.WriteAllText(_phoneFileName, phoneEntry.Text);
                File.WriteAllText(_sidFileName, sidEntry.Text);
                File.WriteAllText(_authFileName, authTokenEntry.Text);
                File.WriteAllText(_passwordFileName, passwordEntry.Text);
                DisplayAlert(null, "Items Saved!", "OK");
            }
            catch
            {
                DisplayAlert("Oops!", "Items not saved, please try again.", "OK");
            }
        }

        async void ExitButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomersPage());
            Navigation.RemovePage(this);
        }
    }
}