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
    public class ActorRepository : Repository<Actor>,IActorRepository
    {
        public ActorRepository(MovieWebDbContext context) : base(context)

        {
        }

        public void Update(Actor actor)
        {
            _dbContext.Update(actor);
        }
    }
}
