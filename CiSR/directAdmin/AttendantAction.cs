﻿#region USING
using System;
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
using System.Diagnostics;

#endregion
[DirectService("AttendantAction")]
public class AttendantAction : BaseAction
{
    [DirectMethod("getUserName", DirectAction.Load, MethodVisibility.Visible)]
    public JObject getUserName(string pUuid, Request request)
    {
        #region Declare
        BasicModel model = new BasicModel();
        #endregion
        try
        {  /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }/*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var data = model.getAttendant_By_Uuid(pUuid);
            if (data.AllRecord().Count == 1)
            {
                return ExtDirect.Direct.Helper.JObjectHelper.StringOnly(data.AllRecord().First().C_NAME);
            }

            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Data Not Found!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("load", DirectAction.Store, MethodVisibility.Visible)]
    public JObject load(string company_uuid, string keyword, string pageNo, string limitNo, string sort, string dir, Request request)
    {
        #region Declare
        List<JObject> jobject = new List<JObject>();
        BasicModel model = new BasicModel();
        AttendantV_Record table = new AttendantV_Record();
        OrderLimit orderLimit = null;
        #endregion
        try
        {  /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }/*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*是Store操作一下就可能含有分頁資訊。*/
            orderLimit = ExtDirect.Direct.Helper.Order.getOrderLimit(pageNo, limitNo, sort, dir);
            /*取得總資料數*/
            var totalCount = model.getAttendantV_By_CompanyUuid_KeyWord_Count(company_uuid, keyword);
            /*取得資料*/
            var data = model.getAttendantV_By_CompanyUuid_KeyWord(company_uuid, keyword, orderLimit);
            if (data.Count > 0)
            {
                /*將List<RecordBase>變成JSON字符串*/
                jobject = JsonHelper.RecordBaseListJObject(data);
            }
            /*使用Store Std out 『Sotre物件標準輸出格式』*/
            return ExtDirect.Direct.Helper.Store.OutputJObject(jobject, totalCount);
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            /*將Exception轉成EXT Exception JSON格式*/
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("info", DirectAction.Load, MethodVisibility.Visible)]
    public JObject info(string pUuid, Request request)
    {
        #region Declare
        BasicModel model = new BasicModel();
        #endregion

        try
        {  /*Cloud身份檢查*/
            checkUser(request.HttpRequest);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }/*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            var data = model.getAttendant_By_Uuid(pUuid);

            if (data.AllRecord().Count == 1)
            {
                return ExtDirect.Direct.Helper.Form.OutputJObject(JsonHelper.RecordBaseJObject(data.AllRecord().First()));
            }
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(new Exception("Data Not Found!"));
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }

    [DirectMethod("submit", DirectAction.FormSubmission, MethodVisibility.Visible)]
    public JObject submit(string uuid,
string create_date,
string update_date,
string is_active,
string company_uuid,
string account,
string c_name,
string e_name,
string email,
string password,
string is_supper,
string is_admin,
string code_page,
string department_uuid,
string phone,
string site_uuid,
string gender,
string birthday,
string hire_date,
string quit_date,
string is_manager,
string is_direct,
string grade,
string id,
string src_uuid,
string is_default_pass, HttpRequest request)
    {


        #region Declare
        var action = SubmitAction.None;
        BasicModel model = new BasicModel();
        Attendant_Record record = new Attendant_Record();
        #endregion
        try
        {  /*Cloud身份檢查*/
            checkUser(request);
            if (this.getUser() == null)
            {
                throw new Exception("Identity authentication failed.");
            }/*權限檢查*/
            if (!checkProxy(new StackTrace().GetFrame(0)))
            {
                throw new Exception("Permission Denied!");
            };
            /*
             * 所有Form的動作最終是使用Submit的方式將資料傳出；
             * 必須有一個特徵來判斷使用者，執行的動作；
             */
            if (uuid.Trim().Length > 0)
            {
                action = SubmitAction.Edit;
                record = model.getAttendant_By_Uuid(uuid).AllRecord().First();
            }
            else
            {
                action = SubmitAction.Create;
                record.UUID = IST.Util.UID.Instance.GetUniqueID();
                record.CREATE_DATE = DateTime.Now;
            }
            record.ACCOUNT = account;
            record.BIRTHDAY = null;
            record.C_NAME = c_name;
            record.CODE_PAGE = code_page;

            record.COMPANY_UUID = getUser().COMPANY_UUID;
            record.DEPARTMENT_UUID = department_uuid;
            record.E_NAME = e_name;
            record.EMAIL = email;
            record.GENDER = gender;
            record.GRADE = grade;
            record.ID = id;
            record.IS_ACTIVE = is_active;
            record.IS_ADMIN = is_admin;
            record.IS_DIRECT = is_direct;
            record.IS_MANAGER = is_manager;
            record.IS_SUPPER = is_supper;
            record.PASSWORD = password;
            record.PHONE = phone;
            record.CODE_PAGE = "TW";
            record.SITE_UUID = null;

            record.IS_ACTIVE = is_active;
            record.UPDATE_DATE = DateTime.Now;

            if (action == SubmitAction.Edit)
            {
                record.gotoTable().Update_Empty2Null(record);
            }
            else if (action == SubmitAction.Create)
            {
                record.gotoTable().Insert(record);
                uuid = record.UUID;
            }

            System.Collections.Hashtable otherParam = new System.Collections.Hashtable();
            otherParam.Add("UUID", record.UUID);
            return ExtDirect.Direct.Helper.Message.Success.OutputJObject(otherParam);
        }
        catch (Exception ex)
        {
            log.Error(ex); IST.MyException.MyException.Error(this, ex);
            return ExtDirect.Direct.Helper.Message.Fail.OutputJObject(ex);
        }
    }
}

