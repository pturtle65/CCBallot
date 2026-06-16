using Microsoft.EntityFrameworkCore;
using ccballot.Data;

namespace ccballot.Services;

public class SSStorageService : IDbStorageService
{
    private readonly AppDbContext _db;

    public SSStorageService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<RevisedBallotRecord>> GetRecordsAsync()
    {
        return await _db.BallotRecords
            .OrderByDescending(r => r.DayNumber)
            .ToListAsync();
    }

    public async Task AddRecordAsync(RevisedBallotRecord record)
    {
        record.Id = 0;
        _db.BallotRecords.Add(record);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteRecordAsync(int id)
    {
        var record = await _db.BallotRecords.FindAsync(id);
        if (record != null)
        {
            _db.BallotRecords.Remove(record);
            await _db.SaveChangesAsync();
        }
    }

    public async Task ReplaceAllAsync(List<RevisedBallotRecord> records)
    {
        _db.BallotRecords.RemoveRange(_db.BallotRecords);
        foreach (var record in records)
        {
            record.Id = 0;
        }
        await _db.BallotRecords.AddRangeAsync(records);
        await _db.SaveChangesAsync();
    }
}
