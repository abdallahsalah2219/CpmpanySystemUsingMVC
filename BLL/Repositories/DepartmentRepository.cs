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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _appDbContext;
        public DepartmentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public int Add(Department entity)
        {
            _appDbContext.Departments.Add(entity);
            return _appDbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
           _appDbContext.Departments.Update(entity);
            return _appDbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _appDbContext.Departments.Remove(entity);
            return _appDbContext.SaveChanges();
        }

        public Department Get(int id)
        {
            ///var department = _appDbContext.Departments.Local.Where(D=>D.Id==id).FirstOrDefault();
            ///  if (department is null)
            ///      department = _appDbContext.Departments.Where(D => D.Id == id).FirstOrDefault();
            ///  return department;
            ///  
            // return _appDbContext.Departments.Find(id);
            return _appDbContext.Find<Department>(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _appDbContext.Departments.AsNoTracking().ToList();
        }

        
    }
}
