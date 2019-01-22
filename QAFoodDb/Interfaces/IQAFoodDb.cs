using Microsoft.EntityFrameworkCore;
using System;

namespace QAFoodDb
{
    public interface IQAFoodDb : IDisposable
    {
        int SaveChanges();
        DbSet<User> Users { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Food> Foods { get; set; }
        DbSet<ReviewDetails> ReviewDetails { get; set; }
    }
}
