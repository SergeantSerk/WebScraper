
using BusinessAccessLibrary.BusinessLogic;
using BusinessAccessLibrary.Interfaces;
using DataAccessLibrary.DataAccess.DBAccessFactory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessAccessLibrary.Factories
{
    public static class BALFactory
    {

       public static IGameManager GetGameManager()
        {
            return new GameManager(
                DALFactory.GetGameDBAccess(),
                DALFactory.GetReleaseDateDBAccess(),
                DALFactory.GetSteamAppDbAccess(),
                DALFactory.GetTagsDBAccess()
                );
        }
     

    }
}
