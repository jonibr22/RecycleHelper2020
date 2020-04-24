using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Service.Helper.APIHelper
{
    public class NotFoundException : Exception
    {
        public int Code = 204;
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class InternalServerErrorException : Exception
    {
        public int Code = 500;
        public InternalServerErrorException()
        {
        }

        public InternalServerErrorException(string message)
            : base(message)
        {
        }

        public InternalServerErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class BadRequestException : Exception
    {
        public int Code = 400;
        public BadRequestException()
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class UnauthorizedException : Exception
    {
        public int Code = 401;
        public UnauthorizedException()
        {
        }

        public UnauthorizedException(string message)
            : base(message)
        {
        }

        public UnauthorizedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    public class NotPermittedException : Exception
    {
        public int Code = 402;
        public NotPermittedException()
        {
        }

        public NotPermittedException(string message)
            : base(message)
        {
        }

        public NotPermittedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
