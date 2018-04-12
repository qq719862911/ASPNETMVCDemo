using MVC2015.Web.Model.Common;
using MVC2015.Web.Model.SystemMaint.Company;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;
using MD = MVC2015.DataProvider.MVC2015DB.Models;
using VM = MVC2015.Web.Model.SystemMaint.Company;

namespace MVC2015.Web.BusinessLogic.SystemMaint
{
    public class Company : IDisposable
    {
        private readonly CTX.MVC2015DBContext Ctx;

        public Company()
        {
            Ctx = new CTX.MVC2015DBContext();
        }

        public Company(CTX.MVC2015DBContext context)
        {
            Ctx = context;
        }

        public IQueryable<VM.CompanyModel> GetItems(CompanySearch search)
        {
            var result = from company in Ctx.tbl_Company.Where(c => c.IsDeleted != true).DefaultIfEmpty()
                        // join uc in Ctx.tbl_Common_UserOfCompany.Where(c => c.IsDeleted != true) on company.ID equals uc.CompanyId into ucinner
                        // from uci in ucinner.DefaultIfEmpty()
                        // where uci.UserId == search.UserId
                         select new VM.CompanyModel()
                         {
                             ID = company.ID,
                             Name = company.Name,
                             Code = company.Code,
                             Description = company.Description,
                             CreatedBy = company.CreatedBy,
                             CreatedDate = company.CreatedDate,
                             UpdatedBy = company.UpdatedBy,
                             UpdatedDate = company.UpdatedDate,
                             ProcessingTime = company.ProcessingTime,
                             ProcessingHour = company.ProcessingTime.Value.Hours,
                             ProcessingMinute = company.ProcessingTime.Value.Minutes,
                             SendTime = company.SendTime,
                             SendHour = company.SendTime.Value.Hours,
                             SendMinute = company.SendTime.Value.Minutes,
                             SendUrl = company.SendUrl,
                             EmailAddress = company.EmailAddress,
                             Rate = company.Rate,
                             Fee = company.Fee,
                             DocumentMaker = company.DocumentMaker,
                             IsDeleted = company.IsDeleted
                         };
            return result;
        }
        public VM.CompanyModel GetCurCompanyById(int gasStationId)
        {
            var result = (from c in Ctx.tbl_Company.Where(c => c.IsDeleted != true)

                          select new VM.CompanyModel()
                          {
                              ID = c.ID,
                              Name = c.Name,
                              Code = c.Code,
                              Description = c.Description,
                              CreatedBy = c.CreatedBy,
                              CreatedDate = c.CreatedDate,
                              UpdatedBy = c.UpdatedBy,
                              UpdatedDate = c.UpdatedDate,
                              ProcessingTime = c.ProcessingTime,
                              ProcessingHour = c.ProcessingTime.Value.Hours,
                              ProcessingMinute = c.ProcessingTime.Value.Minutes,
                              SendTime = c.SendTime,
                              SendHour = c.SendTime.Value.Hours,
                              SendMinute = c.SendTime.Value.Minutes,
                              SendUrl = c.SendUrl,
                              EmailAddress = c.EmailAddress,
                              Rate = c.Rate,
                              Fee = c.Fee,
                              DocumentMaker = c.DocumentMaker,
                              IsDeleted = c.IsDeleted
                          }).FirstOrDefault();
            return result;
        }


        public IQueryable<VM.CompanyModel> GetCompanyList()
        {
            var result = from company in Ctx.tbl_Company.Where(c => c.IsDeleted != true)
                         select new VM.CompanyModel()
                         {
                             ID = company.ID,
                             Name = company.Name,
                             Code = company.Code,
                             Description = company.Description,
                             CreatedBy = company.CreatedBy,
                             CreatedDate = company.CreatedDate,
                             UpdatedBy = company.UpdatedBy,
                             UpdatedDate = company.UpdatedDate,
                             ProcessingTime = company.ProcessingTime,
                             ProcessingHour = company.ProcessingTime.Value.Hours,
                             ProcessingMinute = company.ProcessingTime.Value.Minutes,
                             SendTime = company.SendTime,
                             SendHour = company.SendTime.Value.Hours,
                             SendMinute = company.SendTime.Value.Minutes,
                             SendUrl = company.SendUrl,
                             EmailAddress = company.EmailAddress,
                             Rate = company.Rate,
                             Fee = company.Fee,
                             DocumentMaker = company.DocumentMaker,
                             IsDeleted = company.IsDeleted
                         };
            return result;
        }

        public VM.CompanyModel GetCompanyById(int Id)
        {
            var result = from company in Ctx.tbl_Company.Where(c => c.IsDeleted != true)
                         where company.ID == Id
                         select new VM.CompanyModel()
                         {
                             ID = company.ID,
                             Name = company.Name,
                             Code = company.Code,
                             Description = company.Description,
                             ProcessingTime = company.ProcessingTime,
                             SendTime = company.SendTime,
                             SendUrl = company.SendUrl,
                             EmailAddress = company.EmailAddress,
                             Rate = company.Rate,
                             Fee = company.Fee,
                             DocumentMaker = company.DocumentMaker,
                             CreatedBy = company.CreatedBy,
                             CreatedDate = company.CreatedDate,
                             UpdatedBy = company.UpdatedBy,
                             UpdatedDate = company.UpdatedDate,
                             IsDeleted = company.IsDeleted
                         };
            return result.FirstOrDefault();
        }

        public VM.CompanyModel GetCompanyByIdForDelete(int Id)
        {
            var result = from company in Ctx.tbl_Company.Where(c => c.IsDeleted == true)
                         where company.ID == Id
                         select new VM.CompanyModel()
                         {
                             ID = company.ID,
                             Name = company.Name,
                             Code = company.Code,
                             Description = company.Description,
                             ProcessingTime = company.ProcessingTime,
                             SendTime = company.SendTime,
                             SendUrl = company.SendUrl,
                             EmailAddress = company.EmailAddress,
                             Rate = company.Rate,
                             Fee = company.Fee,
                             DocumentMaker = company.DocumentMaker,
                             CreatedBy = company.CreatedBy,
                             CreatedDate = company.CreatedDate,
                             UpdatedBy = company.UpdatedBy,
                             UpdatedDate = company.UpdatedDate,
                             IsDeleted = company.IsDeleted
                         };
            return result.FirstOrDefault();
        }

        public IQueryable<VM.CompanyModel> GetAllItems()
        {
            IQueryable<MD.tbl_Company> resultQueryable;
            resultQueryable = this.Ctx.tbl_Company;
            resultQueryable = resultQueryable.Where(m => m.IsDeleted != true);

            var result = resultQueryable.Select(a => new VM.CompanyModel()
            {
                ID = a.ID,
                Name = a.Name,
                Code = a.Code,
                Description = a.Description
            });
            return result;
        }

        public bool Update(VM.CompanyModel model, string allSelectList, int currCompanyId, string logonName)
        {

            MD.tbl_Company company = Ctx.tbl_Company.First(c => c.ID == model.ID && c.IsDeleted != true);
            company.Name = model.Name.Trim();
            company.Code = model.Code.Trim();
            company.Description = model.Description;
            company.ProcessingTime = DateTime.Now - DateTime.Now.AddHours(-1); ;
            company.SendTime = DateTime.Now - DateTime.Now.AddHours(-1); ;
            company.SendUrl = model.SendUrl;
            company.EmailAddress = model.EmailAddress;
            company.Rate = model.Rate;
            company.Fee = model.Fee;
            company.DocumentMaker = model.DocumentMaker;

            company.UpdatedBy = model.UpdatedBy;
            company.UpdatedDate = DateTime.Now;
            // 用户公司关系
            UpdateUserCompany(allSelectList, currCompanyId, logonName);
            Ctx.SaveChanges();
            return true;
        }

        public bool UpdateUserCompany(string allSelectList, int currCompanyId, string logonName)
        {

            var delItem = Ctx.tbl_Common_UserOfCompany.Where(m => m.CompanyId == currCompanyId);
            if (delItem.Count() > 0)
            {
                foreach (var item in delItem)
                {
                    item.UpdatedBy = logonName;
                    item.UpdatedDate = DateTime.Now;
                    item.IsDeleted = true;
                }
            }
            if (allSelectList == null)
            {
                Ctx.SaveChanges();
                return true;
            }
            var UserIdList = allSelectList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var UserId in UserIdList)
            {
                int uid = Int32.Parse(UserId);
                var item = Ctx.tbl_Common_UserOfCompany.Where(m => m.UserId == uid && m.CompanyId == currCompanyId).DefaultIfEmpty();
                if (item.Count() == 0 || item.First() == null)
                {
                    if (UserId != "")
                        Ctx.tbl_Common_UserOfCompany.Add(new MD.tbl_Common_UserOfCompany
                        {
                            UserId = int.Parse(UserId),
                            CompanyId = currCompanyId,
                            CreatedBy = logonName,
                            CreatedDate = DateTime.Now
                        });
                }
                else
                    item.First().IsDeleted = false;
            }
            Ctx.SaveChanges();
          
            return true;
        }

        public bool Create(VM.CompanyModel model)
        {
            MD.tbl_Company company = new MD.tbl_Company();
            //UserInfo userBL = new UserInfo();

            company.Name = model.Name.Trim();
            company.Code = model.Code.Trim();
            company.Description = model.Description;
            company.ProcessingTime = DateTime.Now - DateTime.Now.AddHours(-1);//TimeSpan.Parse(model.ProcessingHour + ":" + model.ProcessingMinute);
            company.SendTime = DateTime.Now - DateTime.Now.AddHours(-1); //TimeSpan.Parse(model.SendHour + ":" + model.SendMinute);
            company.SendUrl = "";
            company.EmailAddress = "";
            company.CreatedBy = "sysadmin";//model.CreatedBy;
            company.CreatedDate = DateTime.Now;
            company.Rate = model.Rate;
            company.Fee = model.Fee;
            company.DocumentMaker ="";
            //userBL.UpdateUserCompany(allSelectList);
            Ctx.tbl_Company.Add(company);
            try
            {
                Ctx.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
        }
        public bool Delete(int Id, string Name)
        {
            var model = false;// Ctx.tbl_Gs_GasStationInfo.Any(c => c.Company == Id && c.IsDelete != true);

            if (model)
            {
                return false;
            }

            MD.tbl_Company company = Ctx.tbl_Company.First(c => c.ID == Id);
            company.IsDeleted = true;
            company.UpdatedDate = DateTime.Now;
            company.UpdatedBy = Name;
            Ctx.SaveChanges();
            return true;
        }

        public string GetCurrCompanyNameById(int Id)
        {
            MD.tbl_Company company = Ctx.tbl_Company.FirstOrDefault(c => c.ID == Id && c.IsDeleted != true);
            if (company != null)
            {
                return company.Name;
            }
            else
            {
                return null;
            }
        }

        public List<VM.CompanyModel> GetCompanyListByUserId(int id)
        {
            var item = (from uc in Ctx.tbl_Common_UserOfCompany
                        join c in Ctx.tbl_Company on uc.CompanyId equals c.ID into cinner
                        from ci in cinner.DefaultIfEmpty()
                        where uc.UserId == id && uc.IsDeleted != true
                        select new VM.CompanyModel
                        {
                            ID = uc.CompanyId,
                            Name = ci.Name
                        }

                ).ToList();

            return item;
        }

        public List<VM.CompanyMap> GetAllCompanyMap()
        {
            var result = (from c in Ctx.tbl_Company
                          where c.IsDeleted != true
                          select new VM.CompanyMap()
                          {
                              CompanyId = c.ID,
                              CompanyName = c.Name
                          }
                ).ToList();

            return result;
        }

        public bool ValidateCompanyName(int id, string name)
        {
            return Ctx.tbl_Company.Any(m => m.ID != id
                && m.Name == name.Trim()
                && m.IsDeleted != true);
        }

        public bool ValidateDelete(int id)
        {
            int sysadminid = Ctx.tbl_Common_User.Where(c => c.LogonName.ToLower() == "sysadmin").FirstOrDefault().UserId;
            int countCompanyId = this.Ctx.tbl_Common_UserOfCompany.Where(m => m.CompanyId == id
                && m.IsDeleted != true).Count();// && m.UserId != sysadminid).Count();
          
            if (countCompanyId>0)
            {
                return true;
            }
            return false;

        }

        public bool ValidateDeleteForUser(int id, int userId)
        {
            int countCompanyId = this.Ctx.tbl_Common_UserOfCompany.Where(m => m.CompanyId == id
                && m.IsDeleted != true && m.UserId == userId).Count();
            if (countCompanyId > 0)
                return true;
            else
                return false;

        }

        public void Dispose()
        {
            if (Ctx != null)
            {
                Ctx.Dispose();
            }
        }
    }
}
