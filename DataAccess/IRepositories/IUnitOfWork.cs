using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IActorRepository Actor { get; }
        IGenreRepository Genre { get; }
        IMovieRepository Movie { get; }
        IRatingRepository Rating { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
