using DataAccessLibrary.Interface;


namespace DataAccessLibrary.Models
{
    public class SteamDetailsModel : ISteamDetailsModel
    {
        public int ID { get; set; }
        public string SteamID { get; set; }
        public string SteamReview { get; set; }
        public int SteamReviewCount { get; set; }
    }
}
