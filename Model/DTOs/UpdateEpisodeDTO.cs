using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class UpdateEpisodeDTO
    {
        public MovieDetailDTO MovieDetailDTO { get; set; }
        public IEnumerable<Movie> movies { get; set; } = new List<Movie>();
    }
}
