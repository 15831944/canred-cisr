﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
namespace IST.Config.DataBase
{
    public class Factory
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IDataBaseConfigInfo _idatabaseconfig = null;
        public static IDataBaseConfigInfo getInfo()
        {
            try
            {
                if (_idatabaseconfig != null)
                {
                    return _idatabaseconfig;
                }
                else
                {
                    IST.Config.DataBase.DataBaseConfigInfo ret = new DataBaseConfigInfo();
                    _idatabaseconfig = ret;
                    return _idatabaseconfig;
                }
                // return result.GetType().GetProperty("UUID").GetValue(result, null).ToString();                
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }
    }
}
