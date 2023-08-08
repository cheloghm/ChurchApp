using ChurchApp.ViewModels;
using ChurchApp.Views;
using Microsoft.Maui.Controls;
using Microsoft.Win32;
using System;

namespace ChurchApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var viewModel = BindingContext as LoginViewModel;
            viewModel.OnLoginSuccess += async () =>
            {
                await Navigation.PushAsync(new HomePage()); // Navigate to your main page view
            };
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Navigate to the registration page
            await Navigation.PushAsync(new RegisterPage()); // Replace with your registration page view
        }
    }
}
