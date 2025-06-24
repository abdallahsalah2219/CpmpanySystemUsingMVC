using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _appDbContext;
        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public int Add(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            return _appDbContext.SaveChanges();
        }
        public int Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
            return _appDbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
           _appDbContext.Set<T>().Remove(entity);
            return _appDbContext.SaveChanges();
        }

        public T Get(int id)
        {
            return _appDbContext.Find<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _appDbContext.Set<T>().AsNoTracking().ToList();
        }

        
    }
}
