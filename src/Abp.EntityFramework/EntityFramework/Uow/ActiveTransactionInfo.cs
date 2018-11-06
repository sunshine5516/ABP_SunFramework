﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Abp.EntityFramework.EntityFramework.Uow
{
    public class ActiveTransactionInfo
    {
        public DbContextTransaction DbContextTransaction { get; }
        public DbContext StarterDbContext { get; }
        public List<DbContext> AttendedDbContexts { get; }


        public ActiveTransactionInfo(DbContextTransaction dbContextTransaction, DbContext starterDbContext)
        {
            DbContextTransaction = dbContextTransaction;
            StarterDbContext = starterDbContext;

            AttendedDbContexts = new List<DbContext>();
        }
    }
}
