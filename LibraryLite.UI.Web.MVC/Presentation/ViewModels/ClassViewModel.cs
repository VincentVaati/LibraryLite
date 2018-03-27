namespace LibraryLite.UI.Web.MVC.Presentation.ViewModels
{
    public class ClassViewModel
    {
        public int Id { get; set; }
        public string ClassName { get; set; }

        public ClassViewModel()
        {

        }
        public ClassViewModel(int id, string className)
        {
            Id = id;
            ClassName = className;
        }
    }
}