#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KalydonClientDatabase.Data;
using KalydonClientDatabase.Models;

namespace KalydonClientDatabase.Controllers
{
    public class ProjectTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectTasks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProjectTask.Include(p => p.Project).OrderBy(t => t.CompletionTime);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectTasks
        public async Task<IActionResult> ShowProjectTasksByProjectId(int id)
        {
            var applicationDbContext = _context.ProjectTask.Include(p => p.Project).OrderBy(t => t.CompletionTime);
            return View("Index",await applicationDbContext.Where(pt => pt.ProjectId == id).ToListAsync());
        }

        // GET: ProjectTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTask
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTasks/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name");
            return View();
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CompletionTime,IsCompleted,ProjectId")] ProjectTask projectTask)
        {
            projectTask.Project = _context.Project.Find(projectTask.ProjectId);
            //projectTask.Project.Client = _context.Client.Find(projectTask.Project.ClientId);
            
            ModelState.Clear();
            TryValidateModel(projectTask);
            if (ModelState.IsValid)
            {
                _context.Add(projectTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name", projectTask.ProjectId);
            return View(projectTask);
        }

        // GET: ProjectTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var projectTask = await _context.ProjectTask.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name", projectTask.ProjectId);
            return View(projectTask);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CompletionTime,IsCompleted,ProjectId")] ProjectTask projectTask)
        {
            if (id != projectTask.Id)
            {
                return NotFound();
            }

            projectTask.Project = _context.Project.Find(projectTask.ProjectId);
            //projectTask.Project.Client = _context.Client.Find(projectTask.Project.ClientId);
            ModelState.Clear();
            TryValidateModel(projectTask);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTaskExists(projectTask.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Name", projectTask.ProjectId);
            return View(projectTask);
        }

        // GET: ProjectTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTask
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectTask = await _context.ProjectTask.FindAsync(id);
            _context.ProjectTask.Remove(projectTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTask.Any(e => e.Id == id);
        }
        
        //GET: ProjectTasks/ShowTasksSearch
        public IActionResult ShowTasksSearch() 
        {
            return View();
        }

        //POST: ProjectTasks/ShowTasksDateRange
        [HttpPost]
        public async Task<IActionResult> ShowTasksDateRange(DateTime minDate, DateTime maxDate) 
        {
            return View("Index", await _context.ProjectTask.Where(task => task.CompletionTime >= minDate && task.CompletionTime <= maxDate)
                .OrderBy(t => t.CompletionTime)
                .Include(p => p.Project)
                .ToListAsync());
        }
    }
}
