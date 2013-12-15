using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chess.Data.Entities
{
    public class Role : IModifiable
    {
        [Key]
        public virtual Guid RoleId { get; set; }

        [Required]
        public virtual string RoleName { get; set; }

        public virtual string Description { get; set; }

        public virtual ICollection<ChessUser> Users { get; set; }
    }
}