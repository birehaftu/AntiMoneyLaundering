using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;

namespace AntiLaundering.Control.ResourceManagement
{
    public class CompanyManagement
    {
        AntiMoney_UMEntities sms = new AntiMoney_UMEntities();
        public Boolean CompanyExists(String Company)
        {
            var rop = (from l in sms.Companies where l.CompanyName == Company select l).FirstOrDefault();
            if (rop != null)
                return true;
            else
            {
                Boolean x = CompanyExistsUM(Company);
                return x;
            }
        }
        public Boolean CompanyExistsUM(String Company)
        {
            var rop = (from l in sms.Companies where l.CompanyName == Company select l).FirstOrDefault();
            if (rop != null)
                return true;
            else
                return false;
        }
        public Boolean InsertCompany(Guid CompanyId, String CompanyLogo,String imagePhoto, String CompanyName, String CompanyType, String Descrbition,
            String Fax, String ManagerName, String Mobile, int NumberOfVehicles, String POBOX, String SubCity, String Tel, String Woreda, String City,String Email)
        {
            Company com = new Company();
            com.CompanyId = CompanyId;
            com.CompanyLogo = CompanyLogo;
            com.Photo = imagePhoto;
            com.CompanyName = CompanyName;
            com.CompanyType = CompanyType;
            com.Descrbition = Descrbition;
            com.Fax = Fax;
            com.ManagerName = ManagerName;
            com.Mobile = Mobile;
            com.NumberOfVehicles = NumberOfVehicles;
            com.POBOX = POBOX;
            com.SubCity = SubCity;
            com.Tel = Tel;
            com.Woreda = Woreda;
            com.City = City;
            com.Email = Email;
            sms.Companies.Add(com);
            int change = sms.SaveChanges();
            if (change > 0)
            {
               return true;
                //return true;
            }
            else
                return false;
        }
        public Boolean UpdateCompany(Guid CompanyId, String CompanyLogo, String imagePhoto, String CompanyName, String CompanyType, String Descrbition,
            String Fax, String ManagerName, String Mobile, int NumberOfVehicles, String POBOX, String SubCity, String Tel, String Woreda, String City, String Email)
        {
            //Company com = new Company();
            var com = (from d in sms.Companies where d.CompanyId == CompanyId select d).FirstOrDefault();
            com.CompanyId = CompanyId;
            com.CompanyLogo = CompanyLogo;
            com.Photo = imagePhoto;
            com.CompanyName = CompanyName;
            com.CompanyType = CompanyType;
            com.Descrbition = Descrbition;
            com.Fax = Fax;
            com.Email = Email;
            com.ManagerName = ManagerName;
            com.Mobile = Mobile;
            com.NumberOfVehicles = NumberOfVehicles;
            com.POBOX = POBOX;
            com.SubCity = SubCity;
            com.Tel = Tel;
            com.Woreda = Woreda;
            com.City = City;
            int change = sms.SaveChanges();
            if (change > 0)
            {
                return true;
                //return true;
            }
            else
                return false;
        }
        public Boolean DeleteCompany(Guid Id)
        {
            try
            {
                var company = (from c in sms.Companies where c.CompanyId == Id select c).FirstOrDefault();
                //var compan = (from c in ctx.Companies where c.CompanyId == company.CompanyId select c).FirstOrDefault();
                sms.Companies.Remove(company);
                int change = sms.SaveChanges();
                if (change > 0)
                {
                   // DeleteCompanyUM(ctx, compan);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Company> GetCompany()
        {
            var comp_data = (from v in sms.Companies select v).ToList();
            return comp_data;
        }

        public Company GetCompanyById(Guid Id)
        {
            var comp_data = (from d in sms.Companies where d.CompanyId == Id select d).FirstOrDefault();
            return comp_data;
        }
    }
}