using DataAccess.Data;
using DataAccess.IRepositories;
using DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Model.Entitys;
using Model.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MovieRepository :Repository<Movie>,IMovieRepository
    {
        public MovieRepository(MovieWebDbContext context) : base(context)

        {
        }
        public async Task AddNewGenreAsync(Movie movie, Guid[] GenreIds, IUnitOfWork _data, MovieWebDbContext _db)
        {
            movie.Genres.Clear();
            foreach (var genreId in GenreIds)
            {
               var genre = await _data.Genre.GetAsync(new QueryOptions<Genre>()
                {
                   Where=c=>c.GenreId.Equals(genreId),
                    Includes = "Movies",
                });
                if (genre is not null)
                {
                    movie.Genres.Add(genre);
                }

            }
        }
        public async Task AddNewActorAsync(Movie movie, Guid[] ActorIds, IUnitOfWork _data, MovieWebDbContext _db)
        {
            movie.Actors.Clear();
            foreach (var actorId in ActorIds)
            {
                var actor = await _data.Actor.GetAsync(new QueryOptions<Actor>()
                {
                    Where=c=>c.ActorId.Equals(actorId),
                    Includes = "Movies",
                });
                if (actor is not null)
                {
                    movie.Actors.Add(actor);
                }

            }
        }
        public void Update(Movie movie)
        {
            _dbContext.Update(movie);
        }
    }
}
