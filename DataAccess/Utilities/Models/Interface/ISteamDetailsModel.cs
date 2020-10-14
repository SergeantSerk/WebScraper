namespace DataAccessLibrary.Interface
{
    public interface ISteamDetailsModel
    {
        int ID { get; set; }
        string SteamID { get; set; }
        string SteamReview { get; set; }
        int SteamReviewCount { get; set; }
    }
}