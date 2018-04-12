using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VM = MVC2015.Web.Model.SystemMaint.Company;
using BL = MVC2015.Web.BusinessLogic.SystemMaint;

namespace MVC2015.Web.Site.Common
{
    public static class CompanyMap
    {

        public static List<VM.CompanyMap> GetALLCompanyMap()
        {
            BL.Company companyBL = new BL.Company();
            List<VM.CompanyMap> companyMap = new List<VM.CompanyMap>();
            companyMap = companyBL.GetAllCompanyMap();

            return companyMap;
        }
    }
}