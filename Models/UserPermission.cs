namespace StoreApp.Models
{
    public class UserPermission 
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserPermissionID { get; set; }
        public int UserID { get; set; }
        public int PermissionID { get; set; }

        public User User { get; set; }
        public Permission Permission { get; set; }
    }
}