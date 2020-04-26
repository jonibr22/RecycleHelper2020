using RecycleHelperApplication.Data.Infrastructure;
using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Data.Repositories
{
    public interface IBahanRepository : IRepository<Bahan>
    {
    }
    public class BahanRepository : RepositoryBase<Bahan>, IBahanRepository
    {
        public BahanRepository(IDbFactory DbFactory) : base(DbFactory) { }
    }
}
