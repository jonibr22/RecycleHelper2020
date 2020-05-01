using RecycleHelperApplication.Data.Infrastructure;
using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Data.Repositories
{
    public interface IDetailPanduanRepository : IRepository<DetailPanduan>
    {
    }
    public class DetailPanduanRepository : RepositoryBase<DetailPanduan>, IDetailPanduanRepository
    {
        public DetailPanduanRepository(IDbFactory DbFactory) : base(DbFactory) { }
    }
}
