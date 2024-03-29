﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentAServ.Models.ViewModels
{
   public class ServiceVM
    {
        public Service Service { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public IEnumerable<SelectListItem> FrequencyList { get; set; }
    }
}
