using ClientProcessing.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProcessing.Application.Interface
{
    public interface IClientService
    {
        Task<int> AddClientAsync(Client client);
        Task<List<Client>> GetClientsAsync();
        Task ImportClientsFromXmlAsync(string xml);
        Task<IEnumerable<Client>> ExportClientsToJsonAsync();
    }
}
