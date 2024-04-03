using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    public class Genre
    {
        [Key]
        public Guid GenreId { get; set; }
        public string GenreName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    }
}
