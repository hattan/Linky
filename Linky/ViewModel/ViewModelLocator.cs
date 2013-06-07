using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Linky.Interfaces;
using Linky.Services;
using Microsoft.Practices.ServiceLocation;

namespace Linky.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                if (!SimpleIoc.Default.IsRegistered<ILinkService>())
                {
                    SimpleIoc.Default.Register<ILinkService, DesignTimeLinkService>();
                }
            }
            else
            {
                if (!SimpleIoc.Default.IsRegistered<ILinkService>())
                {
                    SimpleIoc.Default.Register<ILinkService, LinkService>();
                }
            }

            SimpleIoc.Default.Register<AppHubViewModel>();
            SimpleIoc.Default.Register<AddLinkViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
        }

        public AppHubViewModel AppHub
        {
            get { return ServiceLocator.Current.GetInstance<AppHubViewModel>(); }
        }

        public AddLinkViewModel AddLink
        {
            get { return ServiceLocator.Current.GetInstance<AddLinkViewModel>(); }
        }

        public SearchViewModel Search
        {
            get { return ServiceLocator.Current.GetInstance<SearchViewModel>(); }
        }


        public static void Cleanup(){}
    }
}