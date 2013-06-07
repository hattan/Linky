using System;
using Linky.Common;
using Linky.Interfaces;
using Linky.Model;
using Microsoft.Practices.ServiceLocation;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.UI.Xaml;

namespace Linky.Views
{
    public sealed partial class ShareTargetView : LayoutAwarePage
    {
        private ShareOperation _ShareOperation;

        public ShareTargetView()
        {
            InitializeComponent();
        }

        public void Activate(ShareTargetActivatedEventArgs args)
        {
            DefaultViewModel["Sharing"] = false;

            _ShareOperation = args.ShareOperation;
            DataPackagePropertySetView shareProperties = _ShareOperation.Data.Properties;
            DefaultViewModel["Link"] = new Link
                                           {
                                               Name = shareProperties.Title,
                                               Uri = new Uri(shareProperties.Description)
                                           };

            Window.Current.Content = this;
            Window.Current.Activate();
        }

        private void ShareButton_Click(object sender, RoutedEventArgs e)
        {
            DefaultViewModel["Sharing"] = true;
            _ShareOperation.ReportStarted();

            var link = DefaultViewModel["Link"] as Link;
            if (link != null)
            {
                var linkService = ServiceLocator.Current.GetInstance<ILinkService>();
                linkService.Add(link);
            }

            _ShareOperation.ReportCompleted();
        }

        protected override bool GetShareContent(DataRequest request)
        {
            return true;
        }
    }
}