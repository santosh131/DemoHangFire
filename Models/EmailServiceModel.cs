using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoHangFire.Models
{
    public class EmailServiceModel
    {
        [Required] 
        public string ServiceName { get; set; }

        [Required]
        public string ConnectionString { get; set; }

        [Required]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter valid Number between 1 & 30")]
        public int TimeInterval { get; set; } = 5;
    }
}
