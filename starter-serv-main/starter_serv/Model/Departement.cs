using Google.Protobuf.WellKnownTypes;
using System.ComponentModel.DataAnnotations;

namespace starter_serv.Model
{
    public class Departement : EntityBase
    {
        [Key]
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }

    public class DepartementRoleMenu
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64 DepartementId { get; set; }
        public Int64 RoleId { get; set; }
        public Int64 MenuId { get; set; }
        public int View { get; set; }
        public int Created { get; set; }
        public int Edited { get; set; }
        public int Deleted { get; set; }
    }

}
