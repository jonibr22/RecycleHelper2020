using RecycleHelperApplication.Model.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        T ExecSPToSingle(string SPName, object[] Param = null);
        Task<T> ExecSPToSingleAsync(string SPName, object[] Param = null);
        IEnumerable<T> ExecSPToList(string SPName, SqlParameter[] Param = null);
        Task<IEnumerable<T>> ExecSPToListAsync(string SPName, SqlParameter[] Param = null);
        Task<ExecuteResult> ExecMultipleSPWithTransaction(List<StoredProcedure> SP);
    }
}
