using System.Collections.Generic;
using System.Threading.Tasks;
using Linky.Model;

namespace Linky.Interfaces
{
    public interface ILinkService
    {
        Task<List<Link>> GetAllLinks();
        Task<List<Link>> Search(string searchTerm);
        void Add(Link link);
    }
}