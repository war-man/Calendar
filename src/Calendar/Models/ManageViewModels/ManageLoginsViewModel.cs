using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http.Authentication; /* .netcore 2.0 */
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Calendar.Models.ManageViewModels
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        /* .netcore 2.0 begin */
        //public IList<AuthenticationDescription> OtherLogins { get; set; }
        public IList<AuthenticationScheme> OtherLogins { get; set; }
        /* .netcore 2.0 end */
    }
}
