﻿using RecycleHelperApplication.Data.Infrastructure;
using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
    }
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IDbFactory DbFactory) : base(DbFactory) { }
    }
}
