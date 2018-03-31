using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryLite.UI.Web.MVC.Presentation.ViewModels
{
    public class ReturnBookResponseViewModel
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Penalty { get; set; }
    }
}