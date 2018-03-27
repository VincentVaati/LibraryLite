using System.ComponentModel.DataAnnotations;

namespace LibraryLite.UI.Web.MVC.Presentation.ViewModels
{
    public class ApplicationSettingsViewModel
    {
        public int Id { get; set; }
        [Display(Name="Institution name")]
        [Required]
        public string InstitutionName { get; set; }
        [Required]
        [Display(Name = "Loan limit")]
        public int MaxBooksLoanable { get; set; }
        [Display(Name = "Loan days")]
        [Required]
        public int LoanDays { get; set; }
        [Required]
        public decimal Penalty { get; set; }

        public ApplicationSettingsViewModel() { }
        public ApplicationSettingsViewModel(int id,string institutionName,int maxBooksLoanable,int loanDays,decimal penalty)
        {
            Id = id;
            InstitutionName = institutionName;
            MaxBooksLoanable = maxBooksLoanable;
            LoanDays = loanDays;
            Penalty = penalty;
        }
    }
}