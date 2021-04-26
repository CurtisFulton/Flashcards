using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flashcards.Mobile.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [BindingContext(typeof(LoginPageViewModel))]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}