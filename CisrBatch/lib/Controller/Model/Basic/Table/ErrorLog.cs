using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IST.Attribute;
using IST.DB;
using IST.Config.DataBase;
using IST.DB.SQLCreater;
using CISR.Controller.Model.Basic.Table.Record;
namespace CISR.Controller.Model.Basic.Table
{
    [ISTDataBase("BASIC")]
    [ISTTableView("ERROR_LOG", true)]
    public partial class ErrorLog : TableBase
    {
        /*固定物件*/
        //IST.DB.SQLCreater.ASQLCreater sqlCreater = null;
        /*固定物件但名稱需更新*/
        private ErrorLog_Record _currentRecord = null;
        private IList<ErrorLog_Record> _All_Record = new List<ErrorLog_Record>();
        /*建構子*/
        public ErrorLog() { }
        public ErrorLog(IDataBaseConfigInfo dbc, string db) : base(dbc, db) { }
        public ErrorLog(IDataBaseConfigInfo dbc) : base(dbc) { }
        public ErrorLog(IDataBaseConfigInfo dbc, ErrorLog_Record currenData)
        {
            this.setDataBaseConfigInfo(dbc);
            this._currentRecord = currenData;
        }
        public ErrorLog(IList<ErrorLog_Record> currenData)
        {
            this._All_Record = currenData;
        }
        /*欄位資訊 Start*/
        public string UUID { get { return "UUID"; } }
        public string CREATE_DATE { get { return "CREATE_DATE"; } }
        public string UPDATE_DATE { get { return "UPDATE_DATE"; } }
        public string IS_ACTIVE { get { return "IS_ACTIVE"; } }
        public string ERROR_CODE { get { return "ERROR_CODE"; } }
        public string ERROR_TIME { get { return "ERROR_TIME"; } }
        public string ERROR_MESSAGE { get { return "ERROR_MESSAGE"; } }
        public string APPLICATION_NAME { get { return "APPLICATION_NAME"; } }
        public string ATTENDANT_UUID { get { return "ATTENDANT_UUID"; } }
        public string ERROR_TYPE { get { return "ERROR_TYPE"; } }
        public string IS_READ { get { return "IS_READ"; } }
        /*欄位資訊 End*/
        /*固定的方法，但名稱需變更 Start*/
        public ErrorLog_Record CurrentRecord()
        {
            try
            {
                if (_currentRecord == null)
                {
                    if (this._All_Record.Count > 0)
                    {
                        _currentRecord = this._All_Record.First();
                    }
                }
                return _currentRecord;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public ErrorLog_Record CreateNew()
        {
            try
            {
                ErrorLog_Record newData = new ErrorLog_Record();
                return newData;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public IList<ErrorLog_Record> AllRecord()
        {
            try
            {
                return _All_Record;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public void RemoveAllRecord()
        {
            try
            {
                _All_Record = new List<ErrorLog_Record>();
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*固定的方法，但名稱需變更 End*/
        /*有關PK的方法*/
        //TEMPLATE TABLE 201303180156
        public ErrorLog Fill_By_PK(string puuid)
        {
            try
            {
                IList<ErrorLog_Record> ret = null;
                ret = this.Where(
                new SQLCondition(this)
                                    .Equal(this.UUID, puuid)
                ).FetchAll<ErrorLog_Record>();
                _All_Record = ret;
                if (_All_Record.Count > 0)
                {
                    _currentRecord = ret.First();
                }
                else
                {
                    _currentRecord = null;
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        //TEMPLATE TABLE 201303180156
        public ErrorLog Fill_By_PK(string puuid, DB db)
        {
            try
            {
                IList<ErrorLog_Record> ret = null;
                ret = this.Where(
                new SQLCondition(this)
                                    .Equal(this.UUID, puuid)
                ).FetchAll<ErrorLog_Record>(db);
                _All_Record = ret;
                if (_All_Record.Count > 0)
                {
                    _currentRecord = ret.First();
                }
                else
                {
                    _currentRecord = null;
                }
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        //TEMPLATE TABLE 20130319042
        public ErrorLog_Record Fetch_By_PK(string puuid)
        {
            try
            {
                IList<ErrorLog_Record> ret = null;
                ret = this.Where(
                new SQLCondition(this)
                                    .Equal(this.UUID, puuid)
                ).FetchAll<ErrorLog_Record>();
                return ret.First();
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        //TEMPLATE TABLE 20130319044
        public ErrorLog_Record Fetch_By_PK(string puuid, DB db)
        {
            try
            {
                IList<ErrorLog_Record> ret = null;
                ret = this.Where(
                new SQLCondition(this)
                                    .Equal(this.UUID, puuid)
                ).FetchAll<ErrorLog_Record>(db);
                return ret.First();
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        //TEMPLATE TABLE 20130319045
        public ErrorLog Fill_By_Uuid(string puuid)
        {
            try
            {
                IList<ErrorLog_Record> ret = null;
                ret = this.Where(
                new SQLCondition(this)
                                    .Equal(this.UUID, puuid)
                ).FetchAll<ErrorLog_Record>();
                _All_Record = ret;
                _currentRecord = ret.First();
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        //TEMPLATE TABLE 20130319046
        public ErrorLog Fill_By_Uuid(string puuid, DB db)
        {
            try
            {
                IList<ErrorLog_Record> ret = null;
                ret = this.Where(
                new SQLCondition(this)
                                    .Equal(this.UUID, puuid)
                ).FetchAll<ErrorLog_Record>(db);
                _All_Record = ret;
                _currentRecord = ret.First();
                return this;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        //TEMPLATE TABLE 20130319047
        public ErrorLog_Record Fetch_By_Uuid(string puuid)
        {
            try
            {
                IList<ErrorLog_Record> ret = null;
                ret = this.Where(
                new SQLCondition(this)
                                    .Equal(this.UUID, puuid)
                ).FetchAll<ErrorLog_Record>();
                return ret.First();
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.ErrorNoThrowException(this, ex);
                return null;
            }
        }
        //TEMPLATE TABLE 20130319048
        public ErrorLog_Record Fetch_By_Uuid(string puuid, DB db)
        {
            try
            {
                IList<ErrorLog_Record> ret = null;
                ret = this.Where(
                new SQLCondition(this)
                                    .Equal(this.UUID, puuid)
                ).FetchAll<ErrorLog_Record>(db);
                return ret.First();
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*利用物件自已的AllRecord的資料來更新資料行*/
        public void UpdateAllRecord()
        {
            try
            {
                UpdateAllRecord<ErrorLog_Record>(this.AllRecord());
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*利用物件自已的AllRecord的資料來更新資料行*/
        public void UpdateAllRecord(DB db)
        {
            try
            {
                UpdateAllRecord<ErrorLog_Record>(this.AllRecord(), db);
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*利用物件自已的AllRecord的資料來新增資料行*/
        public void InsertAllRecord()
        {
            try
            {
                InsertAllRecord<ErrorLog_Record>(this.AllRecord());
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*利用物件自已的AllRecord的資料來新增資料行*/
        public void InsertAllRecord(DB db)
        {
            try
            {
                InsertAllRecord<ErrorLog_Record>(this.AllRecord(), db);
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*利用物件自已的AllRecord的資料來刪除資料行*/
        public void DeleteAllRecord()
        {
            try
            {
                DeleteAllRecord<ErrorLog_Record>(this.AllRecord());
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*利用物件自已的AllRecord的資料來刪除資料行*/
        public void DeleteAllRecord(DB db)
        {
            try
            {
                DeleteAllRecord<ErrorLog_Record>(this.AllRecord(), db);
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*依照資料表與資料表的關係，產生出來的方法*/
        public List<Attendant_Record> Link_Attendant_By_Uuid()
        {
            try
            {
                List<Attendant_Record> ret = new List<Attendant_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                Attendant ___table = new Attendant(dbc);
                SQLCondition condition = new SQLCondition(___table);
                foreach (var item in AllRecord())
                {
                    condition
                    .L().Equal(___table.UUID, item.ATTENDANT_UUID).R().Or();
                }
                condition.CheckSQL();
                ret = (List<Attendant_Record>)
                        ___table.Where(condition)
                        .FetchAll<Attendant_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180340*/
        public List<Attendant_Record> Link_Attendant_By_Uuid(OrderLimit limit)
        {
            try
            {
                List<Attendant_Record> ret = new List<Attendant_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                Attendant ___table = new Attendant(dbc);
                SQLCondition condition = new SQLCondition(___table);
                foreach (var item in AllRecord())
                {
                    condition
                    .L().Equal(___table.UUID, item.ATTENDANT_UUID).R().Or();
                }
                condition.CheckSQL();
                ret = (List<Attendant_Record>)
                        ___table.Where(condition)
                        .Order(limit)
                        .Limit(limit)
                        .FetchAll<Attendant_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180336*/
        public Attendant LinkFill_Attendant_By_Uuid()
        {
            try
            {
                var data = Link_Attendant_By_Uuid();
                Attendant ret = new Attendant(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180337*/
        public Attendant LinkFill_Attendant_By_Uuid(OrderLimit limit)
        {
            try
            {
                var data = Link_Attendant_By_Uuid(limit);
                Attendant ret = new Attendant(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
    }
}
