using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flashcards.Mobile.Decks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [BindingContext(typeof(DeckPageViewModel))]
    public partial class DeckPage : ContentPage
    {
        public DeckPage()
        {
            InitializeComponent();
        }
    }
}