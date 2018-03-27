using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Models
{
    public class UserPermission 
    {
        //public int UserPermissionID { get; set; }
        public int UserID { get; set; }
        public int PermissionID { get; set; }

        public User User { get; set; }
        public Permission Permission { get; set; }
    }
}