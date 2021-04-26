using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flashcards.Mobile.Authentication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [BindingContext(typeof(RegistrationPageViewModel))]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }
    }
}