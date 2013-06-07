using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linky.Common;
using Linky.Interfaces;
using Linky.Model;
using Linky.Views;
using Microsoft.Practices.ServiceLocation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Search;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Linky
{
    sealed partial class App : Application
    {
        private SearchPane _SearchPane;

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        private async void searchPane_SuggestionsRequested(SearchPane sender,
                                                           SearchPaneSuggestionsRequestedEventArgs args)
        {
            if (args.QueryText.Length >= 2)
            {
                var linkService = ServiceLocator.Current.GetInstance<ILinkService>();
                SearchPaneSuggestionsRequestDeferral deferral = args.Request.GetDeferral();
                List<Link> results = await linkService.Search(args.QueryText);
                List<string> searchResults = results.Select(s => s.Name).ToList();
                args.Request.SearchSuggestionCollection.AppendQuerySuggestions(searchResults);
                deferral.Complete();
            }
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            _SearchPane = SearchPane.GetForCurrentView();
            _SearchPane.SuggestionsRequested += searchPane_SuggestionsRequested;

            SettingsPane.GetForCurrentView().CommandsRequested += App_SettingsRequested;

            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                if (!rootFrame.Navigate(typeof (AppHub), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            Window.Current.Activate();
        }

        private void App_SettingsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            var privacyPolicySettingsCommand = new SettingsCommand("privacypolicy", "Privacy Policy",
                                                                  PrivacyPolicySettingsCommandHandler);
            args.Request.ApplicationCommands.Add(privacyPolicySettingsCommand);
        }

        private  async void PrivacyPolicySettingsCommandHandler(IUICommand command)
        {
            var uri = new Uri("http://www.google.com");
            await Launcher.LaunchUriAsync(uri);
        }


        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }

        protected override async void OnSearchActivated(SearchActivatedEventArgs args)
        {
            UIElement previousContent = Window.Current.Content;
            var frame = previousContent as Frame;
            if (frame == null)
            {
                frame = new Frame();
                SuspensionManager.RegisterFrame(frame, "AppFrame");

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                    }
                }
            }

            frame.Navigate(typeof (SearchView), args.QueryText);
            Window.Current.Content = frame;

            Window.Current.Activate();
        }

        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            var shareTargetPage = new ShareTargetView();
            shareTargetPage.Activate(args);
        }

      
    }
}