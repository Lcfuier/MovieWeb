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
    public class RatingRepository : Repository<Rating>,IRatingRepository
    {
        public RatingRepository(MovieWebDbContext context) : base(context)

        {
        }

        public void Update(Rating rating)
        {
            _dbContext.Update(rating);
        }
    }
}
