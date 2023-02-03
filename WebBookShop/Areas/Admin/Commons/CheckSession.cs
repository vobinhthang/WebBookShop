using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBookShop.Areas.Admin.Commons
{
    public static class CheckSession
    {
        public static bool LoginSession()
        {
            if (HttpContext.Current.Session["LOGINSESSION"] != null)
            {
                return true;
            }
            return false;
        }
    }
}