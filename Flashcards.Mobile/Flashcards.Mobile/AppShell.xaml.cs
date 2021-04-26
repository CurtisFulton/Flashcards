using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flashcards.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public ICommand SignOutCommand { get; }

        public AppShell()
        {
            this.SignOutCommand = new AsyncCommand(this.SignOut);
            this.BindingContext = this;
            InitializeComponent();
        }

        private async Task SignOut()
        {
            await this.GoToAsync("//login");
        }
    }
}