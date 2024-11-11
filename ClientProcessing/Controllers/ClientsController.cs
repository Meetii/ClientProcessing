using ClientProcessing.Application.Interface;
using ClientProcessing.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientProcessing.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clients = await _clientService.GetClientsAsync();
                return Ok(clients);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            try
            {
                await _clientService.AddClientAsync(client);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExportClientsToJson()
        {
            try
            {
                var jsonData = await _clientService.ExportClientsToJsonAsync();
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ImportClients([FromBody] ImportClientsRequest request)
        {
            try
            {
                await _clientService.ImportClientsFromXmlAsync(request.XmlData);
                return Ok("Clients imported successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error importing clients: {ex.Message}");
            }
        }
    }

    public class ImportClientsRequest
    {
        public string XmlData { get; set; }
    }
}
