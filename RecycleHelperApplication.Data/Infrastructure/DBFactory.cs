using RecycleHelperApplication.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Data.Infrastructure
{
    public class DBFactory : Disposable, IDbFactory
    {
        ApplicationContext ApplicationContext;

        public ApplicationContext Init()
        {
            return ApplicationContext ?? (ApplicationContext = new ApplicationContext());
        }

        protected override void DisposeCore()
        {
            if (ApplicationContext != null)
            {
                ApplicationContext.Dispose();
            }
        }
    }
}
