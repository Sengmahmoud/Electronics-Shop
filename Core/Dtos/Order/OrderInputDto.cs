using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Order
{
    public record OrderInputDto:DtoBase<Guid>
    {
        [Required(ErrorMessage ="Field Required")]
        [Display(Name ="Name")]
        public string CusomerName { get; set; }
        [Required(ErrorMessage = "Field Required")]
        [Display(Name ="Mobile")]
        [RegularExpression("01([0-9]{8})", ErrorMessage = @"MobileFromat")]
        public string CusomerMobile { get; set; }
        [Display(Name ="Email")]
        [EmailAddress]
        public string CusomerEmail { get; set; }
        public Guid ProductId { get; set; }
        public int Qantity { get; set; }

    }
}
