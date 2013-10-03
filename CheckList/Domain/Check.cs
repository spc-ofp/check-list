using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckList.Domain
{
    public class Check
    {
        public virtual int Id { get; set; }
        public virtual OfpSystem OfpSystem { get; set; }
        public virtual String Description { get; set; }
        public virtual Boolean IsImplemented { get; set; }
    }
}