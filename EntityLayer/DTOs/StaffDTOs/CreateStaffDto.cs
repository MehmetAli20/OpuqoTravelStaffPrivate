﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.StaffDTOs
{
    public class CreateStaffDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        //public int AdminID { get; set; } = 1;
        //public bool IsAdmin { get; set; } = false;
    }
}