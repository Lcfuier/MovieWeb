using Model.DTOs;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Constants
{
    public class CheckGenre
    {
        public static bool IsSelected(MovieDTO movie, Genre genre)
        {
            if (movie.MovieId == Guid.Empty)
            {
                return false;
            }
            return movie.GenreIds.Contains(genre.GenreId);
        }
    }
}
