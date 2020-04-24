using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RecycleHelperApplication.Service.Helper.APIHelper
{
    public class PropertyGetter
    {
        public static string GetIDRequestFromHeader(HttpContext context)
        {
            return context.Request.Headers.Get("IDRequest");
        }
    }
}
