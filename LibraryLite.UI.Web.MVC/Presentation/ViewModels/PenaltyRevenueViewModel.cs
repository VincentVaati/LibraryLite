using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryLite.UI.Web.MVC.Presentation.ViewModels
{
    public class PenaltyRevenueViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DateReturned { get; set; }
        public decimal Penalty { get; set; }

        public PenaltyRevenueViewModel() { }
    }
}