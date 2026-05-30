namespace ccballot;

public class Snapshot
{
    public int CastInBox { get; set; }
    public int Provisionals { get; set; }
    public int Spoiled { get; set; }
    public int Unused { get; set; }
    public int Signatures { get; set; }
}

public class SectionData
{
    public Snapshot Sheets { get; set; } = new();
    public Snapshot Cards { get; set; } = new();
    public int PublicCount { get; set; }
}
