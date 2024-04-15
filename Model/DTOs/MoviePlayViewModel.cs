using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class MoviePlayViewModel
    {
        public IEnumerable<MovieDetail> movieDetails { get; set; }
        public IEnumerable<Movie> movies { get; set; }
        public MovieDetail movieDetail { get; set; }
    }
}
