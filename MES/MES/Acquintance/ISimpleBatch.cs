namespace MES.Acquintance
{
    public interface ISimpleBatch
    {
        float BatchID { get; set; }

        float BeerType { get; set; }

        float Amount { get; set; }

        string TimeStart { get; set; }

        string TimeEnd { get; set; }

        double OEE { get; set; }

        float Speed { get; set; }
    }
}
