using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAwards.BLL.Interfaces;

namespace UserAwards.BLL.DR
{
    public static class UserAwardsBLLDR
    {
        private static IUserAwardsBLL _userAwardsBLL;

        public static IUserAwardsBLL UserAwardsBLL => _userAwardsBLL ?? (_userAwardsBLL = new UserAwardsBLL());
    }
}
