using ExpenseManager.Models;
using Microsoft.EntityFrameworkCore;
namespace ExpenseManager.NewFolder
{
    public class ExpenseDbContext:DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) 
        {
        }
        public DbSet<ExpenseModel> ExpensesTable { get; set; }
    }
}
