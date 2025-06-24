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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
       // private readonly AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext appDbContext):base(appDbContext)
        {
            //_appDbContext = appDbContext;
        }
        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _appDbContext.Employees
                .Where(e => e.Address.ToLower().Contains(address.ToLower()));
        }
    }
}
