using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Linky.Common;
using Linky.Interfaces;
using Linky.Model;

namespace Linky.Services
{
    public class LinkService : ILinkService
    {
        const string FileName = "links";
     
        private async Task<bool> FileExists()
        {
            return await StorageHelper.FileExistsAsync(FileName);
        }

        public Task<List<Link>> GetAllLinks()
        {
            try
            {
                return StorageHelper.ReadFileAsync<List<Link>>("links");
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Link>> Search(string searchTerm)
        {
            if (await FileExists())
            {
                var links = await StorageHelper.ReadFileAsync<List<Link>>("links");
                return links.Where(l => l.Name.ToLower().StartsWith(searchTerm.ToLower())).ToList();
            }
            return new List<Link>();
        }

        public async void Add(Link link)
        {
            link.LinkId = Guid.NewGuid();
            List<Link> links = await FileExists() ? await StorageHelper.ReadFileAsync<List<Link>>(FileName) : new List<Link>();
            links.Add(link);
            StorageHelper.WriteFileFireAndForget(FileName, links,StorageHelper.StorageStrategies.Local);
        }

    }
}