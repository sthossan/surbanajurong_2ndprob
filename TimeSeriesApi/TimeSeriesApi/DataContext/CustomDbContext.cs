using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection.Emit;
using System.Threading.Tasks;
using TimeSeriesApi.Models;

namespace Models.TimeSeriesApi
{
    public class CustomDbContext : DbContext
    {
        #region Constructor

        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options) { }
        public CustomDbContext() : base() { }

        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // base.OnModelCreating(builder);

            builder.Entity<Building>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BuildingName).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.BuildingLocation).IsRequired().HasColumnType("varchar(50)");
            });

            builder.Entity<TsObject>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TsName).IsRequired().HasColumnType("varchar(50)");
            });

            builder.Entity<DataField>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.DataFieldName).IsRequired().HasColumnType("varchar(50)");
            });

            builder.Entity<Reading>(entity =>
            {
                entity.HasKey(e => new { e.BuildingId, e.ObjectId, e.DatafieldId, e.Timestamp });

                entity.Property(e => e.BuildingId).IsRequired().HasColumnType("smallint");
                entity.Property(e => e.ObjectId).IsRequired().HasColumnType("tinyint");
                entity.Property(e => e.DatafieldId).IsRequired().HasColumnType("tinyint");
                entity.Property(e => e.Value).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Timestamp).IsRequired().HasColumnType("datetime");
            });
        }

        #region DbSet

        public DbSet<Building> Building { get; set; }
        public DbSet<TsObject> TsObject { get; set; }
        public DbSet<DataField> DataField { get; set; }
        public DbSet<Reading> Reading { get; set; }

        #endregion

        #region Transactions

        private IDbContextTransaction _transaction;

        public void BeginTran()
        {
            _transaction = Database.BeginTransaction();
        }
        public async Task BeginTranAsync()
        {
            _transaction = await Database.BeginTransactionAsync();
        }

        public void CommitTran()
        {
            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }
        public async Task CommitTranAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public void RollbackTran()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
        public async Task RollbackTranAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
        #endregion

    }
}
