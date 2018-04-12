using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.Common
{
    /// <summary>
    /// 消费类型
    /// </summary>
    public enum EnumPayWay
    {
        CashPayment = 1,
        AdvanceGas = 2,
        AdvanceMoney = 3,
        TallyGas = 4,
        TallyMoney = 5
    }
    public enum EnumUserStatus
    {
        Normal = 1,
        Disable = 0,
    }
    /// <summary>
    /// PosTranException类型
    /// (1.Add;2.Update)
    /// </summary>
    public enum EnumPosTranExceptionOperationType
    {
        Add = 1,
        Update = 2
    }

    /// <summary>
    /// CardRechargeException类型
    ///  (1.Add;2.Update)
    /// </summary>
    public enum EnumCardRechargeExceptionOperationType
    {
        Add = 1,
        Update = 2
    }
    /// <summary>
    /// 付款方式
    /// </summary>
    public enum EnumPayType
    {
        Cash = 1,
        BankCard = 2
    }
    /// <summary>
    /// 子母类型
    /// </summary>
    public enum EnumSubAndSuperType
    {
        Sub = 1,
        Super = 2,
        Sub_Super = 3
    }
    /// <summary>
    /// 气站类型
    /// </summary>
    public enum EnumGasType
    {
        LNG = 1,
        CNG = 2,
        L_CNG = 3
    }
    public enum EnumGasOsVersion
    {
        WinXP = 1,
        Win7 = 2,
        Win8 = 3,
        Win2003 = 4,
        MacOs = 5
    }
    /// <summary>
    /// 气站连接状态
    /// </summary>
    public enum EnumGasStationConnectionStatus
    {
        Online = 0,
        Offline = 1
    }
    /// <summary>
    /// 气站连接类型
    /// </summary>
    public enum EnumGasStationConnectionType
    {
        LAN = 1,
        GPRS = 2
    }
    /// <summary>
    /// Auth type, Form, WS_Federation, etc
    /// </summary>
    public enum EnumAuthenticationType
    {
        Form = 1,
        WS_Federation = 2,
        SAML20 = 3,
        Nothing = 4
    }
    /// <summary>
    /// DB Type(1.MSSQL;2.MYSQL;3.TBC)
    /// </summary>
    public enum EnumDbType
    {
        MSSQL = 1,
        MYSQL = 2,
        TBC = 3
    }
    /// <summary>
    /// Encoding Type(0.UTF-8;1.GBK )
    /// </summary>
    public enum EnumEncodingType
    {
        UTF_8 = 1,
        GBK = 2
    }
    /// <summary>
    /// DB Record Type
    /// </summary>
    public enum EnumDbRecordType
    {
        PosRecord = 1,
        CardRecord = 2
    }

    public enum EnumRole
    {
        SiteAdmin = 1,
        SysAdmin = 3,
        General = 7,
        Calibration = 8,
        Inspection = 9,
        BrowseUser = 10,
        NotLogin = 101
    }

    public enum EnumMenuId
    {
        Menu_GasStation = 10000,
        Menu_Logging = 20000,
        Menu_SysMaint = 90000
    }

    public enum EnumLight
    {
        GreenLight = 0,
        RedLight = 1
    }

    public enum EnumAcknowledgement
    {
        No = 0,
        Yes = 1
    }

    public enum EnumPowerMode
    {
        AC = 1,
        DC = 2
    }

    public enum EnumDataChannel
    {
        LAN = 1,
        GRPS = 2
    }

    public enum EnumJobType
    {
        PutNewGasStation = 1,
        PutPOSDBconnectInfo = 2,
        PutNewTASetting = 3,
        GetPOSTranRecord = 4,
        GetPOSICCardTopUpRecord = 5
    }

    public enum EnumBackupStatus
    {
        Processing = 0,
        Success = 1,
        Failure = 2
    }

    public enum EnumBackupType
    {
        Auto = 1,
        Manual = 2,
        Realtime = 3
    }

    public enum EnumNCHistoryStatus
    {
        SendFailed = -2,
        GenrateFailed = -1,
        QueueWait = 0,
        GenrateSuccessful = 1,
        SendSuccessful = 2,
    }

    public enum EnumMappingColumnType
    {
        Sales = 1,
        ICCard = 2
    }

    public enum TimerType
    {
        BackUpDataRecord = 1,
        CheckPushDataSituation = 2,
        JobTimeout = 3,
        ProcessData = 4,
        SendProcessData = 5,
        DeleteDataRecord = 6
    }

    public enum EnumEnableStatus
    {
        No = 0,
        Yes = 1
    }

    public enum EnumAssitType
    {
        /// <summary>
        /// 客商辅助核算_商业客户
        /// </summary>
        Customer_Commercial = 1,
        /// <summary>
        /// 客商辅助核算_零星客户
        /// </summary>
        Customer_Sporadic = 2,
        /// <summary>
        /// 部门辅助核算
        /// </summary>
        Department = 3,
        /// <summary>
        /// 资金账户
        /// </summary>
        Fund_Account = 4
    }

    public enum EnumCurrencyUnit
    {
        CNY = 1,
        HKD = 2
    }

    public enum EnumWeightUnit
    {
        Ton = 1,
        Kg = 2
    }

    public enum EnumPressureUnit
    {
        Mpa = 1
    }

    public enum EnumTemperatureUnit
    {
        Degree = 1
    }

    public enum BrowserVersionType
    {
        Forbid = -1,
        Cue = 0,
        Permit = 1
    }
}
