
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace EightElements.Web.SupportTool.Domain
{
    public interface IDbContext
    {
        /// <summary>
        /// Execute an store procedure against the database to get one or more record set
        /// </summary>
        /// <typeparam name="TResultType"></typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        TResultType GetSingleRecord<TResultType>(string storeProcedure, 
                                                 params object[] parameters);

        /// <summary>
        /// Execute a store procedure against the database to get multiple record set.
        /// </summary>
        /// <typeparam name="TResultType"></typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<TResultType> GetManyRecords<TResultType>(string storeProcedure, 
                                                             params object[] parameters) 
                                                             where TResultType : class;

        /// <summary>
        /// Execute multiple query (width one store procedure) in single connection. 
        /// This's useful when execute one SP several times 
        /// with different values of input parameters
        /// </summary>
        /// <typeparam name="TResultType"></typeparam>
        /// <param name="storeProcedure"></param>
        /// <param name="paramsList"></param>
        /// <returns></returns>
        IEnumerable<TResultType> GetManyRecordsWithPrams<TResultType>(
                                                                    string storeProcedure, 
                                                                    List<List<object>> paramsList) 
                                                                    where TResultType : class;

        /// <summary>
        /// Execute a store procedure against the database to get multiple table like DataSet
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns>IMultipleResults type </returns>
        IMultipleResults GetManyTables(string storeProcedure, params object[] parameters);

        /// <summary>
        /// Execute storeprocedure with many parameter
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="paramsList"></param>
        void ExecuteQuery(string storeProcedure, List<List<Object>> paramsList);

        void ExecuteQuery(string storeProcedure, params object[] param);

        TResultType ExecuteScalarQuery<TResultType>(string storeProcedure, 
                                                    params object[] parameters);

        object ExcuteScalarQuery(string storeProcedure, params object[] parameters);

    }

    public class DbContext : System.Data.Linq.DataContext, IDbContext, IDisposable
    {      
        #region Constructor
        public DbContext(string connectionString) :
            base(connectionString, mappingSource)
        {
        }
        #endregion

        #region Private methods
        private Type[] GetPropertyTypes<T>(T obj)
            where T : class
        {
            Type type = obj.GetType();
            return type.GetProperties().Where(p =>
                    !p.PropertyType.IsClass
                    || p.PropertyType.IsValueType
                    || p.PropertyType == typeof(String)
                ).Select(p => p.PropertyType).ToArray();
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Procedure name must be not empty");
            if (name.ToLower().StartsWith("exec"))
                throw new Exception("Illegal of store procedure");
            if (name.Trim().Split(' ').Length != 1)
                throw new Exception("Illegal of store procedure");
        }
        private string BuildTextCommand<T>(string storeProc, T obj)
            where T : class
        {
            this.ValidateName(storeProc);

            Type[] types = this.GetPropertyTypes<T>(obj);
            int length = types.Length;
            string command = string.Format("EXEC {0} ", storeProc);
            for (int i = 0; i < length; i++)
            {
                if (i < length - 1)
                    command += "{" + i.ToString() + "},";
                if (i == length - 1)
                    command += "{" + i.ToString() + "}";
            }

            return command;
        }

        private string BuildTextCommand(string storeProc, params object[] parameters)
        {
            this.ValidateName(storeProc);

            int length = parameters.Length;
            string command = string.Format("EXEC {0} ", storeProc);
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                if ((parameters[i] == null || parameters[i] is System.DBNull))
                {
                    if (i < length - 1)
                        command += "NULL,";
                    if (i == length - 1)
                        command += "NULL";
                }
                else
                {
                    //string f = "{0}";
                    //if (parameters[i].GetType() == typeof(string))
                    //    f = "N'{0}'"; // Fixed problem when insert unicode characters

                    if (i < length - 1)
                        command += "{" + index.ToString() + "},";
                    if (i == length - 1)
                        command += "{" + index.ToString() + "}";
                    index++;
                }
            }

            return command;
        }

        private string BuildBatchCommand(string storeProc, 
                                         List<List<object>> paramList, 
                                         List<object> outputParams)
        {
            this.ValidateName(storeProc);

            string exeCommand = string.Format("EXEC {0} ", storeProc);
            string command = string.Empty;

            System.Text.StringBuilder commandBuilder = new System.Text.StringBuilder();
            int index = 0,
                i = 0,
                length = 0;

            foreach (IEnumerable<object> itemList in paramList)
            {
                length = itemList.Count();
                i = 0;
                command = string.Empty;

                foreach (object item in itemList)
                {
                    if (item == null || item is System.DBNull)
                    {
                        if (i < length - 1)
                            command += "NULL,";
                        if (i == length - 1)
                            command += "NULL";
                    }
                    else
                    {
                        if (i < length - 1)
                            command += "{" + index.ToString() + "},";
                        if (i == length - 1)
                            command += "{" + index.ToString() + "}";

                        outputParams.Add(item);

                        index++;
                    }
                    i++;
                }

                commandBuilder.AppendLine(exeCommand + command);
            }

            return commandBuilder.ToString();
        }

        private object[] EnsureParams(object[] parameters)
        {
            List<object> objList = new List<object>();
            foreach (var p in parameters)
            {
                if ((p != null && !(p is System.DBNull)))
                {
                    objList.Add(p);
                }
            }

            return objList.ToArray();
        }
        #endregion Private methods

        #region IDatabase Members

        public object ExcuteScalarQuery(string storeProcedure, params object[] parameters)
        {
            var query = BuildTextCommand(storeProcedure, parameters);
            object[] param = EnsureParams(parameters);
            object result = this.ExecuteQuery<long>(query, param).FirstOrDefault();

            return result;
        }
        public TResultType ExecuteScalarQuery<TResultType>(string storeProcedure, 
                                                           params object[] parameters)
        {
            return this.ExecuteQuery<TResultType>(BuildTextCommand(storeProcedure, parameters), 
                                                  EnsureParams(parameters)).FirstOrDefault();
        }

        public TResultType GetSingleRecord<TResultType>(string storeProcedure, 
                                                        params object[] parameters)
        {
            return this.ExecuteQuery<TResultType>(BuildTextCommand(storeProcedure, parameters), 
                                                  EnsureParams(parameters)).FirstOrDefault();
        }

        public IEnumerable<TResultType> GetManyRecords<TResultType>(string storeProcedure, 
                                                                    params object[] parameters) 
                                                                    where TResultType : class
        {
            return this.ExecuteQuery<TResultType>(BuildTextCommand(storeProcedure, parameters), 
                                                  EnsureParams(parameters));
        }

        public IEnumerable<TResultType> GetManyRecordsWithPrams<TResultType>(
                                                                    string storeProcedure, 
                                                                    List<List<object>> paramsList) 
                                                                    where TResultType : class
        {
            DbConnection connection = new SqlConnection(this.Connection.ConnectionString);
            connection.Open();
            DbCommand cmd = connection.CreateCommand();
            List<List<object>> paras = new List<List<object>>(paramsList.Count);
            List<object> paramsOutput = new List<object>();
            int numberOfQuery = 0;

            foreach (List<object> listPara in paramsList)
            {
                ++numberOfQuery;

                List<object> newParam = new List<object>();
                foreach (object pr in listPara)
                {

                    if (pr != null && pr.GetType() == typeof(String))
                    {
                        newParam.Add(string.Format(CultureInfo.CreateSpecificCulture("en-US"), 
                                                   "N'{0}'", 
                                                   ((String)pr).Replace("'", "''")));
                    }
                    else if (pr != null && pr.GetType() == typeof(DateTime))
                    {
                        newParam.Add(string.Format(CultureInfo.CreateSpecificCulture("en-US"), 
                                                   "'{0:G}'", 
                                                   pr));
                    }
                    else
                        newParam.Add(pr);
                }
                paras.Add(newParam);
            }
            string command = this.BuildBatchCommand(storeProcedure, paras, paramsOutput);
            cmd.CommandText = string.Format(command, paramsOutput.ToArray());
            cmd.CommandTimeout = 60;
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            IMultipleResults multiResults = this.Translate(reader);

            if (multiResults != null)
            {
                List<TResultType> results = new List<TResultType>();

                for (int i = 0; i < numberOfQuery; i++)
                {
                    IEnumerable<TResultType> lst = multiResults.GetResult<TResultType>();

                    if (lst != null)
                        results.AddRange(lst);
                }

                return results;
            }
            return null;
        }

        public void ExecuteQuery(string storeProcedure, List<List<Object>> paramsList)
        {
            DbConnection connection = new SqlConnection(this.Connection.ConnectionString);
            connection.Open();
            DbCommand cmd = connection.CreateCommand();
            List<List<object>> paras = new List<List<object>>(paramsList.Count);
            List<object> paramsOutput = new List<object>();
            int numberOfQuery = 0;

            foreach (List<object> listPara in paramsList)
            {
                ++numberOfQuery;

                List<object> newParam = new List<object>();
                foreach (object pr in listPara)
                {

                    if (pr != null && pr.GetType() == typeof(String))
                    {
                        newParam.Add(string.Format(CultureInfo.CreateSpecificCulture("en-US"), 
                                                   "N'{0}'", 
                                                   ((String)pr).Replace("'", "''")));
                    }
                    else if (pr != null && pr.GetType() == typeof(DateTime))
                    {
                        newParam.Add(string.Format(CultureInfo.CreateSpecificCulture("en-US"), 
                                                   "'{0:G}'", 
                                                   pr));
                    }
                    else
                        newParam.Add(pr);
                }
                paras.Add(newParam);
            }
            string command = this.BuildBatchCommand(storeProcedure, paras, paramsOutput);
            cmd.CommandText = string.Format(command, paramsOutput.ToArray());
            cmd.CommandTimeout = 60;

            cmd.ExecuteNonQuery();
        }

        public void ExecuteQuery(string storeProcedure, params object[] parameters)
        {
            this.ExecuteCommand(BuildTextCommand(storeProcedure, parameters), 
                                EnsureParams(parameters));
        }

        public IMultipleResults GetManyTables(string storeProcedure, params object[] parameters)
        {
            DbConnection connection = new SqlConnection(this.Connection.ConnectionString);
            connection.Open();
            DbCommand cmd = connection.CreateCommand();
            object[] param = new object[parameters.Length];
            int i = 0;
            foreach (object pr in parameters)
            {
                if (pr.GetType() == typeof(String))
                {
                    param[i] = string.Format(CultureInfo.CreateSpecificCulture("en-US"), 
                                             "N'{0}'", 
                                             ((String)pr).Replace("'", "''"));
                }
                else if (pr.GetType() == typeof(DateTime))
                {
                    param[i] = string.Format(CultureInfo.CreateSpecificCulture("en-US"), 
                                             "'{0:G}'", 
                                             pr);
                }
                else
                    param[i] = pr;

                ++i;
            }
            cmd.CommandText = string.Format(BuildTextCommand(storeProcedure, parameters), param);
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return this.Translate(reader);
        }

        #endregion

        #region Fields
        private static MappingSource mappingSource = new AttributeMappingSource();
        #endregion
    }
}