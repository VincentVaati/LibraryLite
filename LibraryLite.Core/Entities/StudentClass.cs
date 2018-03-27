using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryLite.Core.Interfaces;

namespace LibraryLite.Core.Entities
{
    public class StudentClass : IEntity
    {

        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string ClassName { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public StudentClass()
        {
            Students = new HashSet<Student>();
        }
    }
}
