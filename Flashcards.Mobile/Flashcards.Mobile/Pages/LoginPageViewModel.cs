using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Flashcards.Mobile.Pages
{
    public class LoginPageViewModel
    {
        public ICommand LoginCommand { get; private set; }

        public LoginPageViewModel()
        {
            this.LoginCommand = new AsyncCommand(this.LogUserIn);
        }

        private async Task LogUserIn()
        {
            await Shell.Current.GoToAsync("//study");
        }
    }
}
