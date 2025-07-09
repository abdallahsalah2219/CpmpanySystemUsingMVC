using BLL.Interfaces;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            EmployeeRepository = new EmployeeRepository(_appDbContext);
            DepartmentRepository = new DepartmentRepository(_appDbContext);

        }
       

        public int Complete()
        {
            return _appDbContext.SaveChanges();
        }
        public void Dispose() 
        {
            _appDbContext.Dispose();
        }
    }
}
