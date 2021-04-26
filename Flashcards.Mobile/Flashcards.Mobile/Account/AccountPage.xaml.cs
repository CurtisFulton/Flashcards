using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flashcards.Mobile.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [BindingContext(typeof(AccountPageViewModel))]
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }
    }
}