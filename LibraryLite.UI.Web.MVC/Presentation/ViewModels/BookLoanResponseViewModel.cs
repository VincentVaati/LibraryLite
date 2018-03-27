namespace LibraryLite.UI.Web.MVC.Presentation.ViewModels
{
    public class BookLoanResponseViewModel
    {
        public bool Success { get; private set; }
        public string ResponseMessage { get; private set; }

        public BookLoanResponseViewModel(bool success, string responseMessage)
        {
            Success = success;
            ResponseMessage = responseMessage;
        }
    }
}