using ExpenseManager.Models;
using ExpenseManager.NewFolder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using Microsoft.Reporting.WebForms;

namespace ExpenseManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExpenseDbContext dbContext;

        public HomeController(ExpenseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var expenses = dbContext.ExpensesTable.ToList();
            return View(expenses);
        }

        public IActionResult Reports()
        {
            var data = dbContext.ExpensesTable.Select(d => new ExpenseModel
            {
                Id = d.Id,
                Description = d.Description,
                Amount = d.Amount,
                Date = d.Date,
            }).ToList();

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ExpenseModel expense)
        {
            if (ModelState.IsValid)
            {
                var exp = new ExpenseModel
                {
                    Description = expense.Description,
                    Amount = expense.Amount,
                    Date = expense.Date,
                };
                dbContext.ExpensesTable.Add(exp);
                int result = dbContext.SaveChanges();
                if (result > 0)
                {
                    ViewBag.Msg = "Expense added successfully";
                    ViewBag.MsgType = "Success";
                }
                else
                {
                    ViewBag.Msg = "Unable to add the expense now, please try again later";
                    ViewBag.MsgType = "Error";
                }

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Msg = "Invalid data. Please check the form and try again.";
                ViewBag.MsgType = "Error";
                return View(expense);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var exp = dbContext.ExpensesTable.FirstOrDefault(x => x.Id == id);
            if (exp != null)
            {
                var upd = new ExpenseUpdateModel()
                {
                    Id = exp.Id,
                    Description = exp.Description,
                    Amount = exp.Amount,
                    Date = exp.Date,
                };
                return View(upd);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(ExpenseUpdateModel eum)
        {
            if (ModelState.IsValid)
            {
                var expense = dbContext.ExpensesTable.Find(eum.Id);
                if (expense != null)
                {
                    expense.Description = eum.Description;
                    expense.Amount = eum.Amount;
                    expense.Date = eum.Date;
                    int result= dbContext.SaveChanges();
                    if (result > 0)
                    {
                        ViewBag.Msg = "Expense updated successfully";
                        ViewBag.MsgType = "Success";
                    }
                    else
                    {
                        ViewBag.Msg = "Unable to update the expense now, please try again later";
                        ViewBag.MsgType = "Error";
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(eum);
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var expense = dbContext.ExpensesTable.FirstOrDefault(x => x.Id == id);
            if (expense != null)
            {
                dbContext.ExpensesTable.Remove(expense);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult SearchResults()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchResults(string SearchString)
        {
            var result = new List<ExpenseModel>();
            if (!String.IsNullOrEmpty(SearchString))
            {
                result = dbContext.ExpensesTable
                    .Where(i => i.Description.Contains(SearchString))
                    .ToList();
            }

            ViewBag.SearchString = SearchString;
            return View(result);
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(string user,string pass)
        {
            if (user =="prabin" && pass=="prabin007")
            {
                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.error = "user name and password dosen't matched";
                return View();
            }
        }

        public IActionResult LogOut()
        {
            return RedirectToAction("LogIn");
        }

    }
}
