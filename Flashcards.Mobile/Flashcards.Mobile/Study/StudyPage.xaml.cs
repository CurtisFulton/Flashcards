using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flashcards.Mobile.Study
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [BindingContext(typeof(StudyPageViewModel))]
    public partial class StudyPage : ContentPage
    {
        public StudyPage()
        {
            InitializeComponent();
        }
    }
}