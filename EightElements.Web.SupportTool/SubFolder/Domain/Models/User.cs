using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EightElements.Web.SupportTool.Domain.Models
{
    public class UserInfo
    {
        public User CMSUser { get; set; }

        public IList<Portal> Portals { get; set; }
    }

    public class User
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// FullName
        /// </summary>
        public string FullName { get; set; }
    }

    public class Portal
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Currency { get; set; }

        public string InitMsisdnNumber { get; set; }

        /// <summary>
        /// Remart will insert when user cancel subscription
        /// </summary>
        public string CancelRemark { get; set; }
    }
}
