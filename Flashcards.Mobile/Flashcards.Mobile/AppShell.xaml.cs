using Flashcards.Mobile.Account;
using Flashcards.Mobile.Authentication;
using Flashcards.Mobile.Decks;
using Flashcards.Mobile.Study;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
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

            Routing.RegisterRoute("login/registration", typeof(RegistrationPage));

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<LoginPageViewModel>();
            serviceCollection.AddScoped<RegistrationPageViewModel>();
            serviceCollection.AddScoped<StudyPageViewModel>();
            serviceCollection.AddScoped<AccountPageViewModel>();
            serviceCollection.AddScoped<DeckPageViewModel>();

            this.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private IServiceProvider ServiceProvider { get; }
        private IMemoryCache CurrentServiceScopes { get; } = new MemoryCache(new MemoryCacheOptions());

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            // If there is a current item, we want to make sure the scope for that page has been removed.
            // That will dispose of any dependencies still alive
            var currentRoute = this.CurrentState?.Location.OriginalString;
            var targetRoute = args.Target.Location.OriginalString;
            if (!string.IsNullOrWhiteSpace(currentRoute))
            {
                currentRoute = currentRoute.Replace("//", "/");
                targetRoute = targetRoute.Replace("//", "/");

                if (currentRoute.StartsWith(targetRoute, StringComparison.CurrentCultureIgnoreCase))
                {
                    var remainingPath = currentRoute.Substring(targetRoute.Length);
                    var subPaths = remainingPath.Split('/', StringSplitOptions.RemoveEmptyEntries);

                    var currentPath = targetRoute;
                    foreach (var path in subPaths)
                    {
                        currentPath += "/" + path;
                        this.CurrentServiceScopes.Remove(currentPath);
                    }
                }
                else if (!targetRoute.StartsWith(currentRoute))
                {
                    var subPaths = currentRoute.Split('/', StringSplitOptions.RemoveEmptyEntries);

                    var currentPath = "";
                    foreach (var path in subPaths)
                    {
                        currentPath += "/" + path;
                        this.CurrentServiceScopes.Remove(currentPath);
                    }
                }
            }
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            var path = this.CurrentState?.Location.OriginalString.Replace("//", "/");
            this.TrySetBindingContext(this.CurrentPage, path);

            base.OnNavigated(args);
        }

        private void TrySetBindingContext(Page page, string? path)
        {
            if (page is null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            if (page.BindingContext is not null)
            {
                return;
            }

            // Check if this 
            var currentPageType = page.GetType();
            var bindingContextType = currentPageType.GetCustomAttribute<BindingContextAttribute>()?.BindingContextType;
            if (bindingContextType is null)
            {
                return;
            }

            var pageScope = this.ServiceProvider.CreateScope();
            var entryOptions = new MemoryCacheEntryOptions();
            entryOptions.RegisterPostEvictionCallback(OnPageScopeEvicted);

            this.CurrentServiceScopes.Set(path, new PageScopeData(page, pageScope), entryOptions);
            page.BindingContext = pageScope.ServiceProvider.GetRequiredService(bindingContextType);
        }

        private void OnPageScopeEvicted(object key, object value, EvictionReason reason, object state)
        {
            if (value is PageScopeData pageScopeData)
            {
                pageScopeData.ServiceScope.Dispose();
                Device.BeginInvokeOnMainThread(() => pageScopeData.Page.BindingContext = null);
                Console.WriteLine($"Disposed: {key}");
            }
        }

        private class PageScopeData 
        {
            public PageScopeData(Page page, IServiceScope serviceScope)
            {
                this.Page = page;
                this.ServiceScope = serviceScope;
            }

            public Page Page { get; }
            public IServiceScope ServiceScope { get; }
        }
    }
}