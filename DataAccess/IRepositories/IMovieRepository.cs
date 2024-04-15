using DataAccess.Data;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IMovieRepository:IRepository<Movie>
    {
        void Update(Movie movie);
        Task AddNewGenreAsync(Movie movie, Guid[] GenreIds, IUnitOfWork _data, MovieWebDbContext _db);
        Task AddNewActorAsync(Movie movie, Guid[] ActorIds, IUnitOfWork _data, MovieWebDbContext _db);
    }
}
