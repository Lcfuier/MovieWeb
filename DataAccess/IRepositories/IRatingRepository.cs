using Model.Entitys;
using Model.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IRatingRepository : IRepository<Rating>
    {

        void Update(Rating rating);
    }
}
