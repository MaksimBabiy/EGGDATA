namespace AdminPanel.DataBaseCore
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using AdminPanelDataBaseCore.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;

    public class AdminDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options)
        : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<FileStorage> FileStorages { get; set; }

        #region Transactions

        public IDbContextTransaction BeginTransaction()
        {
            return this.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            builder.Entity<ApplicationUser>().HasMany(u => u.Tokens).WithOne(i => i.User);

            builder.Entity<Token>().ToTable("Tokens");
            builder.Entity<Token>().Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Entity<Token>().HasOne(i => i.User).WithMany(u => u.Tokens);
        }
    }
}
