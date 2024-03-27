using DataAccess.Data;
using DataAccess.IRepositories;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieWebDbContext _context;
        public IActorRepository Actor { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public IMovieRepository Movie { get; private set; }
        public IRatingRepository Rating { get; private set; }
        public IUserRepository User { get; private set; }
        public UnitOfWork(MovieWebDbContext context)
        {
            _context = context;
            Actor = new ActorRepository(_context);
            Genre = new GenreRepository(_context);
            Movie = new MovieRepository(_context);
            Rating = new RatingRepository(_context);
            User = new UserRepository(_context);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
