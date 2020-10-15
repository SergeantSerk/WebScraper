using DataAccessLibrary.Models;
using System;

namespace DataAccessLibrary.Interface
{
    public interface IGameModel
    {
        string About { get; set; }
        string Developer { get; set; }
        int ID { get; set; }
        string Publisher { get; set; }
        DateTime ReleaseDate { get; set; }
        int SteamDetailsID { get; set; }
        string Thumbnail { get; set; }
        string Title { get; set; }

     
    }
}