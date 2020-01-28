using Microsoft.EntityFrameworkCore;
using PushFive.Core.Data;
using PushFive.Voting.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PushFive.Voting.Data
{
    public class VotingContext : DbContext, IUnitOfWork
    {
        public VotingContext(DbContextOptions<VotingContext> options) : base(options)
        {
        }

        public DbSet<Domain.Models.Voting> Votings { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<VotingItem> VotingItems { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(64)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VotingContext).Assembly);
        }
    }
}
