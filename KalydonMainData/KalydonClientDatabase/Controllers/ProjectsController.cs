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
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KalydonClientDatabase.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Project.OrderBy(p => p.StartDate);
            return View(await applicationDbContext.ToListAsync());
        }
        //GET: Projects/FindProjects
        public IActionResult FindProjects()
        {
            return View();
        }
        public async Task<IActionResult> FindProjectByName(string name)
        {
            return View("Index", await _context.Project.Where(p => p.Name.Contains(name)).ToListAsync());
        }

        //GET: Projects
        /*public async Task<IActionResult> ShowProjectsByClientId(int id)
        {
            var applicationDbContext = _context.Project.Include(p => p.Client);
            return View("Index",await applicationDbContext.Where(p => p.ClientId == id).ToListAsync());
        }*/

        public IActionResult ShowProjectTasks(int id) 
        {
            return RedirectToAction("ShowProjectTasksByProjectId", "ProjectTasks", new { id });
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                //.Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            
            //ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name");
            
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name","StartDate","EndDate")]Project project)
        {
            //project.Client = _context.Client.Find(project.ClientId);
            
            ModelState.Clear();
            TryValidateModel(project);
            
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            //ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", project.ClientId);

            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            //ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", project.ClientId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }
            
            //project.Client = _context.Client.Find(project.ClientId);
          
            ModelState.Clear();
            TryValidateModel(project);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            //ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", project.ClientId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
