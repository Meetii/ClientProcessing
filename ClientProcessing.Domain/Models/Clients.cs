using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientProcessing.Domain.Models
{
    [XmlRoot("Clients")]
    public class Clients
    {
        [XmlElement("Client")]
        public IEnumerable<Client> ClientList{ get; set; }
    }
}
