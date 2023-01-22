using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ContactApi.Model.Contact
{
    public class CreateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]        
        public string Company { get; set; }           

    }
}
