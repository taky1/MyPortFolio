using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entites;
using Infrastracture;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Core.Interfaces;

namespace Web.Controllers
{
    public class ProtfolioItemsController : Controller
    {
        private readonly IUnitOfWork<ProtfolioItem> _portfolio;
        
        private readonly IHostingEnvironment _hosting;

        
        public ProtfolioItemsController(IUnitOfWork<ProtfolioItem> porfolio, IHostingEnvironment hosting)
        {
            _portfolio = porfolio;
            _hosting = hosting;
        }

        // GET: ProfolioItems
        public ActionResult Index()
        {
            return View(_portfolio.Entity.GetAll());
        }

        // GET: ProfolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profolioItem = _portfolio.Entity.GetById(id);
              
            if (profolioItem == null)
            {
                return NotFound();
            }

            return View(profolioItem);
        }

        // GET: ProfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProfolioItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(PortfolioViewModel model)
        { 
        if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
        string fullPath = Path.Combine(uploads, model.File.FileName);
        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                ProtfolioItem portfolioItem = new ProtfolioItem
    {
        ProjectName = model.ProjectName,
        Description = model.Description,
        ImageUrl = model.File.FileName
    };

    _portfolio.Entity.Insert(portfolioItem);
                _portfolio.Save();
                return RedirectToAction(nameof(Index));
}

            return View(model);
        }
        // GET: ProfolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profolioItem = _portfolio.Entity.GetById(id); 
            
                if (profolioItem == null)
            {
                return NotFound();
            }

            PortfolioViewModel portfolioViewModel = new PortfolioViewModel
            {
               Id = profolioItem.Id,
               Description = profolioItem.Description,
               ImageUrl = profolioItem.ImageUrl,
               ProjectName = profolioItem.ProjectName
            };
            return View(profolioItem);
        }

        // POST: ProfolioItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortfolioViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");

                        string fullPath = Path.Combine(uploads, model.File.FileName);

                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                    ProtfolioItem porfolioItem = new ProtfolioItem
                    {
                        Id = model.Id,

                        Description = model.Description,

                        ImageUrl = model.ImageUrl,

                        ProjectName = model.ProjectName,
                    };

                    _portfolio.Entity.Update(porfolioItem);
                    _portfolio.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfolioItemExists(model.Id))
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
            return View(model);
        }

        // GET: ProfolioItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profolioItem = _portfolio.Entity.GetById(id);
               
            if (profolioItem == null)
            {
                return NotFound();
            }

            return View(profolioItem);
        }

        // POST: ProfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _portfolio.Entity.Delete(id);
            _portfolio.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfolioItemExists(Guid id)
        {
            return _portfolio.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
