using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IST.Attribute;
using IST.DB;
using IST.DB.SQLCreater;
using CISR.Controller.Model.Basic.Table;
namespace CISR.Controller.Model.Basic.Table.Record
{
    [ISTRecord]
    [ISTTableView("APPMENU", true)]
    [ISTDataBase("BASIC")]
    [Serializable]
    public class Appmenu_Record : RecordBase
    {
        public Appmenu_Record() { }
        /*欄位資訊 Start*/
        string _UUID = null;
        string _IS_ACTIVE = null;
        DateTime? _CREATE_DATE = null;
        string _CREATE_USER = null;
        DateTime? _UPDATE_DATE = null;
        string _UPDATE_USER = null;
        string _NAME_ZH_TW = null;
        string _NAME_ZH_CN = null;
        string _NAME_EN_US = null;
        string _ID = null;
        string _APPMENU_UUID = null;
        string _HASCHILD = null;
        string _APPLICATION_HEAD_UUID = null;
        decimal? _ORD = null;
        string _PARAMETER_CLASS = null;
        string _IMAGE = null;
        string _SITEMAP_UUID = null;
        string _ACTION_MODE = null;
        string _IS_DEFAULT_PAGE = null;
        string _IS_ADMIN = null;
        /*欄位資訊 End*/

        [ColumnName("UUID", true, typeof(string))]
        public string UUID
        {
            set
            {
                _UUID = value;
            }
            get
            {
                return _UUID;
            }
        }

        [ColumnName("IS_ACTIVE", false, typeof(string))]
        public string IS_ACTIVE
        {
            set
            {
                _IS_ACTIVE = value;
            }
            get
            {
                return _IS_ACTIVE;
            }
        }

        [ColumnName("CREATE_DATE", false, typeof(DateTime?))]
        public DateTime? CREATE_DATE
        {
            set
            {
                _CREATE_DATE = value;
            }
            get
            {
                return _CREATE_DATE;
            }
        }

        [ColumnName("CREATE_USER", false, typeof(string))]
        public string CREATE_USER
        {
            set
            {
                _CREATE_USER = value;
            }
            get
            {
                return _CREATE_USER;
            }
        }

        [ColumnName("UPDATE_DATE", false, typeof(DateTime?))]
        public DateTime? UPDATE_DATE
        {
            set
            {
                _UPDATE_DATE = value;
            }
            get
            {
                return _UPDATE_DATE;
            }
        }

        [ColumnName("UPDATE_USER", false, typeof(string))]
        public string UPDATE_USER
        {
            set
            {
                _UPDATE_USER = value;
            }
            get
            {
                return _UPDATE_USER;
            }
        }

        [ColumnName("NAME_ZH_TW", false, typeof(string))]
        public string NAME_ZH_TW
        {
            set
            {
                _NAME_ZH_TW = value;
            }
            get
            {
                return _NAME_ZH_TW;
            }
        }

        [ColumnName("NAME_ZH_CN", false, typeof(string))]
        public string NAME_ZH_CN
        {
            set
            {
                _NAME_ZH_CN = value;
            }
            get
            {
                return _NAME_ZH_CN;
            }
        }

        [ColumnName("NAME_EN_US", false, typeof(string))]
        public string NAME_EN_US
        {
            set
            {
                _NAME_EN_US = value;
            }
            get
            {
                return _NAME_EN_US;
            }
        }

        [ColumnName("ID", false, typeof(string))]
        public string ID
        {
            set
            {
                _ID = value;
            }
            get
            {
                return _ID;
            }
        }

        [ColumnName("APPMENU_UUID", false, typeof(string))]
        public string APPMENU_UUID
        {
            set
            {
                _APPMENU_UUID = value;
            }
            get
            {
                return _APPMENU_UUID;
            }
        }

        [ColumnName("HASCHILD", false, typeof(string))]
        public string HASCHILD
        {
            set
            {
                _HASCHILD = value;
            }
            get
            {
                return _HASCHILD;
            }
        }

        [ColumnName("APPLICATION_HEAD_UUID", false, typeof(string))]
        public string APPLICATION_HEAD_UUID
        {
            set
            {
                _APPLICATION_HEAD_UUID = value;
            }
            get
            {
                return _APPLICATION_HEAD_UUID;
            }
        }

        [ColumnName("ORD", false, typeof(decimal?))]
        public decimal? ORD
        {
            set
            {
                _ORD = value;
            }
            get
            {
                return _ORD;
            }
        }

        [ColumnName("PARAMETER_CLASS", false, typeof(string))]
        public string PARAMETER_CLASS
        {
            set
            {
                _PARAMETER_CLASS = value;
            }
            get
            {
                return _PARAMETER_CLASS;
            }
        }

        [ColumnName("IMAGE", false, typeof(string))]
        public string IMAGE
        {
            set
            {
                _IMAGE = value;
            }
            get
            {
                return _IMAGE;
            }
        }

        [ColumnName("SITEMAP_UUID", false, typeof(string))]
        public string SITEMAP_UUID
        {
            set
            {
                _SITEMAP_UUID = value;
            }
            get
            {
                return _SITEMAP_UUID;
            }
        }

        [ColumnName("ACTION_MODE", false, typeof(string))]
        public string ACTION_MODE
        {
            set
            {
                _ACTION_MODE = value;
            }
            get
            {
                return _ACTION_MODE;
            }
        }

        [ColumnName("IS_DEFAULT_PAGE", false, typeof(string))]
        public string IS_DEFAULT_PAGE
        {
            set
            {
                _IS_DEFAULT_PAGE = value;
            }
            get
            {
                return _IS_DEFAULT_PAGE;
            }
        }

        [ColumnName("IS_ADMIN", false, typeof(string))]
        public string IS_ADMIN
        {
            set
            {
                _IS_ADMIN = value;
            }
            get
            {
                return _IS_ADMIN;
            }
        }
        public Appmenu_Record Clone()
        {
            try
            {
                return this.Clone<Appmenu_Record>(this);
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public Appmenu gotoTable()
        {
            try
            {
                var dbc = IST.Config.DataBase.Factory.getInfo();
                Appmenu ret = new Appmenu(dbc, this);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180347*/
        public List<Appmenu_Record> Link_Appmenu_By_AppmenuUuid()
        {
            try
            {
                List<Appmenu_Record> ret = new List<Appmenu_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                Appmenu ___table = new Appmenu(dbc);
                ret = (List<Appmenu_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.APPMENU_UUID, this.UUID))
                    .FetchAll<Appmenu_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180347*/
        public List<GroupAppmenu_Record> Link_GroupAppmenu_By_AppmenuUuid()
        {
            try
            {
                List<GroupAppmenu_Record> ret = new List<GroupAppmenu_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                GroupAppmenu ___table = new GroupAppmenu(dbc);
                ret = (List<GroupAppmenu_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.APPMENU_UUID, this.UUID))
                    .FetchAll<GroupAppmenu_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180347*/
        public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_AppmenuUuid()
        {
            try
            {
                List<AppmenuApppageV_Record> ret = new List<AppmenuApppageV_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                AppmenuApppageV ___table = new AppmenuApppageV(dbc);
                ret = (List<AppmenuApppageV_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.APPMENU_UUID, this.UUID))
                    .FetchAll<AppmenuApppageV_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180347*/
        public List<GroupAppmenuV_Record> Link_GroupAppmenuV_By_AppmenuUuid()
        {
            try
            {
                List<GroupAppmenuV_Record> ret = new List<GroupAppmenuV_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                GroupAppmenuV ___table = new GroupAppmenuV(dbc);
                ret = (List<GroupAppmenuV_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.APPMENU_UUID, this.UUID))
                    .FetchAll<GroupAppmenuV_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180347*/
        public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_AppmenuUuid()
        {
            try
            {
                List<VAppmenuProxyMap_Record> ret = new List<VAppmenuProxyMap_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
                ret = (List<VAppmenuProxyMap_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.APPMENU_UUID, this.UUID))
                    .FetchAll<VAppmenuProxyMap_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180348*/
        public List<Appmenu_Record> Link_Appmenu_By_AppmenuUuid(OrderLimit limit)
        {
            try
            {
                List<Appmenu_Record> ret = new List<Appmenu_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                Appmenu ___table = new Appmenu(dbc);
                ret = (List<Appmenu_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.APPMENU_UUID, this.UUID))
                    .Order(limit)
                    .Limit(limit)
                    .FetchAll<Appmenu_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180348*/
        public List<GroupAppmenu_Record> Link_GroupAppmenu_By_AppmenuUuid(OrderLimit limit)
        {
            try
            {
                List<GroupAppmenu_Record> ret = new List<GroupAppmenu_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                GroupAppmenu ___table = new GroupAppmenu(dbc);
                ret = (List<GroupAppmenu_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.APPMENU_UUID, this.UUID))
                    .Order(limit)
                    .Limit(limit)
                    .FetchAll<GroupAppmenu_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180348*/
        public List<AppmenuApppageV_Record> Link_AppmenuApppageV_By_AppmenuUuid(OrderLimit limit)
        {
            try
            {
                List<AppmenuApppageV_Record> ret = new List<AppmenuApppageV_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                AppmenuApppageV ___table = new AppmenuApppageV(dbc);
                ret = (List<AppmenuApppageV_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.APPMENU_UUID, this.UUID))
                    .Order(limit)
                    .Limit(limit)
                    .FetchAll<AppmenuApppageV_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180348*/
        public List<GroupAppmenuV_Record> Link_GroupAppmenuV_By_AppmenuUuid(OrderLimit limit)
        {
            try
            {
                List<GroupAppmenuV_Record> ret = new List<GroupAppmenuV_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                GroupAppmenuV ___table = new GroupAppmenuV(dbc);
                ret = (List<GroupAppmenuV_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.APPMENU_UUID, this.UUID))
                    .Order(limit)
                    .Limit(limit)
                    .FetchAll<GroupAppmenuV_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180348*/
        public List<VAppmenuProxyMap_Record> Link_VAppmenuProxyMap_By_AppmenuUuid(OrderLimit limit)
        {
            try
            {
                List<VAppmenuProxyMap_Record> ret = new List<VAppmenuProxyMap_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                VAppmenuProxyMap ___table = new VAppmenuProxyMap(dbc);
                ret = (List<VAppmenuProxyMap_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.APPMENU_UUID, this.UUID))
                    .Order(limit)
                    .Limit(limit)
                    .FetchAll<VAppmenuProxyMap_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public List<ApplicationHead_Record> Link_ApplicationHead_By_Uuid()
        {
            try
            {
                List<ApplicationHead_Record> ret = new List<ApplicationHead_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                ApplicationHead ___table = new ApplicationHead(dbc);
                ret = (List<ApplicationHead_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.UUID, this.APPLICATION_HEAD_UUID))
                    .FetchAll<ApplicationHead_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public List<Sitemap_Record> Link_Sitemap_By_Uuid()
        {
            try
            {
                List<Sitemap_Record> ret = new List<Sitemap_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                Sitemap ___table = new Sitemap(dbc);
                ret = (List<Sitemap_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.UUID, this.SITEMAP_UUID))
                    .FetchAll<Sitemap_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        public List<Appmenu_Record> Link_Appmenu_By_Uuid()
        {
            try
            {
                List<Appmenu_Record> ret = new List<Appmenu_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                Appmenu ___table = new Appmenu(dbc);
                ret = (List<Appmenu_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.UUID, this.APPMENU_UUID))
                    .FetchAll<Appmenu_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180404*/
        public List<ApplicationHead_Record> Link_ApplicationHead_By_Uuid(OrderLimit limit)
        {
            try
            {
                List<ApplicationHead_Record> ret = new List<ApplicationHead_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                ApplicationHead ___table = new ApplicationHead(dbc);
                ret = (List<ApplicationHead_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.UUID, this.APPLICATION_HEAD_UUID))
                    .Order(limit)
                    .Limit(limit)
                    .FetchAll<ApplicationHead_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180404*/
        public List<Sitemap_Record> Link_Sitemap_By_Uuid(OrderLimit limit)
        {
            try
            {
                List<Sitemap_Record> ret = new List<Sitemap_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                Sitemap ___table = new Sitemap(dbc);
                ret = (List<Sitemap_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.UUID, this.SITEMAP_UUID))
                    .Order(limit)
                    .Limit(limit)
                    .FetchAll<Sitemap_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180404*/
        public List<Appmenu_Record> Link_Appmenu_By_Uuid(OrderLimit limit)
        {
            try
            {
                List<Appmenu_Record> ret = new List<Appmenu_Record>();
                var dbc = IST.Config.DataBase.Factory.getInfo();
                Appmenu ___table = new Appmenu(dbc);
                ret = (List<Appmenu_Record>)
                                        ___table.Where(new SQLCondition(___table)
                                        .Equal(___table.UUID, this.APPMENU_UUID))
                    .Order(limit)
                    .Limit(limit)
                    .FetchAll<Appmenu_Record>();
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180357*/
        public Appmenu LinkFill_Appmenu_By_AppmenuUuid()
        {
            try
            {
                var data = Link_Appmenu_By_AppmenuUuid();
                Appmenu ret = new Appmenu(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180357*/
        public GroupAppmenu LinkFill_GroupAppmenu_By_AppmenuUuid()
        {
            try
            {
                var data = Link_GroupAppmenu_By_AppmenuUuid();
                GroupAppmenu ret = new GroupAppmenu(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180357*/
        public AppmenuApppageV LinkFill_AppmenuApppageV_By_AppmenuUuid()
        {
            try
            {
                var data = Link_AppmenuApppageV_By_AppmenuUuid();
                AppmenuApppageV ret = new AppmenuApppageV(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180357*/
        public GroupAppmenuV LinkFill_GroupAppmenuV_By_AppmenuUuid()
        {
            try
            {
                var data = Link_GroupAppmenuV_By_AppmenuUuid();
                GroupAppmenuV ret = new GroupAppmenuV(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180357*/
        public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_AppmenuUuid()
        {
            try
            {
                var data = Link_VAppmenuProxyMap_By_AppmenuUuid();
                VAppmenuProxyMap ret = new VAppmenuProxyMap(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180358*/
        public Appmenu LinkFill_Appmenu_By_AppmenuUuid(OrderLimit limit)
        {
            try
            {
                var data = Link_Appmenu_By_AppmenuUuid(limit);
                Appmenu ret = new Appmenu(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180358*/
        public GroupAppmenu LinkFill_GroupAppmenu_By_AppmenuUuid(OrderLimit limit)
        {
            try
            {
                var data = Link_GroupAppmenu_By_AppmenuUuid(limit);
                GroupAppmenu ret = new GroupAppmenu(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180358*/
        public AppmenuApppageV LinkFill_AppmenuApppageV_By_AppmenuUuid(OrderLimit limit)
        {
            try
            {
                var data = Link_AppmenuApppageV_By_AppmenuUuid(limit);
                AppmenuApppageV ret = new AppmenuApppageV(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180358*/
        public GroupAppmenuV LinkFill_GroupAppmenuV_By_AppmenuUuid(OrderLimit limit)
        {
            try
            {
                var data = Link_GroupAppmenuV_By_AppmenuUuid(limit);
                GroupAppmenuV ret = new GroupAppmenuV(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180358*/
        public VAppmenuProxyMap LinkFill_VAppmenuProxyMap_By_AppmenuUuid(OrderLimit limit)
        {
            try
            {
                var data = Link_VAppmenuProxyMap_By_AppmenuUuid(limit);
                VAppmenuProxyMap ret = new VAppmenuProxyMap(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*2013031800428*/
        public ApplicationHead LinkFill_ApplicationHead_By_Uuid()
        {
            try
            {
                var data = Link_ApplicationHead_By_Uuid();
                ApplicationHead ret = new ApplicationHead(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*2013031800428*/
        public Sitemap LinkFill_Sitemap_By_Uuid()
        {
            try
            {
                var data = Link_Sitemap_By_Uuid();
                Sitemap ret = new Sitemap(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*2013031800428*/
        public Appmenu LinkFill_Appmenu_By_Uuid()
        {
            try
            {
                var data = Link_Appmenu_By_Uuid();
                Appmenu ret = new Appmenu(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180429*/
        public ApplicationHead LinkFill_ApplicationHead_By_Uuid(OrderLimit limit)
        {
            try
            {
                var data = Link_ApplicationHead_By_Uuid(limit);
                ApplicationHead ret = new ApplicationHead(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180429*/
        public Sitemap LinkFill_Sitemap_By_Uuid(OrderLimit limit)
        {
            try
            {
                var data = Link_Sitemap_By_Uuid(limit);
                Sitemap ret = new Sitemap(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Error(ex); IST.MyException.MyException.Error(this, ex);
                throw ex;
            }
        }
        /*201303180429*/
        public Appmenu LinkFill_Appmenu_By_Uuid(OrderLimit limit)
        {
            try
            {
                var data = Link_Appmenu_By_Uuid(limit);
                Appmenu ret = new Appmenu(data);
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
