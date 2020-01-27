namespace Server.DataBaseCore
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Storage;
    using Server.DataBaseCore.Entities;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly ClaimsPrincipal principal;

        private string userName;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPrincipal principal)
        //    : base(options)
        //{
        //    this.principal = principal as ClaimsPrincipal;
        //}

        public DbSet<Migration> Migrations { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Token> Tokens { get; set; }

        #region Transactions

        public virtual IDbContextTransaction BeginTransaction()
        {
            return this.Database.BeginTransaction();
        }

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.Database.BeginTransactionAsync(cancellationToken);
        }

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default(CancellationToken))
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

           // this.ApplicationEntityDefaultValueSqlForAllObjects(builder);

            //builder.Entity<UserAcceptedTerm>()
            //    .HasKey(t => new { t.UserId, t.TermsOfServiceId });
        }

        internal string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(this.userName))
                {
                    this.userName = this.principal?.Identity?.Name ?? "Anonymous";
                }

                return this.userName;
            }

            set => this.userName = value;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.AddTimestamps();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            this.AddTimestamps();

            return base.SaveChanges();
        }

        internal void ApplicationEntityDefaultValueSqlForAllObjects(ModelBuilder builder)
        {
            // Execute ApplicationEntityDefaultValueSql for every child of the ApplicationEntity
            var applicationEntityTypes = typeof(EntityDataType).GetTypeInfo().Assembly.GetTypes().Where(
                t => t.GetTypeInfo().IsClass
                && typeof(EntityDataType).IsAssignableFrom(t)
                && !t.GetTypeInfo().IsAbstract
                && t.GetTypeInfo().IsSubclassOf(typeof(EntityDataType)));

            foreach (Type type in applicationEntityTypes)
            {
                var dbSetType = this.GetType()
                    .GetRuntimeProperties()
                    .Where(o => o.PropertyType.GetTypeInfo().IsGenericType && o.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) && o.PropertyType.GenericTypeArguments.Contains(type))
                    .FirstOrDefault();

                if (dbSetType != null)
                {
                    MethodInfo method = typeof(ApplicationDbContext).GetMethod("ApplicationEntityDefaultValueSql", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (method == null)
                    {
                        throw new NotImplementedException("The 'ApplicationEntityDefaultValueSql' method is not implemented");
                    }

                    MethodInfo generic = method.MakeGenericMethod(type);
                    generic?.Invoke(this, new object[] { builder });
                }
            }
        }



        private ModelBuilder ApplicationEntityDefaultValueSql<TEntity>(ModelBuilder builder)
            where TEntity : EntityDataType
        {

            string currentDate = "GETDATE()";

            return builder?.Entity<TEntity>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql(currentDate);
                entity.Property(e => e.UpdatedDate).HasComputedColumnSql(currentDate);
                entity.Property(e => e.IsActive).HasDefaultValueSql("1");
            });
        }

        private void AddTimestamps()
        {
            var entities = this.ChangeTracker.Entries().Where(x => x.Entity is EntityDataType && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var entityEntries = entities as EntityEntry[] ?? entities.ToArray();

            if (!entityEntries.Any())
            {
                return;
            }

            var currentUser = this.UserName;

            foreach (EntityEntry entity in entityEntries)
            {
                if (entity.State == EntityState.Added)
                {
                    // ((EntityDataType)entity.Entity).CreatedDate = currentUser;

                    if (!((EntityDataType)entity.Entity).IsActive/*.HasValue*/)
                    {
                        ((EntityDataType)entity.Entity).IsActive = true;
                        ((EntityDataType)entity.Entity).CreatedDate = DateTime.Now;
                        ((EntityDataType)entity.Entity).UpdatedDate = ((EntityDataType)entity.Entity).CreatedDate;
                    }
                }
                else
                {
                    //entity.Property("CreatedBy").IsModified = false;

                    // ((EntityDataType)entity.Entity).UpdatedDate = currentUser;

                    ((EntityDataType)entity.Entity).UpdatedDate = DateTime.Now;
                }
            }
        }
    }
}