using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuotationsApp.Models
{
    public class Quotation
    {
        public int QuotationID
        {
            get; set;
        }
        public string Author
        {
            get; set;
        }
        public string Quote
        {
            get; set;
        }
        [Display(Name = "Date Added")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DateAdded
        {
            get; set;
        }
        public int CategoryID
        {
            get; set;
        }
        public virtual Category Category
        {
            get; set;
        }
        //public string CategoryName
        //{
        //    get; set;
        //}
    }
}