using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter the description")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Enter the amount")]
        public float Amount {  get; set; }

        [Required(ErrorMessage ="Choose the date")]
        public DateTime Date { get; set; }
        public int DisplayOrder { get; set; }
    }
}
