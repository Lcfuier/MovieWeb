using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    public class MovieActor
    {
        [Key]
        [Required]
        public Guid MovieId { get; set; }
        [Key]
        [Required]
        public Guid ActorId { get; set; }
        public Actor? Actor { get; set; }
        public Genre? Genre { get; set; }   

    }
}
