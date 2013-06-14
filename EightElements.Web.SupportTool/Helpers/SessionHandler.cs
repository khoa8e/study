using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EightElements.Web.SupportTool.Domain.Models;
using System.Web.Security;
using EightElements.Web.SupportTool.Domain;
using System.Web.Mvc;

namespace EightElements.Web.SupportTool.Helpers
{
    public static class SessionHandler
    {
        #region Fields
        private readonly static string _userInfo = "UserInfo";
        #endregion

        #region Public properties

        public static UserInfo UserInfo
        {
            get
            {
                UserInfo userInfo = new UserInfo();
                if (HttpContext.Current.Session[_userInfo] == null)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        var formsIdentity = HttpContext.Current.User.Identity as FormsIdentity;
                        //this.username.Text = formsIdentity.Ticket.UserData;
                        //get from database
                        UserRepository userRepository = new UserRepository();
                        userInfo = userRepository.GetUserInfoByUsername(formsIdentity
                                                                        .Ticket
                                                                        .UserData);
                    }
                }
                else
                {
                    userInfo = HttpContext.Current.Session[_userInfo] as UserInfo;
                }
                return userInfo;
            }
            set
            {
                HttpContext.Current.Session[_userInfo] = value;
            }
        }

        public static List<SelectListItem> PortalListItem
        {
            get
            {
                List<SelectListItem> listItem = new List<SelectListItem>();

                foreach (Portal p in UserInfo.Portals)
                {
                    listItem.Add(new SelectListItem { Value = p.Id.ToString(), Text = p.Title });
                }
                return listItem;
            }
        }
        #endregion
    }
}