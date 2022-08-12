using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;

namespace AntiLaundering.Control.UserManagement
{
    public class IndexUserMgnt
    {
        AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
        public Operation getOperationByOPName(String OpName)
        {
            try
            {
                var rop = (from l in entities.VWModules select l).ToList();
                var OperationFiltered_data = new List<VWModule>();
                var ModuleNames_data = new List<String>();
                VWModule od;
                foreach (VWModule l in rop)
                {
                    od = new VWModule();
                    if (!ModuleNames_data.Contains(l.ModuleName))
                    {
                        OperationFiltered_data.Add(l);
                        ModuleNames_data.Add(l.ModuleName);
                    }
                }
                Operation Opera = entities.Operations.Where(p => p.OperationName == OpName).FirstOrDefault();

                return Opera;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<VWRoleOperation> getOperationsByUserName(String UName)
        {
            try
            {
                var op = (from v in entities.VWRoleOperations
                          where v.USERNAME == UName && (String.IsNullOrEmpty(v.OperationLink) ||
                          (v.OperationName == v.ModuleName && !String.IsNullOrEmpty(v.OperationLink)))
                          orderby v.Rank, v.OperationName
                          select v).ToList();
                var rop = new List<VWRoleOperation>();
                var lt = new List<int>();
                foreach (VWRoleOperation l in op)
                {
                    Boolean found = false;
                    foreach (var list in lt)
                    {
                        if (list.Equals(l.Id))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        rop.Add(l);
                        lt.Add(l.Id);
                    }
                }
                return rop;
            }
            catch (Exception ex)
            {
                string mesg = ex.InnerException.Message;
                return null;
            }
        }
        public List<VWRoleOperation> getOperationsByModuleName(String ModuleName,string uname)
        {
            try
            {
                var op = (from list in entities.VWRoleOperations
                          where !String.IsNullOrEmpty(list.OperationLink) && list.USERNAME==uname && list.ModuleName == ModuleName
                          orderby list.Rank ascending, list.OperationName, list.Id
                          select list).ToList();
                var rop = new List<VWRoleOperation>();
                var lt = new List<String>();
                foreach (VWRoleOperation l in op)
                {
                    Boolean found = false;
                    foreach (var list in lt)
                    {
                        if (list.Equals(l.OperationName))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        rop.Add(l);
                        lt.Add(l.OperationName);
                    }
                }
                return rop;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public VWRoleOperation getFirstOperationsByModuleName(String ModuleName)
        {
            try
            {
                var op = (from list in entities.VWRoleOperations
                          where !String.IsNullOrEmpty(list.OperationLink) && list.ModuleName == ModuleName
                          orderby list.Rank ascending, list.OperationName, list.Id
                          select list).FirstOrDefault();
                return op;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Operation getOperationsByID(int OpId)
        {
            try
            {
                Operation Ope = entities.Operations.Where(p => p.Id == OpId).FirstOrDefault();
                return Ope;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Operation getOperationsByOperarionID(Guid OpId)
        {
            try
            {
                Operation Ope = entities.Operations.Where(p => p.OperationID == OpId).FirstOrDefault();
                return Ope;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int getSubSystemByName(String SName)
        {
            try
            {
                int x = 0;
                SubSystem Ope = entities.SubSystems.Where(p => p.SubSystemName == SName).FirstOrDefault();
                if (Ope != null)
                    x = Ope.Id;
                return x;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Guid getSubSystemIDByName(String SName)
        {
            try
            {
                Guid x = Guid.Empty;
                SubSystem Ope = entities.SubSystems.Where(p => p.SubSystemName == SName).FirstOrDefault();
                if (Ope != null)
                    x = Ope.SubSystemID;
                return x;
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }
    }
}