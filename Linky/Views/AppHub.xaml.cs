using System;
using System.Collections.Generic;
using Linky.Common;
using Linky.Model;
using Linky.ViewModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Linky.Views
{
    public sealed partial class AppHub : LayoutAwarePage
    {
        public AppHub()
        {
            InitializeComponent();
        }

        protected override bool GetShareContent(DataRequest request)
        {
            var link = ResultsGridView.SelectedItem as Link;
            if (link != null)
            {
                DataPackage dataPackage = request.Data;
                dataPackage.Properties.Title = link.Name + " Shared via Linky!";
                dataPackage.Properties.Description = link.Name + " Shared via Linky!";
                dataPackage.SetUri(link.Uri);
                return true;
            }
            return true;
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var viewModel = DataContext as AppHubViewModel;
            if (viewModel != null)
            {
                viewModel.OnNavigatedTo(navigationParameter);
            }
        }

        private void AddLink_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (AddLinkView));
        }

        private async void LinkGrid_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var link = e.ClickedItem as Link;
            await Launcher.LaunchUriAsync(link.Uri);
        }
    }
}