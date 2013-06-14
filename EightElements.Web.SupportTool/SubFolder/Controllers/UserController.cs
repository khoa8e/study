using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EightElements.Web.SupportTool.Domain;
using EightElements.Web.SupportTool.Helpers;
using EightElements.Web.SupportTool.Domain.Models;

namespace EightElements.Web.SupportTool.Controllers
{
    public class UserController : Controller
    {        
        #region Private Methods
        [NonAction]
        private void PreparePortal()
        {
            List<SelectListItem> listPortal = new List<SelectListItem>();
            foreach (Portal p in _userRepository.GetAllActivePortals())
            {
                listPortal.Add(new SelectListItem { Value = p.Id.ToString(), Text = p.Title });
            }
            ViewData["portal"] = listPortal;
        }
        #endregion

        #region Public Methods

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            int failureCount = 0;
            if (HttpContext.Session["LoginFailureCount"] != null)
                failureCount = (int)HttpContext.Session["LoginFailureCount"];
            if (failureCount > 5)
            {
                ViewData["LoginResult"] = "TMF";
                return View();
            }

            string username = collection["username"];
            string password = collection["password"];
            bool createPersistentCookie = false;
            bool.TryParse(collection["rememberme"], out createPersistentCookie);
            var userInfo = _userRepository.GetUserInfoByUsername(username);

            if (userInfo.CMSUser.Password == password)
            {
                SessionHandler.UserInfo = userInfo;
                // Initialize FormsAuthentication, for what it's worth
                FormsAuthentication.Initialize();

                // Create a new ticket used for authentication
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1, // Ticket version
                        username, // Username associated with ticket
                        DateTime.Now, // Date/time issued
                        DateTime.Now.AddMinutes(30), // Date/time to expire
                        createPersistentCookie, // "true" for a persistent user cookie
                        username,// "User", // User-data, in this case the roles
                        FormsAuthentication.FormsCookiePath);// Path cookie valid for

                // Encrypt the cookie using the machine key for secure transport
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(
                                    FormsAuthentication.FormsCookieName, // Name of auth cookie
                                    hash // Hashed ticket  
                                                );

                // Set the cookie's expiration time to the tickets expiration time
                if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                // Add the cookie to the list for outgoing response
                Response.Cookies.Add(cookie);

                // Don't call FormsAuthentication.RedirectFromLoginPage since it
                // could
                // replace the authentication ticket (cookie) we just added
                //Go to the search
                return new RedirectResult("/Home/Index");
            }
            else
            {
                failureCount++;
                ViewData["LoginResult"] = "IUP";
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return new RedirectResult("/User/Login");
        }

        public ActionResult UserProfile()
        {
            UserInfo userInfo = SessionHandler.UserInfo;

            return View(userInfo);
        }


        public JsonResult UpdatePortalSetting(int portalId, string initNumber, string cancelRemark)
        {
            bool result = false;
            if (SessionHandler.UserInfo != null)
            {
                Portal selectedPortal = SessionHandler
                                        .UserInfo
                                        .Portals
                                        .Where(p => p.Id == portalId)
                                        .FirstOrDefault();
                //check old value
                if (selectedPortal.InitMsisdnNumber != initNumber || 
                    selectedPortal.CancelRemark != cancelRemark)
                {
                    //update database
                    _userRepository.UpdatePortalSetting(SessionHandler.UserInfo.CMSUser.ID, 
                                                        portalId, 
                                                        initNumber, 
                                                        cancelRemark);
                    result = true;
                    // update database to Session
                    SessionHandler.UserInfo = _userRepository.GetUserInfoByUsername(
                                              SessionHandler.UserInfo.CMSUser.Username);
                }
            }
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageUser(string username)
        {
            PreparePortal();
            if (username == null)
            {
                username = string.Empty;
            }
            var model = _userRepository.GetUserInfoByUsername(username);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddUserPortal(FormCollection collection)
        {
            string username = collection["username"];

            string submitButton = collection["submitButton"];
            if (submitButton != "Search")
            {
                int portalId = 0;
                int.TryParse(collection["portal"], out portalId);
                _userRepository.AddUserPortal(username, portalId);
            }
            return Redirect("ManageUser?username=" + username);
        }

        public ActionResult DeletePortal(int userId, int portalId, string username)
        {
            _userRepository.DeleteUserPortal(userId, portalId);
            return Redirect("ManageUser?username=" + username);
        }
        #endregion

        #region Fields
        private readonly UserRepository _userRepository = new UserRepository();
        #endregion
    }
}
