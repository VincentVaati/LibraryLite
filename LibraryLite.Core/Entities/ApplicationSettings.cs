using LibraryLite.Core.Interfaces;

namespace LibraryLite.Core.Entities
{
    public class ApplicationSettings : IEntity
    {
        public int Id { get; set; }
        public string InstitutionName { get; set; }
        public int MaxBooksLoanable { get; set; }
        public int LoanDays { get; set; }
        public decimal Penalty { get; set; }

        public ApplicationSettings() { }

        public ApplicationSettings(string institutionName, int maxBooksLoanable, int loanDays, decimal penalty)
        {
            InstitutionName = institutionName;
            MaxBooksLoanable = maxBooksLoanable;
            LoanDays = loanDays;
            Penalty = penalty;
        }
    }
}
