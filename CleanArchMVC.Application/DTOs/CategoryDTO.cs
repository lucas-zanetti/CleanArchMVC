using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchMVC.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string? Name { get; set; }
    }
}
