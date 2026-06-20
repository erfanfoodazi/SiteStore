using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products.DTOs
{
    public class ProductImageResultDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
