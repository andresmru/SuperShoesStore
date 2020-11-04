using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperShoes.Model
{
    [Table("ProductExistences", Schema = "dbo")]
    public class ProductExistence
    {
        [Required]
        public int StoreId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int TotalInShelf { get; set; }

        [Required]
        public int TotalInVault { get; set; }
    }
}
