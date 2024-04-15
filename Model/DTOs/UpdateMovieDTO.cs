using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class UpdateMovieDTO
    {
        public MovieDTO movieDto { get; set; } = new();
        public IEnumerable<Actor> actors { get; set; } = new List<Actor>();
        public IEnumerable<Genre> genres { get; set; } = new List<Genre>();
    }
}
