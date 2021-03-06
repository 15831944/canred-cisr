﻿#region USING
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ExtDirect;
using ExtDirect.Direct;
using IST.DB.SQLCreater;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using CISR.Controller.Model.Basic;
using CISR.Controller.Model.Basic.Table;
using CISR.Controller.Model.Basic.Table.Record;
using CISR;
using System.Text;
using IST.Util;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Xml;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;

#endregion
[DirectService("InitAction")]
public class InitAction : BaseAction
{
    [DirectMethod("renameDirectNamespace", DirectAction.Load, MethodVisibility.Visible)]
    public JObject renameDirectNamespace(string orgName, string newName, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        OrderLimit orderLimit = null;
        #endregion
        try
        { /*權限檢查*/
            /*
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
             */
            /*是Store操作一下就可能含有分頁資訊。*/

            /*
                *.aspx
                *.js(除了/js)
             */
            //HttpServerUtility util = new HttpServerUtility();

            string rootPath = request.HttpRequest.MapPath("~");
            var allfilepath = getAllChangeFilePath(rootPath);
            var allReplaceKeyWord = getAllFunction(orgName);
            foreach (string file in allfilepath)
            {
                System.IO.StreamReader sr = new StreamReader(file);
                var content = sr.ReadToEnd();
                sr.Close();

                var needReplace = false;
                foreach (string keyWord in allReplaceKeyWord)
                {
                    content = content.Replace(keyWord, newName + "." + keyWord.Substring(keyWord.IndexOf(".") + 1));
                    needReplace = true;
                }

                if (needReplace)
                {
                    System.IO.StreamWriter sw = new StreamWriter(file, false, Encoding.UTF8);
                    sw.WriteLine(content);
                    sw.Close();
                }
            }
            //orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/

            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();

        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }


    public List<string> getAllFunction(string prexString)
    {
        List<string> ret = new List<string>();
        AppDomain MyDomain = AppDomain.CurrentDomain;
        Assembly[] AssembliesLoaded = MyDomain.GetAssemblies();
        foreach (var allAssembly in AssembliesLoaded)
        {
            foreach (var theType in allAssembly.GetTypes())
            {
                object[] allCustomAttribute = theType.GetCustomAttributes(false);
                foreach (var _theType in allCustomAttribute)
                {
                    Type directType = _theType.GetType();
                    if (typeof(DirectServiceAttribute) == directType)
                    {
                        string className = theType.Name;
                        List<string> allFunction = DirectProxyGenerator.generateDirectApiNameList(className, prexString);
                        if (allFunction.Count() > 0)
                        {
                            foreach (var item in allFunction)
                            {
                                ret.Add(item);
                            }
                        }
                    }
                }
            }
        }
        return ret;
    }
    public List<string> getAllChangeFilePath(string rootPath)
    {
        List<string> ret = new List<string>();
        if (rootPath.EndsWith("\\js"))
        {
            return ret;
        }
        System.IO.DirectoryInfo di = new DirectoryInfo(rootPath);
        foreach (var tmpDi in di.GetDirectories())
        {
            var itemall = getAllChangeFilePath(tmpDi.FullName);
            foreach (string subItem in itemall)
            {
                ret.Add(subItem);
            }
        }

        foreach (var tmpItem in di.GetFiles("*.js"))
        {
            ret.Add(tmpItem.FullName);
        }

        foreach (var tmpItem in di.GetFiles("*.aspx"))
        {
            ret.Add(tmpItem.FullName);
        }

        return ret;
    }


    [DirectMethod("loadBasic", DirectAction.Load, MethodVisibility.Visible)]
    public JObject loadBasic(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        OrderLimit orderLimit = null;
        #endregion
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/

            //orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            var dbc = IST.Config.DataBase.Factory.getInfo();
            var dt = dbc.GetBaseConfig_DataTable();
            var row = dt.Select("KEY='DB->BASIC'");
            if (row.Count() > 0)
            {
                /*使用Store Std out 『Sotre物件標準輸出格式』*/
                return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(row[0]));
            }

            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未知的錯誤!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadException", DirectAction.Load, MethodVisibility.Visible)]
    public JObject loadException(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        OrderLimit orderLimit = null;
        #endregion
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/

            //orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            var dbc = IST.Config.DataBase.Factory.getInfo();
            var dt = dbc.GetBaseConfig_DataTable();
            var row = dt.Select("KEY='DB->MyException'");
            if (row.Count() > 0)
            {
                /*使用Store Std out 『Sotre物件標準輸出格式』*/
                return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(row[0]));
            }

            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未知的錯誤!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadAction", DirectAction.Load, MethodVisibility.Visible)]
    public JObject loadAction(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        OrderLimit orderLimit = null;
        #endregion
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/

            //orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            var dbc = IST.Config.DataBase.Factory.getInfo();
            var dt = dbc.GetBaseConfig_DataTable();
            var row = dt.Select("KEY='DB->ActionLog'");
            if (row.Count() > 0)
            {
                /*使用Store Std out 『Sotre物件標準輸出格式』*/
                return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(row[0]));
            }

            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未知的錯誤!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("loadParameter", DirectAction.Load, MethodVisibility.Visible)]
    public JObject loadParameter(Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel basicModel = new BasicModel();
        ErrorLog table = new ErrorLog();
        OrderLimit orderLimit = null;
        #endregion
        try
        { /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/
            // //System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //doc.Load(ConfigFilePath( ConfigType.ParameterConfig ));

            DataTable dt = new DataTable();
            dt.Columns.Add("AppName");
            dt.Columns.Add("AuthenticationType");
            dt.Columns.Add("EnableGuestLogin");
            dt.Columns.Add("GuestCompany");
            dt.Columns.Add("GuestAccount");
            dt.Columns.Add("IsProductionServer");
            dt.Columns.Add("DefaultPage");
            dt.Columns.Add("LogonPage");
            dt.Columns.Add("WebRoot");
            dt.Columns.Add("DEVUserCompany");
            dt.Columns.Add("DEVUserAccount");
            dt.Columns.Add("DEVUserPassword");
            dt.Columns.Add("NoPermissionPage");
            dt.Columns.Add("UploadFolder");
            dt.Columns.Add("Title");
            dt.Columns.Add("SystemIcon");
            dt.Columns.Add("CompanyImage");
            dt.Columns.Add("SystemName");
            dt.Columns.Add("SystemDescription");
            dt.Columns.Add("SystemFoolter");
            dt.Columns.Add("InitCompany");
            dt.Columns.Add("InitCompanyUuid");
            dt.Columns.Add("InitAdmin");
            dt.Columns.Add("InitAdminUuid");
            dt.Columns.Add("InitAppUuid");
            dt.Columns.Add("LogoutPage");
            dt.Columns.Add("DirectApplicationName");
            dt.Columns.Add("DirectTimeOut");


            dt.AcceptChanges();
            var row = dt.NewRow();


            row["AppName"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().AppName;
            row["AuthenticationType"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().AuthenticationType;
            row["EnableGuestLogin"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().EnableGuestLogin.ToString().ToLower();
            row["GuestCompany"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().GuestCompany;
            row["GuestAccount"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().GuestAccount;
            row["IsProductionServer"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().IsProductionServer.ToString().ToLower();
            row["DefaultPage"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().DefaultPage;
            row["LogonPage"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().LogonPage;
            row["WebRoot"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().WebRoot;
            row["DEVUserCompany"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().DEVUserCompany;
            row["DEVUserAccount"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().DEVUserAccount;
            row["DEVUserPassword"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().DEVUserPassword;
            row["NoPermissionPage"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().NoPermissionPage;
            row["UploadFolder"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().UploadFolder;
            row["Title"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().Title;
            row["SystemIcon"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().SystemIcon;
            row["CompanyImage"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().CompanyImage;
            row["SystemName"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().SystemName;
            row["SystemDescription"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().SystemDescription;
            row["SystemFoolter"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().SystemFoolter;
            row["InitCompany"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().InitCompany;
            row["InitCompanyUuid"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().InitCompanyUuid == "" ? IST.Util.UID.Instance.GetUniqueID() : CISR.Parameter.Config.ParemterConfigs.GetConfig().InitCompanyUuid;
            row["InitAdmin"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().InitAdmin;
            row["InitAdminUuid"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().InitAdminUuid == "" ? IST.Util.UID.Instance.GetUniqueID() : CISR.Parameter.Config.ParemterConfigs.GetConfig().InitAdminUuid;
            row["InitAppUuid"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid == "" ? IST.Util.UID.Instance.GetUniqueID() : CISR.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid;
            row["LogoutPage"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().LogoutPage;
            row["DirectApplicationName"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().DirectApplicationName;
            row["DirectTimeOut"] = CISR.Parameter.Config.ParemterConfigs.GetConfig().DirectTimeOut.ToString();

            //XmlDocument doc = 
            //orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Store.OutputJObject(JsonHelper.DataRowSerializerJObject(row));

        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submitParameter", DirectAction.Load, MethodVisibility.Visible)]
    public JObject submitParameter(
        string AppName, string AuthenticationType, string EnableGuestLogin,
        string GuestCompany, string GuestAccount, string IsProductionServer,
        string DefaultPage, string LogonPage, string WebRoot,
        string NoPermissionPage, string UploadFolder, string Title,
        string SystemIcon, string CompanyImage, string SystemName,
        string SystemDescription, string SystemFoolter,
        string InitCompany,
        string InitCompanyUuid,
        string InitAdmin,
        string InitAdminUuid,
        string InitAppUuid,
        string LogoutPage,
        string DEVUserCompany,
        string DEVUserAccount,
        string DEVUserPassword,
        string DirectApplicationName,
        string DirectTimeOut,
        Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(ConfigFilePath(ConfigType.ParemterFilePath));
            doc.GetElementsByTagName("AppName")[0].InnerText = AppName;
            doc.GetElementsByTagName("AuthenticationType")[0].InnerText = AuthenticationType;
            doc.GetElementsByTagName("EnableGuestLogin")[0].InnerText = EnableGuestLogin;
            doc.GetElementsByTagName("DefaultPage")[0].InnerText = DefaultPage;
            doc.GetElementsByTagName("LogonPage")[0].InnerText = LogonPage;
            doc.GetElementsByTagName("WebRoot")[0].InnerText = WebRoot;
            doc.GetElementsByTagName("NoPermissionPage")[0].InnerText = NoPermissionPage;
            doc.GetElementsByTagName("UploadFolder")[0].InnerText = UploadFolder;
            doc.GetElementsByTagName("Title")[0].InnerText = Title;
            doc.GetElementsByTagName("SystemIcon")[0].InnerText = SystemIcon;
            doc.GetElementsByTagName("CompanyImage")[0].InnerText = CompanyImage;
            doc.GetElementsByTagName("SystemName")[0].InnerText = SystemName;
            doc.GetElementsByTagName("SystemDescription")[0].InnerText = SystemDescription;
            doc.GetElementsByTagName("SystemFoolter")[0].InnerText = SystemFoolter;
            doc.GetElementsByTagName("InitCompany")[0].InnerText = InitCompany;
            doc.GetElementsByTagName("InitCompanyUuid")[0].InnerText = InitCompanyUuid;
            doc.GetElementsByTagName("InitAdmin")[0].InnerText = InitAdmin;
            doc.GetElementsByTagName("InitAdminUuid")[0].InnerText = InitAdminUuid;
            doc.GetElementsByTagName("InitAppUuid")[0].InnerText = InitAppUuid;
            doc.GetElementsByTagName("LogoutPage")[0].InnerText = LogoutPage;
            doc.GetElementsByTagName("DEVUserAccount")[0].InnerText = DEVUserAccount;
            doc.GetElementsByTagName("DEVUserPassword")[0].InnerText = DEVUserPassword;
            doc.GetElementsByTagName("DEVUserCompany")[0].InnerText = DEVUserCompany;
            doc.GetElementsByTagName("IsProductionServer")[0].InnerText = IsProductionServer;
            doc.GetElementsByTagName("DirectApplicationName")[0].InnerText = DirectApplicationName;
            doc.GetElementsByTagName("DirectTimeOut")[0].InnerText = DirectTimeOut;


            doc.Save(ConfigFilePath(ConfigType.ParemterFilePath));
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }


    [DirectMethod("testConnection", DirectAction.Load, MethodVisibility.Visible)]
    public JObject testConnection(string type, Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var dbc = IST.Config.DataBase.Factory.getInfo();
            IST.DB.DataBaseConnection dbConnection = new IST.DB.DataBaseConnection(dbc);
            IST.DB.DB db = null;

            type = type.ToUpper();
            switch (type)
            {
                case "BASIC":
                    db = new IST.DB.DB(dbConnection, "BASIC");
                    break;
                case "EXCEPTION":
                    db = new IST.DB.DB(dbConnection, "MyException");
                    break;
                case "ACTION":
                    db = new IST.DB.DB(dbConnection, "ActionLog");
                    break;
            }
            if (db != null && db.TestConnection())
            {
                return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("無法連接!"));
            }

        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submitBasic", DirectAction.Store, MethodVisibility.Visible)]
    public JObject submitBasic(string type, string connection, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        #endregion
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/

            //orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(ConfigFilePath(ConfigType.DataBaseConfig));

            var allDb = doc.GetElementsByTagName("DB");
            foreach (XmlNode item in allDb)
            {
                if (item.Attributes["Name"].Value.ToUpper() == "BASIC")
                {
                    item.Attributes["Type"].Value = type.ToUpper();
                    item.ChildNodes[0].InnerText = connection;
                }
            }
            doc.Save(ConfigFilePath(ConfigType.DataBaseConfig));

            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            //return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未知的錯誤!"));
        }
        catch (Exception ex)
        {
            log.Error(ex);
            IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submitException", DirectAction.Store, MethodVisibility.Visible)]
    public JObject submitException(string type, string connection, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        #endregion
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/

            //orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(ConfigFilePath(ConfigType.DataBaseConfig));

            var allDb = doc.GetElementsByTagName("DB");
            foreach (XmlNode item in allDb)
            {
                if (item.Attributes["Name"].Value == "MyException")
                {
                    item.Attributes["Type"].Value = type.ToUpper();
                    item.ChildNodes[0].InnerText = connection;
                }
            }
            doc.Save(ConfigFilePath(ConfigType.DataBaseConfig));

            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            //return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未知的錯誤!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submitActionLog", DirectAction.Store, MethodVisibility.Visible)]
    public JObject submitActionLog(string type, string connection, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        #endregion
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/

            //orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(ConfigFilePath(ConfigType.DataBaseConfig));

            var allDb = doc.GetElementsByTagName("DB");
            foreach (XmlNode item in allDb)
            {
                if (item.Attributes["Name"].Value == "ActionLog")
                {
                    item.Attributes["Type"].Value = type.ToUpper();
                    item.ChildNodes[0].InnerText = connection;
                }
            }
            doc.Save(ConfigFilePath(ConfigType.DataBaseConfig));

            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
            //return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未知的錯誤!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("initBasic", DirectAction.Store, MethodVisibility.Visible)]
    public JObject initBasic(Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var dbc = IST.Config.DataBase.Factory.getInfo();
            IST.DB.DataBaseConnection dbcon = new IST.DB.DataBaseConnection(dbc);
            IST.DB.DB db = new IST.DB.DB(dbcon, "BASIC");
            var dbType = dbcon.getDataBaseType("BASIC");
            var dbConnectionUser = dbcon.getConnectionUser("BASIC");

            var dbTypeStr = "";
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.MsSQL)
            {
                dbTypeStr = "MSSQL";
            }
            else if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                dbTypeStr = "ORACLE";
            }
            else if (dbType == IST.DB.ADataBaseConnection.DataBaseType.MySql)
            {
                dbTypeStr = "MYSQL";
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未實作的資料庫!"));
            }
            db.TestConnection();

            var fileUrlTable = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\TABLE\\Table.sql";
            var fileUrlView = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\VIEW\\view.sql";
            var fileUrlFP = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\FP\\fp.sql";
            FileInfo fileTable = new FileInfo(fileUrlTable);
            string scriptTable = fileTable.OpenText().ReadToEnd();
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptTable = scriptTable.Replace("{user}", dbConnectionUser).Replace("\r", "").Replace("\n", "");
            }
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                foreach (var sql in scriptTable.Split(';'))
                {
                    if (sql.Trim().Length > 0)
                    {
                        db.CommandText = sql;
                        db.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                db.CommandText = scriptTable;
                db.setTimeout(1000);
                db.ExecuteNonQuery();
            }

            FileInfo fileFP = new FileInfo(fileUrlFP);
            string scriptFp = fileFP.OpenText().ReadToEnd();
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptFp = scriptFp.Replace("{user}", dbConnectionUser);
            }

            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                foreach (string itemSql in scriptFp.Split('/'))
                {
                    if (itemSql.Trim().Length > 0)
                    {
                        db.CommandText = itemSql;
                        db.ExecuteNonQuery();
                    }
                }
            }
            else if (dbType == IST.DB.ADataBaseConnection.DataBaseType.MsSQL)
            {
                foreach (string itemSql in scriptFp.Split(';'))
                {
                    if (itemSql.Trim().Length > 0)
                    {
                        db.CommandText = itemSql;
                        db.ExecuteNonQuery();
                    }
                }
            }
            else if (dbType == IST.DB.ADataBaseConnection.DataBaseType.MySql)
            {
                foreach (string itemSql in scriptFp.Split('/'))
                {
                    if (itemSql.Trim().Length > 0)
                    {
                        db.CommandText = itemSql;
                        db.ExecuteNonQuery();
                    }
                }
            }


            FileInfo fileView = new FileInfo(fileUrlView);
            string scriptView = fileView.OpenText().ReadToEnd();
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptView = scriptView.Replace("{user}", dbConnectionUser);
            }

            foreach (string itemSql in scriptView.Split(';'))
            {
                if (itemSql.Trim().Length > 0)
                {
                    try
                    {
                        db.CommandText = itemSql;
                        db.ExecuteNonQuery();
                    }
                    catch (Exception oracleException)
                    {
                        if (oracleException.ToString().IndexOf("ORA-06575") >= 0)
                        {
                            continue;
                        }
                    }
                }
            }


            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("initException", DirectAction.Store, MethodVisibility.Visible)]
    public JObject initException(Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var dbc = IST.Config.DataBase.Factory.getInfo();
            IST.DB.DataBaseConnection dbcon = new IST.DB.DataBaseConnection(dbc);
            IST.DB.DB db = new IST.DB.DB(dbcon, "MyException");
            var dbType = dbcon.getDataBaseType("MyException");
            var dbConnectionUser = dbcon.getConnectionUser("MyException");

            var dbTypeStr = "";
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.MsSQL)
            {
                dbTypeStr = "MSSQL";
            }
            else if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                dbTypeStr = "ORACLE";
            }
            else if (dbType == IST.DB.ADataBaseConnection.DataBaseType.MySql)
            {
                dbTypeStr = "MYSQL";
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未實作的資料庫!"));
            }
            db.TestConnection();
            //var fileUrlTable = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\MSSQL\\TABLE-Exception\\Table.sql";
            //var fileUrlView = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\MSSQL\\VIEW-Exception\\view.sql";


            var fileUrlTable = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\TABLE-Exception\\Table.sql";
            var fileUrlView = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\VIEW-Exception\\view.sql";

            FileInfo fileTable = new FileInfo(fileUrlTable);
            string scriptTable = fileTable.OpenText().ReadToEnd();
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptTable = scriptTable.Replace("{user}", dbConnectionUser).Replace("\r", "").Replace("\n", "");
            }
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                foreach (var sql in scriptTable.Split(';'))
                {
                    if (sql.Trim().Length > 0)
                    {
                        db.CommandText = sql;
                        db.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                db.CommandText = scriptTable;
                db.ExecuteNonQuery();
            }


            FileInfo fileView = new FileInfo(fileUrlView);
            string scriptView = fileView.OpenText().ReadToEnd();
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptView = scriptView.Replace("{user}", dbConnectionUser);
            }
            foreach (string itemSql in scriptView.Split('/'))
            {
                var sql = itemSql.ToString().Trim();

                if (sql.Trim().Length > 0)
                {
                    while (sql.ToString().Trim().EndsWith(";"))
                    {
                        sql = sql.Substring(0, sql.ToString().Length - 1);
                    }
                    db.CommandText = sql;
                    db.ExecuteNonQuery();
                }
            }


            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("initAction", DirectAction.Store, MethodVisibility.Visible)]
    public JObject initAction(Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var dbc = IST.Config.DataBase.Factory.getInfo();
            IST.DB.DataBaseConnection dbcon = new IST.DB.DataBaseConnection(dbc);
            IST.DB.DB db = new IST.DB.DB(dbcon, "ActionLog");
            var dbType = dbcon.getDataBaseType("ActionLog");
            var dbConnectionUser = dbcon.getConnectionUser("ActionLog");
            var dbTypeStr = "";
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.MsSQL)
            {
                dbTypeStr = "MSSQL";
            }
            else if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                dbTypeStr = "ORACLE";
            }
            else if (dbType == IST.DB.ADataBaseConnection.DataBaseType.MySql)
            {
                dbTypeStr = "MYSQL";
            }
            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未實作的資料庫!"));
            }
            db.TestConnection();
            //var fileUrlTable = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\MSSQL\\TABLE-Action\\Table.sql";
            var fileUrlTable = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\TABLE-Action\\Table.sql";

            FileInfo fileTable = new FileInfo(fileUrlTable);
            string scriptTable = fileTable.OpenText().ReadToEnd();

            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                scriptTable = scriptTable.Replace("{user}", dbConnectionUser).Replace("\r", "").Replace("\n", "");
            }
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                foreach (var sql in scriptTable.Split(';'))
                {
                    if (sql.Trim().Length > 0)
                    {
                        db.CommandText = sql;
                        db.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                db.CommandText = scriptTable;
                db.ExecuteNonQuery();
            }
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }



    [DirectMethod("importData", DirectAction.Store, MethodVisibility.Visible)]
    public JObject importData(string isDownload, Request request)
    {
        try
        {
            /*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            isDownload = isDownload.Trim().ToLower();
            if (isDownload.Length == 0)
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("參數錯誤!"));
            }

            if (!(isDownload == "true" || isDownload == "false"))
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("參數錯誤!"));
            }

            var dbc = IST.Config.DataBase.Factory.getInfo();
            IST.DB.DataBaseConnection dbcon = new IST.DB.DataBaseConnection(dbc);
            IST.DB.DB db = new IST.DB.DB(dbcon, "BASIC");
            var dbType = dbcon.getDataBaseType("BASIC");
            var dbConnectionUser = dbcon.getConnectionUser("BASIC");
            var dbTypeStr = "";
            if (dbType == IST.DB.ADataBaseConnection.DataBaseType.MsSQL)
            {
                dbTypeStr = "MSSQL";
            }
            else if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle)
            {
                dbTypeStr = "ORACLE";
            }
            else if (dbType == IST.DB.ADataBaseConnection.DataBaseType.MySql)
            {
                dbTypeStr = "MYSQL";
            }

            else
            {
                return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("未實作的資料庫!"));
            }
            db.TestConnection();
            var importDataUrl = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\DATA\\importData.sql";
            var saveUrl = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\DATA\\save.sql";
            var keyUrl = AppDomain.CurrentDomain.BaseDirectory + "initDataBase\\SQL\\" + dbTypeStr + "\\DATA\\key.txt";

            FileInfo importData = new FileInfo(importDataUrl);
            string scriptImport = importData.OpenText().ReadToEnd();
            System.Collections.Hashtable ht = new System.Collections.Hashtable();


            using (System.IO.TextReader tr = File.OpenText(keyUrl))
            {
                var tmpKey = "";
                while ((tmpKey = tr.ReadLine()) != null)
                {
                    var uuid = IST.Util.UID.Instance.GetUniqueID();
                    switch (tmpKey)
                    {
                        case "{admin}":

                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, CISR.Parameter.Config.ParemterConfigs.GetConfig().InitAdmin);
                            }
                            break;
                        case "{admin_uuid}":
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, CISR.Parameter.Config.ParemterConfigs.GetConfig().InitAdminUuid);
                            }

                            break;
                        case "{application_name}":
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, CISR.Parameter.Config.ParemterConfigs.GetConfig().AppName);
                            }
                            break;
                        case "{application_head_uuid}":
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, CISR.Parameter.Config.ParemterConfigs.GetConfig().InitAppUuid);
                            }
                            break;
                        case "{company}":
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, CISR.Parameter.Config.ParemterConfigs.GetConfig().InitCompany);
                            }
                            break;
                        case "{company_uuid}":
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, CISR.Parameter.Config.ParemterConfigs.GetConfig().InitCompanyUuid);
                            }
                            break;
                        default:
                            if (ht.ContainsKey(tmpKey) == false)
                            {
                                ht.Add(tmpKey, IST.Util.UID.Instance.GetUniqueID());
                            }
                            break;
                    }
                }
            }


            foreach (System.Collections.DictionaryEntry item in ht)
            {
                scriptImport = scriptImport.Replace(item.Key.ToString(), item.Value.ToString());
            }

            System.IO.StreamWriter sw = new StreamWriter(saveUrl);
            sw.Write(scriptImport);
            sw.Close();
            var sql = System.IO.File.OpenText(saveUrl).ReadToEnd();

            if (isDownload == "false")
            {
                db = new IST.DB.DB(dbcon, "BASIC");
                if (dbType == IST.DB.ADataBaseConnection.DataBaseType.Oracle || dbType == IST.DB.ADataBaseConnection.DataBaseType.MySql)
                {
                    foreach (var _sql in sql.Split(';'))
                    {
                        if (_sql.Trim().Length > 0)
                        {
                            db.CommandText = _sql;
                            db.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    db.CommandText = sql;
                    db.ExecuteNonQuery();
                }
            }

            return ExtDirect.Direct.Helper.Message.Success.OutputJObject();



        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
    /*load Config*/
    public enum ConfigType
    {
        DataBaseConfig, ParemterFilePath, CloudFilePath
    }

    public static string ConfigFilePath(ConfigType configType)
    {
        string filename = null;
        try
        {
            HttpContext context = HttpContext.Current;
            var nv = new NameValueCollection();
            nv = (NameValueCollection)ConfigurationManager.GetSection("APConfigFiles");
            for (int i = 0; i < nv.AllKeys.Length; i++)
            {
                if (nv.AllKeys[i].ToUpper() == configType.ToString().ToUpper())
                {
                    filename = nv[i];
                    break;
                }
            }
            if (context != null)
            {
                if (filename.IndexOf("~") > -1)
                {
                    filename = context.Server.MapPath(filename);
                }
                else
                {
                    filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
                }
            }
            else
            {
                filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename.Replace("~/", ""));
            }


            if (!File.Exists(filename))
            {
                throw new Exception("發生錯誤: 沒有正確的" + configType.ToString().ToUpper() + "文件");
            }
            return filename;
        }
        catch (Exception ex)
        {
            log.Error(ex);
            throw ex;
        }
    }


}



