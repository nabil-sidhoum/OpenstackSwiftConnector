using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tools.Swift.Connector.Model;

namespace Tools.Swift.Connector
{
    public interface ISwiftClient
    {
        HttpClient Client { get; }

        Task<IEnumerable<SwiftContainer>> GetAllContainersAsync();
        Task<IEnumerable<SwiftFile>> GetFilesByContainerAsync(string containername);
        Task<bool> CreateOrReplaceFileOnContainerAsync(string containername, string filename, byte[] data);
    }
}
