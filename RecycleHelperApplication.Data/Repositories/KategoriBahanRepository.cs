using RecycleHelperApplication.Data.Infrastructure;
using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Data.Repositories
{
    public interface IKategoriBahanRepository : IRepository<KategoriBahan>
    {
    }
    public class KategoriBahanRepository : RepositoryBase<KategoriBahan>, IKategoriBahanRepository
    {
        public KategoriBahanRepository(IDbFactory DbFactory) : base(DbFactory) { }
    }
}
