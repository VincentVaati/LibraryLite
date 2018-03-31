using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLite.UseCases.Dtos
{
    public class BookReturnResponseMessage
    {
        public bool IsLateReturn { get; set; }
        public decimal Penalty { get; set; }

        public BookReturnResponseMessage() { }
    }
}
