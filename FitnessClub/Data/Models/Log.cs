using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models
{
    public class Log : BaseEntity
    {
        public int Id { get; set; }
        [Column("exception", TypeName = "text")]
        public string Exception { get; set; }
        [Column("level", TypeName = "int4")]
        public int Level { get; set; }
        [Column("log_event", TypeName = "jsonb")]
        public string Log_event { get; set; }
        [Column("message", TypeName = "text")]
        public string Message { get; set; }
        [Column("message_template", TypeName = "text")]
        public string Message_template { get; set; }
        [Column("timestamp", TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
