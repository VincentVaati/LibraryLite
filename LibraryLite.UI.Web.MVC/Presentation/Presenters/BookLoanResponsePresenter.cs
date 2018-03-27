using System.Text;
using LibraryLite.UseCases.Dtos;
using LibraryLite.UI.Web.MVC.Presentation.ViewModels;

namespace LibraryLite.UI.Web.MVC.Presentation.Presenters
{
    public class BookLoanResponsePresenter
    {
        public BookLoanResponseViewModel Handle(BookLoanResponseMessage responseMessage)
        {
            if (responseMessage.Success)
                return new BookLoanResponseViewModel(true, "Book loan was successful");

            var sb = new StringBuilder();
            sb.AppendLine("Failed to Loan the following book(s)");

            foreach (var err in responseMessage.Errors)
                sb.AppendLine();

            return new BookLoanResponseViewModel(false, sb.ToString());
        }
    }
}