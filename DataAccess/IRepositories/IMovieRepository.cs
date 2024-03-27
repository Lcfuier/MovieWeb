﻿using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IMovieRepository:IRepository<Movie>
    {
        void Update(Movie movie);
    }
}
