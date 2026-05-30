using ccballot;

public class RevisedBallotRecord
{
    public int Id { get; set; }
    public DateTime DateSaved { get; set; } = DateTime.Now;
    public DateTime PageDate { get; set; } = DateTime.Today;
    public int DayNumber { get; set; }
    public bool IsElectionDay { get; set; }

    public int DeliveredSheets { get; set; }
    public int DeliveredCards { get; set; }

    public SectionData Beg { get; set; } = new();
    public SectionData Daily { get; set; } = new();
    public SectionData Eod { get; set; } = new();
}
