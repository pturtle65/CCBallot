using System.Text.Json;
using Microsoft.JSInterop;

namespace ccballot.Services;

public class LocalStorageService : IDbStorageService
{
    private const string StorageKey = "ccballot_data";
    private readonly IJSRuntime _js;
    private List<RevisedBallotRecord>? _cache;

    public LocalStorageService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<List<RevisedBallotRecord>> GetRecordsAsync()
    {
        if (_cache is null)
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", StorageKey);
            _cache = string.IsNullOrEmpty(json)
                ? new List<RevisedBallotRecord>()
                : JsonSerializer.Deserialize<List<RevisedBallotRecord>>(json) ?? new();
        }
        return _cache;
    }

    private async Task PersistAsync()
    {
        if (_cache is null) return;
        var json = JsonSerializer.Serialize(_cache);
        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }

    public async Task AddRecordAsync(RevisedBallotRecord record)
    {
        var records = await GetRecordsAsync();
        record.Id = records.Count > 0 ? records.Max(r => r.Id) + 1 : 1;
        records.Insert(0, record);
        await PersistAsync();
    }

    public async Task DeleteRecordAsync(int id)
    {
        var records = await GetRecordsAsync();
        records.RemoveAll(r => r.Id == id);
        await PersistAsync();
    }

    public async Task ReplaceAllAsync(List<RevisedBallotRecord> records)
    {
        _cache = records;
        await PersistAsync();
    }
}
