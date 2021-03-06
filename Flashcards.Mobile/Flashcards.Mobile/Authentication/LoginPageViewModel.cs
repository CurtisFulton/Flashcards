using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Flashcards.Mobile.Authentication
{
    public class LoginPageViewModel 
    {
        public ICommand LoginCommand { get; }
        public ICommand GoToRegistrationCommand { get; }

        public string? UserName { get; set; }
        public string? Password { get; set; }

        public LoginPageViewModel()
        {
            this.LoginCommand = new AsyncCommand(this.LogUserIn, allowsMultipleExecutions: false);
            this.GoToRegistrationCommand = new AsyncCommand(this.GoToRegistration, allowsMultipleExecutions: false);
        }

        private async Task LogUserIn()
        {
            await Shell.Current.GoToAsync("//study").ConfigureAwait(false);
        }

        private async Task GoToRegistration()
        {
            await Shell.Current.GoToAsync("//login/registration").ConfigureAwait(false);
        }
    }
}
