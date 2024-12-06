﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Category Name")]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Must be between 1 and 100")]
        public int DisplayOrder { get; set; }



        
    }
}