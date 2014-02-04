using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chess.Data.Entities
{
    [Table("IdentityUser")]
    public class ChessUser : IdentityUser, IModifiable
    {
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        [DataType(DataType.MultilineText)]
        public String Comment { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime CreateDate { get; set; }

        [MaxLength(30)]
        public string DisplayName { get; set; }

        public int Rank { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }

        public ICollection<Challenge> Challenges { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}