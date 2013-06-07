using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Linky.Model;
using Linky.Views;
using Windows.UI.Xaml.Controls;

namespace Linky.ViewModel
{
    public class AddLinkViewModel : LinkyBaseViewModel
    {
        private string _Name;
        private string _Url;
        public ICommand AddLinkCommand { get; set; }
        public Frame Frame { get; set; }

        public AddLinkViewModel()
        {
            AddLinkCommand = new RelayCommand(AddLinkCommandHandler);
        }

        private void AddLinkCommandHandler()
        {
            var link = new Link { Name = Name, Uri = new Uri(Url) };
            LinkService.Add(link);

            Frame.Navigate(typeof(AppHub), link);
        }

        public void OnNavigatedTo(object navigationParameter, Frame frame)
        {
            SetNavigationFrame(frame);
            ResetInputProperties();
        }

        private void SetNavigationFrame(Frame frame)
        {
            Frame = frame;
        }

        private void ResetInputProperties()
        {
            Name = "";
            Url = "";
        }


        public string Url
        {
            get { return _Url; }
            set
            {
                if (value == _Url) return;

                RaisePropertyChanging(() => Url);
                _Url = value;
                RaisePropertyChanged(() => Url);
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (value == _Name) return;

                RaisePropertyChanging(() => Name);
                _Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

    }
}