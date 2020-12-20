using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentAServ.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }


        public OrderHeader OrderHeader { get; set; }


        [Required]
        public int ServiceId { get; set; }


        public Service Service { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public double Price { get; set; }
    }
   
}
