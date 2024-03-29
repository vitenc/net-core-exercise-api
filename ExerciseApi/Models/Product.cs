using System.ComponentModel.DataAnnotations.Schema;

namespace ExerciseApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder {get; set; }
        public short ReorderLevel { get; set; }
        public bool Discontinued {get; set; }


        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int SupplierId { get; set; }
        public string Supplier { get; set; }



        /* //FK is not necessary now, as we get infor from SPs 
        [ForeignKey("ProductId")]
        public Category Category { get; set; }
        */
    }
}