using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using AntiLaundering.Model;
using MyUserManagement;
namespace AntiLaundering.Control.UserManagement
{
    public class UserRegiManagement
    {
        AntiMoney_UMEntities hrm = new AntiMoney_UMEntities();
        public Boolean UserExists(String UName)
        {
            AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
            try
            {
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
        public List<User> getAllUsers()
        {
            try
            {
                var user = (from u in hrm.Users select u).ToList();
                return user;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public List<User> getAllUsers(String RoleName)
        {
            try
            {
                var inroles = (from u in hrm.Usersinroles where u.ROLENAME == RoleName select u.USERNAME).ToList();
                var user = (from u in hrm.Users where inroles.Contains(u.USERNAME) select u).ToList();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public User getUserByName(string userName)
        {

            try
            {
                var user = (from u in hrm.Users
                            where u.USERNAME == userName
                            select u).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Boolean updateUserLogIn(string userName, DateTime lastLogIn)
        {
            int change = -1;

            try
            {
                var user = (from u in hrm.Users
                            where u.USERNAME == userName
                            select u).FirstOrDefault();
                if (user != null)
                {
                    user.LASTLOGINDATE = lastLogIn;
                    user.LASTACTIVITYDATE = lastLogIn;
                    change = hrm.SaveChanges();
                }
                if (change > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Boolean updateUserStatus(string EmployeeId, bool status)
        {
            int change = -1;

            try
            {
                var user = (from u in hrm.Users
                            where u.EmployeeId == EmployeeId
                            select u).FirstOrDefault();
                if (user != null)
                {
                    user.ISLOCKEDOUT = status;
                    change = hrm.SaveChanges();
                }
                if (change > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //public Boolean InsertUser(Guid MaintenanceId, String Type, String State, String GarageName,DateTime DateIn,
        //     Double MileageIn, String Description, DateTime AssumedEndDate, DateTime DateOut, Double MileageOut
        //    , String Result, String Approver, Guid MaintainedVehicleId, Guid DriverInId, Guid DriverOutId)
        //{
        //    User MI = new User();
        //    MI.MaintenanceId = MaintenanceId;
        //    MI.Type = Type;
        //    MI.State = State;
        //    MI.GarageName = GarageName;
        //    MI.DateIn = DateIn;
        //    MI.MileageIn = MileageIn;
        //    MI.Description = Description;
        //    MI.AssumedEndDate = AssumedEndDate;
        //    MI.DateOut = DateOut;
        //    MI.MileageOut = MileageOut;
        //    MI.Result = Result;
        //    MI.Approver = Approver;
        //    MI.MaintainedVehicleId = MaintainedVehicleId;
        //    MI.DriverInId = DriverInId;
        //    MI.DriverOutId = DriverOutId;
        //    hrm.Users.Add(MI);
        //    int change = hrm.SaveChanges();
        //    if (change > 0)
        //        return true;
        //    else
        //        return false;
        //}
        //public Boolean UpdateUser(Guid MaintenanceId, String Type, String State, String GarageName, DateTime DateIn,
        //     Double MileageIn, String Description, DateTime AssumedEndDate, DateTime DateOut, Double MileageOut
        //    , String Result, String Approver, Guid MaintainedVehicleId, Guid DriverInId, Guid DriverOutId)
        //{
        //    var MI = (from d in hrm.Users where d.MaintenanceId == MaintenanceId select d).FirstOrDefault();
        //    MI.MaintenanceId = MaintenanceId;
        //    MI.Type = Type;
        //    MI.State = State;
        //    MI.GarageName = GarageName;
        //    MI.DateIn = DateIn;
        //    MI.MileageIn = MileageIn;
        //    MI.Description = Description;
        //    MI.AssumedEndDate = AssumedEndDate;
        //    MI.DateOut = DateOut;
        //    MI.MileageOut = MileageOut;
        //    MI.Result = Result;
        //    MI.Approver = Approver;
        //    MI.MaintainedVehicleId = MaintainedVehicleId;
        //    MI.DriverInId = DriverInId;
        //    MI.DriverOutId = DriverOutId;
        //    int change = hrm.SaveChanges();
        //    if (change > 0)
        //        return true;
        //    else
        //        return false;
        //}
        //public Boolean DeleteUser(Guid MaintenanceId)
        //{
        //    try
        //    {
        //        var Dep = (from c in hrm.Users where c.MaintenanceId == MaintenanceId select c).FirstOrDefault();
        //        hrm.Users.Remove(Dep);
        //        int change = hrm.SaveChanges();
        //        if (change > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public List<VWMaintenance> getUsers()
        //{
        //    AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
        //    var rop = (from l in entities.VWMaintenances select l).ToList();
        //    return rop;
        //}
        //public List<User> getUsersByVehicle(Guid Id)
        //{
        //    AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
        //    var rop = (from l in entities.Users where l.MaintainedVehicleId == Id select l).ToList();
        //    return rop;
        //}

        //public User getUsertById(Guid Id)
        //{
        //    AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
        //    var rop = (from l in entities.Users where l.MaintenanceId == Id select l).FirstOrDefault();
        //    return rop;
        //}
    }
}