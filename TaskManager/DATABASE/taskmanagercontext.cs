using Microsoft.EntityFrameworkCore;
using TaskManager.Modules;

namespace TaskManager.DATABASE
{
    public class taskmanagercontext : DbContext
    {
        public taskmanagercontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; } 
        public DbSet<User> users { get; set; }
        public DbSet<Address> addresses { get; set; }

        public DbSet<UserLogin> usersLogin { get; set; }


        protected override void
        OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(p=>p.Address)
                .WithOne(p=>p.User)
                .HasForeignKey<Address>(p=>p.UserId);


            modelBuilder.Entity<User>()
                .HasMany(o=>o.TaskItems)
                .WithOne(p=>p.User)
                .HasForeignKey(p=>p.UserId);

            modelBuilder.Entity<TaskItem>()
                .HasMany(o=>o.cheakLists)
                .WithOne(o=>o.Task)
                .HasForeignKey(p=>p.TaskId );

            base.OnModelCreating(modelBuilder);
        }

        


    }
}
