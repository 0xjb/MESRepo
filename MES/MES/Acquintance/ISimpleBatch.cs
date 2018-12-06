namespace MES.Acquintance
{
    public interface ISimpleBatch
    {
        float BatchID { get; set; }

        float BeerType { get; set; }

        float DesiredAmount { get; set; }

        string TimestampStart { get; set; }

        string TimestampEnd { get; set; }

        double OEE { get; set; }
    }
}
