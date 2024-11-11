using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProcessing.Domain.Models
{
    public class Address : BaseEntity
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [StringLength(9, ErrorMessage = "Type cannot exceed 9 characters.")]
        public string? Type { get; set; }
        public string? Location { get; set; }
    }
}
