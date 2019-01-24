using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QAFoodDb
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<QAFoodContext>
    {
        public QAFoodContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()               
            .Build();

            var builder = new DbContextOptionsBuilder<QAFoodContext>();

            //string connectionstr = @"server=INVH580\NETSUPPORT; database=QAFoodDb; user id=sa; password=Testing123;"; 
            string connectionstr = @"server = LAPTOP-5URJHDOL; database = QAFoodDb; user id = sa; password = password123;";
            builder.UseSqlServer(connectionstr);

            return new QAFoodContext(builder.Options);
        }
    }
}
