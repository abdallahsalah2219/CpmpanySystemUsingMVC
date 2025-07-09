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
        public void Add(T entity)
        
           => _appDbContext.Set<T>().Add(entity);
           
        
        public void Update(T entity)
           => _appDbContext.Set<T>().Update(entity);
          
        

        public void Delete(T entity)
       
          => _appDbContext.Set<T>().Remove(entity);
           
        

        public T Get(int id)
        {
            return _appDbContext.Find<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T)==typeof(Employee))
                return(IEnumerable<T>) _appDbContext.Employees.Include(E=>E.Department).AsNoTracking().ToList();
            else
                return _appDbContext.Set<T>().AsNoTracking().ToList();
        }


        
    }
}
