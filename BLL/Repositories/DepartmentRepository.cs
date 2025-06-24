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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
       // private readonly AppDbContext _appDbContext;
        public DepartmentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
           // _appDbContext = appDbContext;
        }
    }
}
