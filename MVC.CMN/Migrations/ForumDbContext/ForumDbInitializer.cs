﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using MVC.CMN.DataContexts;

namespace MVC.CMN.Migrations.ContextInitializers {
    public class ForumDbInitializer : IDatabaseInitializer<DataContexts.ForumDbContext> {
        public void InitializeDatabase(DataContexts.ForumDbContext context) {
            if (!context.Database.Exists()) {
                // if database did not exist before - create it
                context.Database.Create();
            }
            else {
                // query to check if MigrationHistory table is present in the database 
                var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
                  "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = 'GRUPP1.CMN.Forum' AND table_name = '__MigrationHistory'");

                // if MigrationHistory table is not there (which is the case first time we run) - create it
                if (migrationHistoryTableExists.FirstOrDefault() == 0) {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }
        }
    }
}