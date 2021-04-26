using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Flashcards.Mobile.Authentication
{
    public class RegistrationPageViewModel
    {
        public ICommand CloseCommand { get; }

        public RegistrationPageViewModel()
        {
            this.CloseCommand = new AsyncCommand(this.CloseRegistrationPage, allowsMultipleExecutions: false);
        }

        private async Task CloseRegistrationPage()
        {
            await Shell.Current.GoToAsync("//login").ConfigureAwait(false);
        }
    }
}
