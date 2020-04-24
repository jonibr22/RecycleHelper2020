using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RecycleHelperApplication.WebAPI.Models
{
    public class APIResult
    {
        public static object ResultSuccessStatus = new
        {
            Code = 200,
            Message = "Success"
        };

        public static object ResultNotFoundStatus = new
        {
            Code = 204,
            Message = "Not Found"
        };

        public static object ResultInternalErrorStatus = new
        {
            Code = 500,
            Message = "Internal Server Error"
        };

        public static object Error(HttpStatusCode code, string errMsg, string IDRequest)
        {
            return new
            {
                Result = new
                {
                    Status = new
                    {
                        Code = code,
                        Message = errMsg
                    },
                    IDRequest = IDRequest
                }
            };
        }
    }
}