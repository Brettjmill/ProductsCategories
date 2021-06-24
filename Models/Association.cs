using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models
  {
      public class Association
      {
        [Key] // the below prop is the primary key, [Key] is not needed if named with pattern: ModelNameId
        public int AssociationId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
      }
  }