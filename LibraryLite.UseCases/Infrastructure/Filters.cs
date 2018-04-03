using System;
namespace LibraryLite.UseCases.Infrastructure
{
    public class ApplicationFilter
    {
        public int MonthId { get; set; }
        public int Year { get; set; }
        public DateTime Date { get; set; }

        public ApplicationFilter() { }
        public ApplicationFilter(int monthId, int year,DateTime date)
        {
            MonthId = monthId;
            Year = year;
            Date = date;
        }
    }
}
