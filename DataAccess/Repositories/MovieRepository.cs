using DataAccess.Data;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using Model.Entitys;
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

        public void Update(Movie movie)
        {
            _dbContext.Update(movie);
        }
    }
}
