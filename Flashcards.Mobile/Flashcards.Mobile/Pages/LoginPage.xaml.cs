using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flashcards.Mobile.Pages
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