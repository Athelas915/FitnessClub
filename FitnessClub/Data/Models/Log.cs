using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models
{
    public class Log
    {
        public string Exception { get; set; }
        public int Level { get; set; }
        [Column(TypeName = "jsonb")]
        public string Log_event { get; set; }
        public string Message { get; set; }
        public string Message_template { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
