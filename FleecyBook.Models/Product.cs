﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleecyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string ISBN { get; set; }
        
        [Required]
        public string Author { get; set; }
        
        [Required]
        [DisplayName("List Price")]
        [Range(1, 10000)]
        public double ListPrice { get; set; }
        
        [Required]
        [DisplayName("Price for 1-50")]
        [Range(1, 10000)]
        public double Price { get; set; }
        
        [Required]
        [DisplayName("Price for 51-100")]
        [Range(1, 10000)]
        public double Price50 { get; set; }
        
        [Required]
        [DisplayName("Price for 100+")]
        [Range(1, 10000)]
        public double Price100 { get; set;}
        
        [ValidateNever]
        public string ImageUrl { get; set; }
        
        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; }
        
        [Required]
        [DisplayName("Cover Type")]
        public int CoverTypeId { get; set; }

        [ValidateNever]
        public CoverType CoverType { get; set;}
    }
}
