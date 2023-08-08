using ChurchApp.ViewModels;

namespace ChurchApp.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        var viewModel = new RegisterViewModel
        {
            OnSuccess = async () => await Navigation.PushAsync(new HomePage()),
            OnFail = message => DisplayAlert("Error", message, "OK")
        };
        BindingContext = viewModel;
    }

}