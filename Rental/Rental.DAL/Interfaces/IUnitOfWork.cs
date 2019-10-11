﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IIdentityUnitOfWork Identity { get; }

        IRentUnitOfWork Rent { get; }
    }
}
