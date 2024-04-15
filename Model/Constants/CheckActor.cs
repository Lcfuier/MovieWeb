using Model.DTOs;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Constants
{
    public class CheckActor
    {
        public static bool IsSelected(MovieDTO movie, Actor actor)
        {
            if (movie.MovieId == Guid.Empty)
            {
                return false;
            }
            return movie.ActorIds.Contains(actor.ActorId);
        }
    }
}
