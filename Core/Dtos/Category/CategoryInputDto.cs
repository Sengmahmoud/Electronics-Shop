using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Category
{
    public record CategoryInputDto:DtoBase<int>
    {
        [Display(Name= "Name")]
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
    }
}
