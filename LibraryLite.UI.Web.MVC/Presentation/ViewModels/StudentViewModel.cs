using LibraryLite.Core.Entities;
using System.ComponentModel.DataAnnotations;
namespace LibraryLite.UI.Web.MVC.Presentation.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [Display(Name="Student number")]
        [Required]
        public string StudentNumber { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Class")]
        public int ClassId { get; set; }
        public StudentClass Class { get; set; }
        public StudentViewModel() { }
        public StudentViewModel(int id, string studentNumber, string firstName, string lastName, int classId, StudentClass cls)
        {
            Id = id;
            StudentNumber = firstName;
            LastName = lastName;
            ClassId = classId;
            Class = cls;
        }
    }
}