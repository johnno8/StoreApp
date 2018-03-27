using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Models.ViewModels
{
    public class UserPermissionData
    {
        //public int UserID { get; set; }
        public int PermissionID { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}