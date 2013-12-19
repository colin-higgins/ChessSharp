using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chess.Data.Entities
{
    public class ChessUser : IdentityUser, IModifiable
    {
        [Key]
        public long ChessUserId { get; set; }
        public virtual String Email { get; set; }
        public virtual String FirstName { get; set; }
        public virtual String LastName { get; set; }

        [DataType(DataType.MultilineText)]
        public virtual String Comment { get; set; }

        public virtual DateTime? LastActivityDate { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? LastPasswordChangedDate { get; set; }
    }
}