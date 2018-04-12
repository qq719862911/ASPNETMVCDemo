using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVC2015.Web.Model.SystemMaint.Company
{
    public class CompanyUserList
    {
        public int UserId { get; set; }
        public string LogonName { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }

    }
}
