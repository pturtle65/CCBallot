namespace ccballot.Services
{
    public interface IDbStorageService
    {
        Task AddRecordAsync(RevisedBallotRecord record);
        Task DeleteRecordAsync(int id);
        Task<List<RevisedBallotRecord>> GetRecordsAsync();
        Task ReplaceAllAsync(List<RevisedBallotRecord> records);
    }
}