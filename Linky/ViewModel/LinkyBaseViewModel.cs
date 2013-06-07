using GalaSoft.MvvmLight;
using Linky.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace Linky.ViewModel
{
    public class LinkyBaseViewModel : ViewModelBase
    {
        public LinkyBaseViewModel()
        {
            LinkService = ServiceLocator.Current.GetInstance<ILinkService>();
        }

        public ILinkService LinkService { get; set; }
    }
}