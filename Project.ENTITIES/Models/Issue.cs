using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Issue : BaseEntity
    {
        public Order Order { get; set; }
        public IssueStatus IssueStatus { get; set; }
        public string IssueDescription { get; set; }

        //Relational Properties
        public Issue()
        {
            IssueStatus = IssueStatus.Request;
        }
    }
}
