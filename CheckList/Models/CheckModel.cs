using CheckList.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckList.Models
{
    public class CheckModel
    {
        public int Id { get; set; }
        public String OfpSystem { get; set; }
        public String Description { get; set; }
        public int IsImplemented { get; set; }

        public CheckModel(Check check)
        {
            this.Id = check.Id;
            this.OfpSystem = check.OfpSystem.Label;
            this.Description = check.Description;
            this.IsImplemented = Convert.ToInt32(check.IsImplemented);
        }

        public CheckModel() { }
    }
}