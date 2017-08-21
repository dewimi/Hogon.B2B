﻿using Hogon.Store.Models.Entities.MemberMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.Security
{
    public interface IRoleFactory
    {
        IRole Create(Account account);
    }
}