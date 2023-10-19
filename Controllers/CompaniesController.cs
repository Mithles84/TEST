using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TEST.Data;
using TEST.Models;

namespace TEST.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationContext _context;

        public CompaniesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Companys.Include(c => c.Employee);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Companys == null)
            {
                return NotFound();
            }

            var company = await _context.Companys
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.IdCompany == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompany,CompanyName,CompanyId")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee", company.CompanyId);
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Companys == null)
            {
                return NotFound();
            }

            var company = await _context.Companys.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee", company.CompanyId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompany,CompanyName,CompanyId")] Company company)
        {
            if (id != company.IdCompany)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.IdCompany))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee", company.CompanyId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Companys == null)
            {
                return NotFound();
            }

            var company = await _context.Companys
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.IdCompany == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Companys == null)
            {
                return Problem("Entity set 'ApplicationContext.Companys'  is null.");
            }
            var company = await _context.Companys.FindAsync(id);
            if (company != null)
            {
                _context.Companys.Remove(company);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return (_context.Companys?.Any(e => e.IdCompany == id)).GetValueOrDefault();
        }
    }
}
