using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;

namespace AntiLaundering.Control.UserManagement
{
    public class ModulesLists
    {
        public List<VWModule> getModueses()
        {
            AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
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
            return OperationFiltered_data;
        }
        public List<SubSystem> getSubSytems() 
        {
            AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
            var rop = (from l in entities.SubSystems select l).ToList();
            var OperationFiltered_data = new List<SubSystem>();
            var ModuleNames_data = new List<String>();
            SubSystem od;
            foreach (SubSystem l in rop)
            {
                od = new SubSystem();
                if (!ModuleNames_data.Contains(l.Id.ToString()))
                {
                    OperationFiltered_data.Add(l);
                    ModuleNames_data.Add(l.Id.ToString());
                }
            }
            return OperationFiltered_data;
        }
    }
}