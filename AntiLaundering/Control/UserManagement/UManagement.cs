using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;

namespace AntiLaundering.Control.UserManagement
{
    public class UManagement
    {
        AntiMoney_UMEntities hrm = new AntiMoney_UMEntities();
        //public Boolean CompanyExists(Guid CompanyId)
        //{
        //    AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
        //    var rop = (from l in entities.Companies where l.CompanyId==CompanyId select l).FirstOrDefault();
        //    if (rop != null)
        //        return true;
        //    else
        //        return false;
        //}

        //public Boolean VehicleExists(String PlateNum)
        //{
        //    AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
        //    var rop = (from l in entities.Vehicles where l.PlateNumber == PlateNum select l).FirstOrDefault();
        //    if (rop != null)
        //        return true;
        //    else
        //        return false;
        //}
        public Boolean RoleExists(String RoleN)
        {
            try
            {
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.Roles where l.ROLENAME == RoleN select l).FirstOrDefault();
                if (rop != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Boolean UserExists(String UName)
        {
            try
            {
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.Users where l.USERNAME == UName select l).FirstOrDefault();
                if (rop != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean OperationExists(String OpName)
        {
            try
            {
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.Operations where l.OperationName == OpName select l).FirstOrDefault();
                if (rop != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int? getSubSytem(String MName)
        {
            try
            {
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.Operations where l.ModuleName == MName select l).FirstOrDefault();
                if (rop != null)
                    return rop.SubSystem;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Guid getSubSytemByName(String MName)
        {
            try
            {
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.Operations where l.ModuleName == MName select l).FirstOrDefault();
                if (rop != null)
                    return rop.SubSystemID??Guid.Empty;
                else
                    return Guid.Empty;
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }
        public List<User> getAllUser(Guid Com)
        {
            try
            {
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.Users where l.CompanyId == Com select l).ToList();
                if (rop != null)
                    return rop;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Boolean SubSytemExists(String OpName)
        {
            try
            {
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.SubSystems where l.SubSystemName == OpName select l).FirstOrDefault();
                if (rop != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public String GetOperationModules(int id)
        {
            try
            {
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.Operations where l.Id == id select l).FirstOrDefault();
                if (rop != null)
                {
                    return rop.ModuleName;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public String GetOperationModules(Guid id)
        {
            try
            {
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.Operations where l.OperationID == id select l).FirstOrDefault();
                if (rop != null)
                {
                    return rop.ModuleName;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public Boolean ModuleInRoleExists(int id, String user)
        {
            try
            {
                String ModuleName = "";
                ModuleName = GetOperationModules(id);
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.VWRoleOperationAssigns where l.ModuleName == ModuleName && l.USERNAME == user select l).FirstOrDefault();
                if (rop != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Boolean ModuleInRoleExists(Guid id, String user)
        {
            try
            {
                String ModuleName = "";
                ModuleName = GetOperationModules(id);
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.VWRoleOperationAssigns where l.ModuleName == ModuleName && l.USERNAME == user select l).FirstOrDefault();
                if (rop != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public String GetModuleName(String operationlink)
        {
            try
            {
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.Operations where l.OperationLink == operationlink select l).FirstOrDefault();
                if (rop != null)
                    return rop.ModuleName;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public String GetCompanyId(String Uname)
        {
            try
            {
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.VWRoleOperationAssigns where l.USERNAME == Uname select l).FirstOrDefault();
                if (rop != null)
                    return rop.CompanyId.ToString();
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int? GetAccessLeve(String operationlink, String user)
        {
            try
            {
                String ModuleName = "";
                //ModuleName = GetModuleName(operationlink);
                AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
                var rop = (from l in entities.VWRoleOperationAssigns where l.OperationLink == operationlink && l.USERNAME == user select l).FirstOrDefault();
                if (rop != null)
                    return rop.AccessLevel;
                else
                    return 4;
            }
            catch (Exception ex)
            {
                return 4;
            }
        }
    }
}