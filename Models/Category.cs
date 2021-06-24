using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models
  {
      public class Category
      {
        [Key] // the below prop is the primary key, [Key] is not needed if named with pattern: ModelNameId
        public int CategoryId { get; set; }
 
        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<Association> Associations {get; set; }
      }
  }