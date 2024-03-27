using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    public class MovieGenre
    {
        [Key]
        [Required]
        public Guid MovieId { get; set; }
        [Key]
        [Required]
        public Guid GenreId { get; set; }
        public Movie? Movie { get; set; }
        public Genre? Genre { get; set; }
    }
}
