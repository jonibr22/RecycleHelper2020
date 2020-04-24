using RecycleHelperApplication.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ApplicationContext Init();
    }
}
