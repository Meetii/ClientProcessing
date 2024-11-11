using ClientProcessing.Application.Interface;
using ClientProcessing.Domain.Models;
using ClientProcessing.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ClientProcessing.Application.Implementation
{
    public class ClientService : IClientService
    {
        public readonly IRepository<Client> _clientRepository;

        public ClientService(IRepository<Client> repository)
        {
            _clientRepository = repository;
        }
        public Task<int> AddClientAsync(Client client)
        {
            try
            {
                Client newClient = new Client();

                newClient.Name = client.Name;
                newClient.BirthDate = client.BirthDate;
                newClient.Addresses = client.Addresses;

                var createdClientId = _clientRepository.CreateAsync(newClient);

                return createdClientId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Client>> ExportClientsToJsonAsync()
        {
            try
            {
                var clients = await _clientRepository.GetAllAsync();
                clients = clients.OrderBy(c => c.Name).ThenBy(c => c.BirthDate).ToList();
                return clients;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            try
            {
                return (List<Client>)await _clientRepository.GetAllAsync();
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public  async Task ImportClientsFromXmlAsync(string xmlData)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Client>), new XmlRootAttribute("Clients"));
                using (StringReader reader = new StringReader(xmlData))
                {
                    var clients = (List<Client>)serializer.Deserialize(reader);
                    foreach (var client in clients)
                    {
                        await _clientRepository.CreateAsync(client);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //because i am using .NET version 6 there isnt bulk insert, in newer version this would be implemented with bulkInsert function
        }
    }
}
