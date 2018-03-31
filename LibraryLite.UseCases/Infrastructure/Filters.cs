using System;
namespace LibraryLite.UseCases.Infrastructure
{
    public class ReportFilter
    {
        public int MonthId { get; set; }
        public int Year { get; set; }
        public DateTime Date { get; set; }

        public ReportFilter() { }
        public ReportFilter(int monthId, int year,DateTime date)
        {
            MonthId = monthId;
            Year = year;
            Date = date;
        }
    }
}
