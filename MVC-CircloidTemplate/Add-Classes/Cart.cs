﻿using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CircloidTemplate.Add_Classes
{
    public class Cart
    {
        private List<Product> prdList = new List<Product>();

        public List<Product> PrdList
        {
            get { return prdList; }
            set { prdList = value; }
        }
    }
}