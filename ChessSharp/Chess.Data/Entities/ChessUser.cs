using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chess.Data.Entities
{
    public class ChessUser : IdentityUser, IModifiable
    {
        public long ChessUserId { get; set; }
        public virtual String Email { get; set; }
        public virtual String FirstName { get; set; }
        public virtual String LastName { get; set; }

        [DataType(DataType.MultilineText)]
        public String Comment { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}