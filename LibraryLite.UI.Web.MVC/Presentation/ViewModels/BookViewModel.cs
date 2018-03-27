using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryLite.Core.Entities;

namespace LibraryLite.UI.Web.MVC.Presentation.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Book number")]
        public string BookNumber { get; set; }
        [Required]
        [Display(Name = "Isbn")]
        public string ISBN { get; set; }
        [Required]
        [Display(Name = "Book title")]
        public string BookTitle { get; set; }

        public BookViewModel() { }
        public BookViewModel(int id, string bookNumber, string isbn, string bookTitle)
        {
            Id = id;
            BookNumber = bookNumber;
            ISBN = isbn;
            BookTitle = bookTitle;
        }
    }
}