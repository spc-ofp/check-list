using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using CheckList.Domain;

namespace CheckList.DAL.Repository
{
    public class CheckRepository
    {
        protected ISession _session;

        public CheckRepository(ISession session)
        {
            _session = session;
        }

        public List<Check> GetChecks(String description, String ofpSystem, bool? isImplemented, int? limit, int offset)
        {
            var query = _session.Query<Check>();
            if (!String.IsNullOrEmpty(description))
                query = query.Where(x => x.Description.Contains(description));
            if (!String.IsNullOrEmpty(ofpSystem))
                query = query.Where(x => x.OfpSystem.Label.Equals(ofpSystem));
            if (isImplemented.HasValue)
                query = query.Where(x => x.IsImplemented == isImplemented.Value);
            if (offset > 0)
                query = query.Skip(offset);
            if (limit.HasValue)
                query = query.Take(limit.Value);
            return query.ToList();
        }
    }
}