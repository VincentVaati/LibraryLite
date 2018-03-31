using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryLite.UI.Web.MVC.Presentation.ViewModels
{
    public class ReturnBookRequestViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Book title")]
        [Required]
        public int BookId { get; set; }
        [Display(Name = "Student name")]
        [Required]
        public int StudentId { get; set; }
        [Required]
        [Display(Name = "Book number")]
        public string BookNumber { get; set; }
        [Required]
        [Display(Name = "Date of Issue")]
        public DateTime DateOfIssue { get; set; }
        [Required]
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }
        [Required]
        [Display(Name = "Date Returned")]
        public DateTime DateReturned { get; set; }
        public decimal Penalty { get; set; }
    }
}