using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EightElements.Web.SupportTool.Domain.Models;
using System.Data.Linq;
using System.Web.Security;
using System.Web.Mvc;
using log4net;

namespace EightElements.Web.SupportTool.Domain
{
    public class UserRepository
    {        
        #region Constructor
        public UserRepository()
        {
            _dbContext = new DbContext(Settings.DatabaseConnectionString);
        }
        #endregion

        #region Public methods
        public UserInfo GetUserInfoByUsername(string userName)
        {
            UserInfo userInfo = new UserInfo();
            try
            {
                //execute store
                IMultipleResults result = _dbContext.GetManyTables("SupportTool_GetUserInfoByUserName",
                                                                    userName);
                //get result
                userInfo.CMSUser = result.GetResult<User>().FirstOrDefault<User>();
                userInfo.Portals = result.GetResult<Portal>().ToList<Portal>();
            }
            catch (Exception exp)
            {
                log.Error("Get User information", exp);
            }

            //return
            return userInfo;
        }

        public bool UpdatePortalSetting(int userId,
                                        int portalId,
                                        string initNumber,
                                        string cancelRemark)
        {
            long result = 0;

            try
            {
                result = _dbContext.ExecuteScalarQuery<long>("SupportTool_UpdatePortalSetting",
                                                             userId,
                                                             portalId,
                                                             initNumber,
                                                             cancelRemark);
            }
            catch (Exception exp)
            {
                log.Error("Update portal setting", exp);
            }

            return result > 0;
        }

        public IList<Portal> GetAllActivePortals()
        {
            List<Portal> result = new List<Portal>();
            try
            {
                result = _dbContext.GetManyRecords<Portal>("SupportTool_GetAllActivePortal")
                                                          .ToList();
            }
            catch (Exception exp)
            {
                log.Error("Get all active portals", exp);
            }

            return result;
        }

        public bool AddUserPortal(string username, int potalId)
        {
            long result = 0;
            try
            {
                result = _dbContext.ExecuteScalarQuery<long>("SupportTool_AddUserPortal",
                                                             username,
                                                             potalId);
            }
            catch (Exception exp)
            {
                log.Error("Add portal to user", exp);
            }

            return result > 0;
        }

        public bool DeleteUserPortal(int userId, int portalId)
        {
            long result = 0;
            try
            {
                result = _dbContext.ExecuteScalarQuery<long>("SupportTool_DeleteUserPortal",
                                                              userId,
                                                              portalId);
            }
            catch (Exception exp)
            {
                log.Error("Delete portal of user ", exp);
            }
            //retun
            return result > 0;
        }
        #endregion

        #region Fields
        private readonly DbContext _dbContext;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserRepository));
        #endregion
    }
}