using MVC2015.Web.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC2015.Web.Site.Common
{
    public class BasicParam
    {
        public const string Default_ConnectionName = "GSIMSDB";
        public const string Default_Lange = "en-us";
        public const string LANGUAGE_DB = "dbLang";
        public const string LANGUAGE = "lang";
        public const string COMPANY_CODE = "companyCode";

        public const string NamingCacheKey = "NamingCustomization";
        public const string CompanyDisplayAttributeKey = "CompanyDisplayAttribute";
        public const string SimpleCompanyModelKey = "SimpleCompanyModel";
        public const string SystemParameterKey = "SystemParameter";

        public const string DatetimeFormat = "yyyy-MM-dd HH:mm:ss";
        public const string DatetimeFormat_yyyy_MM_dd_HH_mm = "yyyy-MM-dd HH:mm";
        public const string DatetimeFormat_yyyymmddhhmm = "yyyyMMddHHmm";
        public const string DatetimeFormat_HH_mm = "HH:mm";
        public const string DatetimeFormat_23_59_59 = "yyyy-MM-dd 23:59:59";
        public const string ShortDatetimeFormat = "yyyy-MM-dd";
        public const string ShrotDatetimeFormat_dd_mm_yy = "dd-MM-yyyy";
        public const string YearMonthFormat = "yyyy-MM";

        public const string JS_yy_mm_dd = "yy-mm-dd";
        public const string JS_dd_mm_yy = "dd-mm-yy";

        public const string ExcelTemplateFileName = @"~\TemplateFile\Excel.xlsx";
        public const string ExcelTempFilePath = @"~\TempFile\";

        public const string CodeVerson = "CodeVersion";

        public const string AppUrl = "AppUrl";
        public const string ForceHttps = "ForceHttps";
        public const string LocalUrl = "LocalUrl";

        public const string GridViewPageRowSizeCookiesKey = "GridViewPageRowSize";

        public static readonly Dictionary<int, string> Themes = new Dictionary<int, string>{
           {12,"Default"},
           {13,"Youthful"},
           {14,"MetropolisBlue"},
           {17,"Office2010Blue"}
        };
        public const string ResetPasswordValue = "True";
        public const string ResetPasswordName = "ResetPassword";

        public const string RolePermissionCacheKey = "RolePermissionCache";

        public static string GetLightPath(EnumLight lightStatus)
        {
            switch (lightStatus)
            {
                case EnumLight.RedLight: return @"~/Images/Icon/RedLight.gif";
                case EnumLight.GreenLight: return @"~/Images/Icon/GreenLight.gif";
                default: return @"~/";
            }
        }

        public const string NCHistory_DataTypeName_SuccessfulXML = "SuccessfulXML";
        public const string NCHistory_DataTypeName_FailedXML = "FailedXML";
        public const string NCHistory_DataTypeName_Exception = "Exception";

    }
}