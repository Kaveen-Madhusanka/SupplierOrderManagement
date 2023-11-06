using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM.ProductService.Application.Dto
{
    public class ProductDto
    {
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Description { get; set; }
        public int? ProductCategoryId { get; set; }
    }
}
