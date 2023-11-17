using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM_Redesing.Models;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using ISM_Redesign.Data;
using Microsoft.EntityFrameworkCore;

namespace ISM_Redesign.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class ExpenseController : ControllerBase
    {
        private readonly List<Expense> _expenses;
        private readonly IsmDbContext _dbContext;
        public ExpenseController(IsmDbContext dbContext)
        {
            _dbContext = dbContext;
            _expenses = new List<Expense>
                        {
                            new Expense { ExpenseID = 1, Description = "Expense 1", Amount = 100, Category = "Fuel"},
                            new Expense { ExpenseID = 2, Description = "Expense 2", Amount = 200, Category = "Fuel"},
                            new Expense { ExpenseID = 3, Description = "Expense 3", Amount = 300, Category = "Salary"},
                        };
        }

        // GET /api/expense
        /// <summary>
        /// Gets all expenses.
        /// </summary>
        /// <returns>A list of all expenses.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await _dbContext.Expenses.ToListAsync();
        }

        // Get /api/expense/{id}
        /// <summary>
        /// Gets an expense by ID.
        /// </summary>
        /// <param name="id">The ID of the expense to retrieve.</param>
        /// <returns>The expense with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = _expenses.FirstOrDefault(e => e.ExpenseID == id);

            if (expense == null)
            {
                return NotFound();
            }

            return await Task.FromResult(expense);
        }

        // POST /api/expense
        /// <summary>
        /// Creates a new expense.
        /// </summary>
        /// <param name="expense">The expense to create.</param>
        /// <returns>The newly created expense.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(CreatedAtAction(nameof(GetExpense), new { id = expense.ExpenseID }, expense));
        }

        // PUT /api/expense/{id}
        /// <summary>
        /// Updates an existing expense.
        /// </summary>
        /// <param name="id">The ID of the expense to update.</param>
        /// <param name="expense">The updated expense.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        // DELETE /api/expense/{id}
        /// <summary>
        /// Deletes an expense by ID.
        /// </summary>
        /// <param name="id">The ID of the expense to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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