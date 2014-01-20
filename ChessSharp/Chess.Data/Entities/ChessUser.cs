﻿using System;
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
    }
}