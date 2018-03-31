using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LibraryLite.Core.Entities;

namespace LibraryLite.UI.Web.MVC.Presentation.ViewModels
{
    public class BookLoanRequestViewModel
    {
        public int Id { get; set; }
        [Display(Name="Student name")]
        [Required]
        public int StudentId { get; set; }
        [Required]
        [Display(Name="Book title")]
        public int BookId { get; set; }
        [Required]
        [Display(Name = "Date of Issue")]
        public DateTime DateOfIssue { get; set; }
        [Required]
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }
        [Display(Name = "Date returned")]
        public DateTime ReturnDate { get; set; }
        public int StudentIdFilter { get; set; }
        public IList<Student> Students { get; set; }
        public Student Student { get; set; }
        public IList<Book> Books { get; set; }
    }
}