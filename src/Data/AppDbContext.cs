using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ccballot.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<RevisedBallotRecord> BallotRecords => Set<RevisedBallotRecord>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<RevisedBallotRecord>(entity =>
        {
            entity.ToTable("BallotRecords");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DateSaved).IsRequired();
            entity.Property(e => e.PageDate).IsRequired();
            entity.Property(e => e.DayNumber).IsRequired();
            entity.Property(e => e.IsElectionDay).IsRequired();
            entity.Property(e => e.DeliveredSheets).IsRequired();
            entity.Property(e => e.DeliveredCards).IsRequired();

            entity.OwnsOne(e => e.Beg, beg =>
            {
                beg.OwnsOne(s => s.Sheets);
                beg.OwnsOne(s => s.Cards);
            });

            entity.OwnsOne(e => e.Daily, daily =>
            {
                daily.OwnsOne(s => s.Sheets);
                daily.OwnsOne(s => s.Cards);
            });

            entity.OwnsOne(e => e.Eod, eod =>
            {
                eod.OwnsOne(s => s.Sheets);
                eod.OwnsOne(s => s.Cards);
            });
        });
    }
}
