using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    [Table("Actor")]
    public class Actor
    {
        [Key]
        public Guid ActorId { get; set; }
        [Required]
        [StringLength(50)]
        public string ActorName { get; set; }
 
        public virtual ICollection<Movie> Movies{ get; set; } = new List<Movie>();
    }
}
