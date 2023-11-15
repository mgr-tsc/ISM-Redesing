using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM_Redesing.Models;
using System.Net.Mime;

namespace ISM_Redesign.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly List<Expense> _expenses;

        public ExpenseController()
        {
            _expenses = new List<Expense>
                        {
                            new Expense { ExpenseID = 1, Description = "Expense 1", Amount = 100 },
                            new Expense { ExpenseID = 2, Description = "Expense 2", Amount = 200 },
                            new Expense { ExpenseID = 3, Description = "Expense 3", Amount = 300 }
                        };
        }

        /// <summary>
        /// Gets all expenses.
        /// </summary>
        /// <returns>A list of all expenses.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await Task.FromResult(_expenses.ToList());
        }

        /// <summary>
        /// Gets an expense by ID.
        /// </summary>
        /// <param name="id">The ID of the expense to retrieve.</param>
        /// <returns>The expense with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = _expenses.FirstOrDefault(e => e.ExpenseID == id);

            if (expense == null)
            {
                return NotFound();
            }

            return await Task.FromResult(expense);
        }

        /// <summary>
        /// Creates a new expense.
        /// </summary>
        /// <param name="expense">The expense to create.</param>
        /// <returns>The newly created expense.</returns>
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            expense.ExpenseID = _expenses.Count + 1;
            _expenses.Add(expense);

            return await Task.FromResult(CreatedAtAction(nameof(GetExpense), new { id = expense.ExpenseID }, expense));
        }

        /// <summary>
        /// Updates an existing expense.
        /// </summary>
        /// <param name="id">The ID of the expense to update.</param>
        /// <param name="expense">The updated expense.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.ExpenseID)
            {
                return BadRequest();
            }

            var existingExpense = _expenses.FirstOrDefault(e => e.ExpenseID == id);

            if (existingExpense == null)
            {
                return NotFound();
            }

            existingExpense.Description = expense.Description;
            existingExpense.Amount = expense.Amount;

            return await Task.FromResult(NoContent());
        }

        /// <summary>
        /// Deletes an expense by ID.
        /// </summary>
        /// <param name="id">The ID of the expense to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = _expenses.FirstOrDefault(e => e.ExpenseID == id);

            if (expense == null)
            {
                return NotFound();
            }

            _expenses.Remove(expense);

            return await Task.FromResult(NoContent());
        }
    }
}