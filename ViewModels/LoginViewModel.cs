using ChurchApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChurchApp.Models;

namespace ChurchApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService = new AuthService();

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }

        public Action OnLoginSuccess { get; set; }

        private async Task Login()
        {
            var userAuth = new UserAuth
            {
                Username = this.Username,
                Password = this.Password
            };

            var token = await _authService.Login(userAuth);

            if (token != null)
            {
                OnLoginSuccess?.Invoke(); // Call action on successful login
            }
            else
            {
                // Handle error
            }
        }

        // Implement INotifyPropertyChanged as needed
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
