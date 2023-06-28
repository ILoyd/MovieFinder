using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Services.NavigationService;

namespace MovieApp.Models
{
    public class PageHeader
    {
        public string MediaType { get; set; }
        public int PageNumber { get; set; }
        public string DayOrWeek { get; set; }
        public int ShowId { get; set; }
        public string SearchKey { get; set; }
    }
}
