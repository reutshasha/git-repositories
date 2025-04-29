using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{

    public class DateRange
    {
        [Required(ErrorMessage = "Start date is required.")]
        public string startDate { get; set; } = string.Empty;


        [Required(ErrorMessage = "End date is required.")]
        public string endDate { get; set; } = string.Empty;
    }
}
