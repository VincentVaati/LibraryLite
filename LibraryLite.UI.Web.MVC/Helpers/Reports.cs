using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryLite.UI.Web.MVC.Helpers
{
    public enum  Report
    {
        [Display(Name="Late Book Loans")]
        LateBookLoans,
        [Display(Name = "Penalty Revenue")]
        PenaltyRevenue
    }  

    public enum Months
    {
        January =1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        Octomber,
        November,
        December
    }
    public static class EnumHelper
    {
        public static SelectList ToSelectList<TEnum>(this TEnum obj)
            where TEnum:struct,IComparable,IFormattable,IConvertible
        {
            return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>().
                Select(x =>
                    new SelectListItem
                    {
                        Text= Enum.GetName(typeof(TEnum),x),
                        Value =Convert.ToInt32(x).ToString()
                    }),"Value","Text");
        }
    }
}
