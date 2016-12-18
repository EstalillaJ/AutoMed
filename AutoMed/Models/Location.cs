﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AutoMed.Models
{
    public class Location
    {   
        public int Id { get; set; }
        [Index(IsUnique=true)]
        [MaxLength(450)]
        public string Name { get; set; }

        public List<Quote> Quotes { get; set; }

        public List<AutoMedUser> Employees { get; set; }
    }
}