using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    [Table("Note")]
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [AllowNull]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int StatusType { get; set; } = 1;

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("StatusType")]
        public virtual Status Status { get; set; }
    }
}
