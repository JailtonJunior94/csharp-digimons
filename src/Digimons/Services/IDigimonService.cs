using System.Threading.Tasks;

namespace Digimons.Services
{
    public interface IDigimonService
    {
        Task<string[]> GetNamesAsync();
        Task RequestAsync(string url);
    }
}
