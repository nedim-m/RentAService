using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentAServ.Models
{
   public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Service Name")]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [DisplayName("Description")]

        public string LongDesc { get; set; }


        [DataType(DataType.ImageUrl)]
        [DisplayName("Image")]
        public string ImageUrl { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        [DisplayName("Frequency")]
        public int FrequencyId { get; set; }

        public Frequency Frequency { get; set; }

    }
}
