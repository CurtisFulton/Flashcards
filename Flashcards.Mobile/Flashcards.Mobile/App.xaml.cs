using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flashcards.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
