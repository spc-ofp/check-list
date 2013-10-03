using CheckList.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckList.DAL.Map
{
    public class CheckMap : ClassMap<Check>
    {
        public CheckMap()
        {
            Schema("dbo");
            Table("quality_check");
            Id(x => x.Id, "check_id");
            References(x => x.OfpSystem, "ofp_system_id");
            Map(x => x.Description, "description");
            Map(x => x.IsImplemented, "is_implemented");
        }
    }
}