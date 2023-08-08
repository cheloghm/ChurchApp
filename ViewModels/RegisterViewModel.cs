using ChurchApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChurchApp.Models;
using ChurchApp.Views;

namespace ChurchApp.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService = new AuthService();
        private string _errorMessage;

        public string Username { get; set; }
        public string Password { get; set; }
        // Include other properties as needed

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public ICommand RegisterCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public Action OnSuccess { get; set; }
        public Action<string> OnFail { get; set; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await Register());
        }

        private async Task Register()
        {
            var userAuth = new UserAuth
            {
                Username = this.Username,
                Password = this.Password
                // Include other properties as needed
            };

            var user = await _authService.Register(userAuth);

            if (user != null)
            {
                OnSuccess?.Invoke();
            }
            else
            {
                OnFail?.Invoke("Registration failed: email already in use.");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
