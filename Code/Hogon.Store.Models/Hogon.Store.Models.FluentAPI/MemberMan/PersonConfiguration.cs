﻿using Hogon.Store.Models.Entities.MemberMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.MemberMan
{
    public class PersonConfiguration: EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            Map(m =>
            {
                m.ToTable("Person");
            });
        }
    }
}
