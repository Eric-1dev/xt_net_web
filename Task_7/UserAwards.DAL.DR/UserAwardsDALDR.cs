using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using UserAwards.DAL.Files;
using UserAwards.DAL.Interfaces;
using UserAwards.DAL.MSSQL;

namespace UserAwards.DAL.DR
{
    public static class UserAwardsDALDR
    {
        private static IUserAwardsDAL _userAwardsDAL;

        public static IUserAwardsDAL UserAwardsDAL => _userAwardsDAL ?? (_userAwardsDAL = new MSQSQLUserAwardsDAL());
        //public static IUserAwardsDAL UserAwardsDAL => _userAwardsDAL ?? (_userAwardsDAL = new FileUserAwardsDAL());
    }
}
