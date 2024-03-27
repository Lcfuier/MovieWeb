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
    public class GenreRepository : Repository<Genre>,IGenreRepository
    {
        public GenreRepository(MovieWebDbContext context) : base(context)

        {
        }

        public void Update(Genre genre)
        {
            _dbContext.Update(genre);
        }
    }
}
