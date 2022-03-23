using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelTogether2.Models
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        [ForeignKey(nameof(UserEmail))]
        public Account Account { get; set; }


        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DataType IssueAt { get; set; }
        public DataType ExpriedAt { get; set; }


    }
}
