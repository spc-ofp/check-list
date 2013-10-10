using CheckList.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckList.DAL.Map
{
    public class CheckTypeMap : ClassMap<CheckType>
    {
        public CheckTypeMap()
        {
            Schema("dbo");
            Table("check_type");
            Id(x => x.Code, "check_type_code");
            Map(x => x.Label, "Label");
        }
    }
}