using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FleecyBook.Models
{
    public class Category
    {                                         // prop tab tab - autowrite properties  _1
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]      /* Name / Range validation _24 */
        [Range(1, 100, ErrorMessage = "Value must in Between 2 Digites !")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
