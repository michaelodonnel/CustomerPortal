using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Customer
    {
        [Key]
        [Required]
        [Column("CustomerId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(9)]
        public string ReferenceNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
    }
}
