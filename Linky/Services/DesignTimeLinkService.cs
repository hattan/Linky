using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Linky.Interfaces;
using Linky.Model;

namespace Linky.Services
{
    public class DesignTimeLinkService : ILinkService
    {
        private readonly List<Link> _Links;

        public DesignTimeLinkService()
        {
            _Links = new List<Link>();
            Add(new Link {Name = "Google", Uri = new Uri("http://www.google.com")});
            Add(new Link {Name = "Yahoo", Uri = new Uri("http://www.yahoo.com")});
            Add(new Link {Name = "Yahoo", Uri = new Uri("http://www.yahoo.com")});
            Add(new Link {Name = "Yahoo", Uri = new Uri("http://www.yahoo.com")});
            Add(new Link {Name = "Yahoo", Uri = new Uri("http://www.yahoo.com")});
            Add(new Link {Name = "Yahoo", Uri = new Uri("http://www.yahoo.com")});
            Add(new Link {Name = "Yahoo", Uri = new Uri("http://www.yahoo.com")});
        }

        public Task<List<Link>> GetAllLinks()
        {
            return Task.Run(() => _Links);
        }

        public Task<List<Link>> Search(string searchTerm)
        {
            List<Link> links = _Links.Where(l => l.Name.Contains(searchTerm) || l.Uri.ToString().Contains(searchTerm)).ToList();
            return Task.Run(() => links);
        }

        public void Add(Link link)
        {
            link.LinkId = Guid.NewGuid();
            _Links.Add(link);
        }

        public Link GetLinkById(Guid id)
        {
            return _Links.FirstOrDefault(l => l.LinkId == id);
        }
    }
}