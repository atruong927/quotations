using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuotationsApp.Models
{
    public class Category
    {
        public int CategoryID
        {
            get; set;
        }
        [Display(Name = "Category")]
        public string Name
        {
            get; set;
        }
        //Navigation property goes 
    }
}