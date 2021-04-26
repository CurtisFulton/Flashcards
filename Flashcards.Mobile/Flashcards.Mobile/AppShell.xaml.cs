using Flashcards.Mobile.Pages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Flashcards.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<LoginPageViewModel>();

            this.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private IServiceProvider ServiceProvider { get; }
        private IMemoryCache CurrentServiceScopes { get; } = new MemoryCache(new MemoryCacheOptions());

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            // If there is a current item, we want to make sure the scope for that page has been removed.
            // That will dispose of any dependencies still alive
            var currentRoute = this.CurrentItem?.Route;
            if (!string.IsNullOrWhiteSpace(currentRoute))
            {
                this.CurrentServiceScopes.Remove(currentRoute);
            }
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            this.TrySetBindingContext(this.CurrentPage, this.CurrentItem?.Route);

            base.OnNavigated(args);
        }

        private void TrySetBindingContext(Page page, string? path)
        {
            if (this.CurrentPage is null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            // Check if this 
            var currentPageType = this.CurrentPage.GetType();
            var bindingContextType = currentPageType.GetCustomAttribute<BindingContextAttribute>()?.BindingContextType;
            if (bindingContextType is null)
            {
                return;
            }

            var pageScope = this.ServiceProvider.CreateScope();
            var entryOptions = new MemoryCacheEntryOptions();
            entryOptions.RegisterPostEvictionCallback(OnPageScopeEvicted);

            this.CurrentServiceScopes.Set(path, pageScope, entryOptions);
            this.CurrentPage.BindingContext = pageScope.ServiceProvider.GetRequiredService(bindingContextType);
        }

        private void OnPageScopeEvicted(object key, object value, EvictionReason reason, object state)
        {
            if (value is IServiceScope serviceScope)
            {
                serviceScope.Dispose();
            }
        }

        private void SignOutButton_Clicked(object sender, EventArgs e)
        {
            // Temp
            Shell.Current.GoToAsync("//login");
        }
    }
}