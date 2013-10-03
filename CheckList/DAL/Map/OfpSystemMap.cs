using CheckList.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckList.DAL.Map
{
    public class OfpSystemMap : ClassMap<OfpSystem>
    {
        public OfpSystemMap()
        {
            Schema("dbo");
            Table("ofp_system");
            Id(x => x.Id, "ofp_system_id");
            Map(x => x.Label, "Label");
        }
    }
}