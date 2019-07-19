using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TextMe.Pages;
using TextMe.Data;
using System.IO;

namespace TextMe
{
    public partial class App : Application
    {
        static CustomerDatabase database;
        public static CustomerDatabase Database
        {
            get
            {
                if(database == null)
                {
                    database = new CustomerDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Customers.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new CustomersPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
