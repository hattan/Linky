using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Linky.Model;

namespace Linky.ViewModel
{
    public class AppHubViewModel : LinkyBaseViewModel
    {
        private ObservableCollection<Link> _Links;

        public AppHubViewModel()
        {
            Links = new ObservableCollection<Link>();

            if (IsInDesignMode)
            {
                LoadLinks();
            }
        }

        public ObservableCollection<Link> Links
        {
            get { return _Links; }
            set
            {
                if (value == _Links) return;

                RaisePropertyChanging(() => Links);
                _Links = value;
                RaisePropertyChanged(() => Links);
            }
        }


        private async void LoadLinks(Link newLink = null)
        {
            List<Link> links = await LinkService.GetAllLinks();
            if (links == null) return;
            
            foreach (Link link in links)
            {
                Links.Add(link);
            }

            if (newLink != null)
            {
                bool exists = Links.Any(l => l.LinkId == newLink.LinkId);
                if (!exists)
                {
                    Links.Add(newLink);
                }
            }
        }

        public void OnNavigatedTo(object navigationParameter)
        {
            Links.Clear();
            LoadLinks(navigationParameter as Link);
        }
    }
}