using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMe.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using TextMe.Data;
using System.Collections.ObjectModel;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace TextMe.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomersPage : ContentPage
    {
        public CustomersPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            customerListView.ItemsSource = await App.Database.GetCustomersAsync();            
        }

        async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCustomerPage
            {
                BindingContext = new Customer()
            });
        }

        async void CustomerListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {                
                var cust = e.SelectedItem as Customer;
                var action = await DisplayActionSheet(cust.Name, "Cancel", null, "Send Message", "Remove Customer");
                try
                {
                    string messageFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "message.txt");
                    string phoneFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "phone.txt");
                    string accountSIDFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sid.txt");
                    string authTokenFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "auth.txt");

                    string message = File.ReadAllText(messageFile);
                    string accountPhone = File.ReadAllText(phoneFile);
                    string accountSID = File.ReadAllText(accountSIDFile);
                    string authToken = File.ReadAllText(authTokenFile);


                    switch (action)
                    {
                        case "Send Message":
                            bool answer = await DisplayAlert(null, "Send message to " + cust.Name + "?", "Send", "Cancel");
                            if (answer == true)
                            {
                                TwilioClient.Init(accountSID, authToken);
                                MessageResource.Create(
                                    body: message,
                                    from: new Twilio.Types.PhoneNumber("+1" + accountPhone),
                                    to: new Twilio.Types.PhoneNumber("+1" + cust.Phone)
                                );
                                OnAppearing();
                                break;
                            }
                            OnAppearing();
                            break;
                        case "Remove Customer":
                            bool answer2 = await DisplayAlert(null, "Are you sure you want to delete " + cust.Name + "?", "Delete", "Cancel");
                            if (answer2 == true)
                            {
                                await App.Database.RemoveCustomerAsync(cust);
                                OnAppearing();
                                break;
                            }
                            else
                                OnAppearing();
                            break;
                        default:
                            OnAppearing();
                            break;

                    }
                }
                catch
                {
                    await DisplayAlert("Oops!", "Something happened. Please check your settings and try again.", "OK");
                    OnAppearing();
                }
            }
        }

        async void SettingsPage_Clicked(object sender, EventArgs e)
        {             
            await Navigation.PushAsync(new LoginPage());
        }

        async void AddCustomer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCustomerPage());
        }        
    }
}