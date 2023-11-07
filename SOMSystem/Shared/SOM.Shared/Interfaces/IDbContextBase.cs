﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM.Shared.Interfaces
{
    public interface IDbContextBase : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    }
}
