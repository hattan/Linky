using System.Collections.Generic;
using System.Collections.ObjectModel;
using Linky.Model;

namespace Linky.ViewModel
{
    public class SearchViewModel : LinkyBaseViewModel
    {
        private ObservableCollection<Link> _Links;

        public SearchViewModel()
        {
            if (IsInDesignMode)
            {
                LoadLinks("Foo");
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

        private async void LoadLinks(string searchTerm)
        {
            if (searchTerm == null) return;

            List<Link> links = await LinkService.Search(searchTerm);
            if (links == null) return;

            Links = new ObservableCollection<Link>();
            foreach (Link link in links)
            {
                Links.Add(link);
            }
        }

        public void OnNavigatedTo(object navigationParameter)
        {
            LoadLinks(navigationParameter as string);
        }
    }
}