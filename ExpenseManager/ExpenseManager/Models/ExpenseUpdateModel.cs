using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models
{
    public class ExpenseUpdateModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public float Amount { get; set; }

        public DateTime Date { get; set; }

    }
}
