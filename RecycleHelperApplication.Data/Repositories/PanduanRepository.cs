using RecycleHelperApplication.Data.Infrastructure;
using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Data.Repositories
{
    public interface IPanduanRepository : IRepository<Panduan>
    {
    }
    public class PanduanRepository : RepositoryBase<Panduan>, IPanduanRepository
    {
        public PanduanRepository(IDbFactory DbFactory) : base(DbFactory) { }
    }
}
