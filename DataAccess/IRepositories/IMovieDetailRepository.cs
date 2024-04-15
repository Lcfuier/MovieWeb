using Model.Entitys;
using Model.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IMovieDetailRepository : IRepository<MovieDetail>
    {

        void Update(MovieDetail movieDetail);
    }
}
