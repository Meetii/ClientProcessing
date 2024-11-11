using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientProcessing.Domain.Models
{
    public class Client : BaseEntity
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [XmlAttribute("Name")]
        public string Name { get; set; } = string.Empty;
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [XmlAttribute("BirthDate")]
        public DateTime BirthDate { get; set; }
        [XmlElement("Addresses")]
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
