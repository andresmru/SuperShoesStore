using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperShoes.Model
{
    public class ProductExistenceDTO
    {
        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int TotalInShelf { get; set; }

        public int TotalInVault { get; set; }
    }
}
