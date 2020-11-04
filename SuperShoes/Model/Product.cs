using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperShoes.Model
{
    [Table("Products", Schema = "dbo")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public Decimal Price { get; set; }

        [Required]
        public byte StatusId { get; set; }
    }
}
