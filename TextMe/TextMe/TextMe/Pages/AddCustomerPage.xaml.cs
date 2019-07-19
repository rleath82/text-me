using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMe.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Text.RegularExpressions;

namespace TextMe.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCustomerPage : ContentPage
    {
        public AddCustomerPage()
        {
            InitializeComponent();
        }

        async void AddButton_Clicked(object sender, EventArgs e)
        {                        
            var phonePattern = @"^(\+0?1\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";
            Customer customer = new Customer
            {
                Name = nameEntry.Text,
                Phone = phoneEntry.Text,
                OrderTime = DateTime.Now
            };
            if (!string.IsNullOrWhiteSpace(customer.Name) && !string.IsNullOrWhiteSpace(customer.Phone))
            {
                if (Regex.IsMatch(customer.Phone, phonePattern))
                {
                    await App.Database.AddCustomerAsync(customer);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert(null, "Not a valid phone number. Must be 10 digits (including area code).", "OK");
                }
            }
        }        
    }
}