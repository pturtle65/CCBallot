public class BallotRecord
{
    public int Id { get; set; }
    public DateTime DateSaved { get; set; } = DateTime.Now;

    public DateTime PageDate { get; set; }
    public int DayNumber { get; set; }
    public bool IsElectionDay { get; set; }

    public int DeliveredSheets { get; set; }
    public int DeliveredCards { get; set; }

    public int BegProvInBagSheets { get; set; }
    public int BegProvInBagCards { get; set; }
    public int BegSpoiledInEnvsSheets { get; set; }
    public int BegSpoiledInEnvsCards { get; set; }
    public int BegUnusedSheets { get; set; }
    public int BegUnusedCards { get; set; }
    public int BegPublicCount { get; set; }
    public int BegCastInBoxSheets { get; set; }
    public int BegCastInBoxCards { get; set; }

    public int SigsTodaySheets { get; set; }
    public int SigsTodayCards { get; set; }
    public int ProvTodaySheets { get; set; }
    public int ProvTodayCards { get; set; }
    public int SpoiledTodaySheets { get; set; }
    public int SpoiledTodayCards { get; set; }
    public int UnusedTodaySheets { get; set; }
    public int UnusedTodayCards { get; set; }
    public int CastInBoxTodaySheets { get; set; }
    public int CastInBoxTodayCards { get; set; }

    public int EodProvInBagSheets { get; set; }
    public int EodProvInBagCards { get; set; }
    public int EodSpoiledInEnvsSheets { get; set; }
    public int EodSpoiledInEnvsCards { get; set; }
    public int EodUnusedSheets { get; set; }
    public int EodUnusedCards { get; set; }
    public int EodCastInBoxSheets { get; set; }
    public int EodCastInBoxCards { get; set; }
    public int EodPublicCount { get; set; }
}
