using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Banque2
{
    public class DefaultDesignTimeDbContextFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                               .SetBasePath(path)
                               .AddJsonFile("appsettings.json");


            var config = builder.Build();

            var connectionString = config.GetConnectionString("DefaultContext");

            DbContextOptionsBuilder<Db> optionBuilder = new DbContextOptionsBuilder<Db>();
            optionBuilder.UseSqlServer(connectionString);

            return new Db(optionBuilder.Options);
        }
    }
}