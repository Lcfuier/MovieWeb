using DataAccess.Data;
using DataAccess.IRepositories;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MovieDetailRepository : Repository<MovieDetail>, IMovieDetailRepository
    {
        public MovieDetailRepository(MovieWebDbContext context) : base(context)

        {
        }

        public void Update(MovieDetail movieDetail)
        {
            _dbContext.Update(movieDetail);
        }
    }
}
