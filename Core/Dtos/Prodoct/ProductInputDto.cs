using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Prodoct
{
    public record ProductInputDto:DtoBase<Guid>
    {
        [Required(ErrorMessage ="Field Required")]
        [Display(Name ="Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Field Required")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "Field Required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
   
    }
}
