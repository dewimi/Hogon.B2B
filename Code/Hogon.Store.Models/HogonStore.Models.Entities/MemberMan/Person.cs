﻿using Hogon.Store.Models.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.MemberMan
{
    /// <summary>
    /// 个人
    /// </summary>
    public class Person : Account
    {
        public override IQueryable<Function> GetAvailableFunctions()
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Menu> GetAvailableMenus()
        {
            throw new NotImplementedException();
        }
    }
}
