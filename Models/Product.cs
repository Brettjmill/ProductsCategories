using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models
  {
      public class Product
      {
        [Key] // the below prop is the primary key, [Key] is not needed if named with pattern: ModelNameId
        public int ProductId { get; set; }
 
        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "is required")]
        [MinLength(10, ErrorMessage = "must be at least 10 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "is required")]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<Association> Associations {get; set; }
      }
  }
