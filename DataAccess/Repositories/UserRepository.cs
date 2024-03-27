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
    public class UserRepository : Repository<User>,IUserRepository
    {
        public UserRepository(MovieWebDbContext context) : base(context)
        {
        }

        public void Update(User user)
        {
            _dbContext.Update(user);
        }
    }
}
