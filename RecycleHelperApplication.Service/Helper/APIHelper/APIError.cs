using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Service.Helper.APIHelper
{
    public class APIError
    {
        public static object From(InternalServerErrorException e)
        {
            return new
            {
                Status = new
                {
                    Code = e.Code,
                    Message = "Internal Server Error"
                },
                Message = e.Message
            };
        }
        public static object From(UnauthorizedException e)
        {
            return new
            {
                Status = new
                {
                    Code = e.Code,
                    Message = "Unauthorized"
                },
                Message = e.Message
            };
        }
        public static object From(NotPermittedException e)
        {
            return new
            {
                Status = new
                {
                    Code = e.Code,
                    Message = "Not Permitted"
                },
                Message = e.Message
            };
        }
        public static object From(NotFoundException e)
        {
            return new
            {
                Status = new
                {
                    Code = e.Code,
                    Message = "Entity Not Found"
                },
                Message = e.Message
            };
        }
    }
}
