using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookMyMeal.Data.DataLayer
{
    [Table("employeelogin")]
    public partial class Employeelogin
    {
        public Employeelogin()
        {
            BookmymealCreatedByNavigations = new HashSet<Bookmymeal>();
            BookmymealEmployeeLogins = new HashSet<Bookmymeal>();
        }

        [Key]
        [Column("emp_id")]
        public long EmpId { get; set; }
        [Column("name")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("email")]
        [StringLength(100)]
        [Unicode(false)]
        public string Email { get; set; } = null!;
        [Column("password")]
        [StringLength(255)]
        [Unicode(false)]
        public string Password { get; set; } = null!;

        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<Bookmymeal> BookmymealCreatedByNavigations { get; set; }
        [InverseProperty("EmployeeLogin")]
        public virtual ICollection<Bookmymeal> BookmymealEmployeeLogins { get; set; }
    }
}
