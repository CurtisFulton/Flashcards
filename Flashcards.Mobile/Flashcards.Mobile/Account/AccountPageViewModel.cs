using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Flashcards.Mobile.Account
{
    public class AccountPageViewModel
    {
        public ICommand SignOutCommand { get; }

        public AccountPageViewModel()
        {
            this.SignOutCommand = new AsyncCommand(this.SignOut, allowsMultipleExecutions: false);
        }

        private async Task SignOut()
        {
            await Shell.Current.GoToAsync("//login");
        }
    }
}
