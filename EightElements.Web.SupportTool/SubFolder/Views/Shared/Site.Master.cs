using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EightElements.Web.SupportTool.Helpers;
using System.Web.Security;
using log4net;

namespace EightElements.Web.SupportTool.Views.Shared
{
    public partial class Site : System.Web.UI.MasterPage
    {
        #region Fields
        private static readonly ILog log = LogManager.GetLogger(
                                                           typeof(Site));
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                logoutLink.Visible = true;
                welcome.Visible = true;
                username.Visible = true;
                //Khoa.Nguyen added:GetAuthenticatedUser
                
                //var formsIdentity = HttpContext.Current.User.Identity as FormsIdentity;         
                if (SessionHandler.UserInfo != null)
                {
                    this.username.Text = SessionHandler.UserInfo.CMSUser.FullName;
                }
                else
                {
                    log.Info("User Information in Session is null");
                }
            }
            else
            {
                //If the user is not logged 
                if (!this.Request.Url.ToString().EndsWith("/User/Login") && !this.Request.Url.ToString().EndsWith("/Home/Error"))
                    Response.Redirect("/User/Login");
                logoutLink.Visible = false;
                welcome.Visible = false;
                username.Visible = false;
            }
        }
        #endregion
    }
}
