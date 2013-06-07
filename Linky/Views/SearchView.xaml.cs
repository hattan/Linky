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
    public sealed partial class SearchView : LayoutAwarePage
    {
        public SearchView()
        {
            InitializeComponent();
        }

        protected override bool GetShareContent(DataRequest request)
        {
            return true;
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var viewModel = DataContext as SearchViewModel;
            if (viewModel != null)
            {
                viewModel.OnNavigatedTo(navigationParameter);
            }
        }

        private async void LinkGrid_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var link = e.ClickedItem as Link;
            await Launcher.LaunchUriAsync(link.Uri);
        }

        private void AddLink_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (AddLinkView));
        }
    }
}