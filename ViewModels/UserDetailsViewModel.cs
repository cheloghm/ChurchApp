using ChurchApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChurchApp.ViewModels
{
    public class UserDetailsViewModel : INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime DOB { get; set; }
        public ImageSource ProfilePhoto { get; set; }

        // ... other properties and INotifyPropertyChanged implementation

        public async Task LoadUserDetails(string token, string userId)
        {
            var authService = new AuthService();
            var userDetailsDTO = await authService.GetUserDetails(token, userId);

            if (userDetailsDTO != null)
            {
                FirstName = userDetailsDTO.FirstName;
                MiddleName = userDetailsDTO.MiddleName;
                LastName = userDetailsDTO.LastName;
                Email = userDetailsDTO.Email;
                Role = userDetailsDTO.Role;
                DOB = userDetailsDTO.DOB;
                ProfilePhoto = ImageSource.FromStream(() => new MemoryStream(userDetailsDTO.ProfilePhoto));

                // Notify property changes if necessary
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
