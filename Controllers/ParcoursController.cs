using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationFormation.Models;
using AutoMapper;

namespace WebApplicationFormation.Controllers
{
    [Authorize]
    public class ParcoursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Parcours
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            return View(await db.Parcours.ToListAsync());
        }

        // GET: Parcours/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcours parcours = await db.Parcours.FindAsync(id);
            if (parcours == null)
            {
                return HttpNotFound();
            }
            return View(parcours);
        }
        [Authorize(Roles = "Admin")]
        // GET: Parcours/Create
        public ActionResult Create()
        {
            List<Module> modules = db.Modules.ToList();
            ViewBag.IdModule = new SelectList(modules, "Id", "Resume");
            return View();
            
        }

        // POST: Parcours/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Designation,NbHeures,IdModule")] ParcoursVM parcoursVm)
        {
            if (ModelState.IsValid)
            {
                MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<ParcoursVM, Parcours>());
                // 2 : créer un Mapper
                Mapper mapper = new Mapper(config);
                // 3 : mappage
                Parcours parcours = mapper.Map<Parcours>(parcoursVm);
                Module module = db.Modules.SingleOrDefault(x => x.Id == parcoursVm.IdModule);
                parcours.Modules = new List<Module>();
                parcours.Modules.Add(module);
                
                db.Parcours.Add(parcours);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(parcoursVm);
        }
        [Authorize(Roles = "Admin")]
        // GET: Parcours/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcours parcours = await db.Parcours.FindAsync(id);
            if (parcours == null)
            {
                return HttpNotFound();
            }
            List<Module> modules = db.Modules.ToList();
            ViewBag.IdModule = new SelectList(modules, "Id", "Resume");

            ParcoursVM parcoursVm = new ParcoursVM();
            parcoursVm.Designation = parcours.Designation;
            parcoursVm.Id = parcours.Id;
            parcoursVm.NbHeures = parcours.NbHeures;
            parcoursVm.Modules = parcours.Modules;

            return View(parcoursVm);
        }

        // POST: Parcours/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NbHeures")] Parcours parcours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parcours).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(parcours);
        }

        // GET: Parcours/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcours parcours = await db.Parcours.FindAsync(id);
            if (parcours == null)
            {
                return HttpNotFound();
            }
            return View(parcours);
        }

        // POST: Parcours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Parcours parcours = await db.Parcours.FindAsync(id);
            db.Parcours.Remove(parcours);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        /*public async Task<ActionResult> AddModule(int? id)
        {

        }*/
        public async Task<ActionResult> RetraitModule(int? IdModule, int? IdParcours)
        {
            if (IdModule == null || IdParcours == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcours parcours = await db.Parcours.FindAsync(IdParcours);
            Module module = await db.Modules.FindAsync(IdModule);
            if (parcours == null || module == null)
            {
                return HttpNotFound();
            }
            if(parcours.Modules.Any(x => (x.Id == IdModule))){
                parcours.Modules.Remove(module);
            }
            await db.SaveChangesAsync();
            ParcoursVM parcoursVm = new ParcoursVM();
            parcoursVm.Designation = parcours.Designation;
            parcoursVm.Id = parcours.Id;
            parcoursVm.NbHeures = parcours.NbHeures;
            parcoursVm.Modules = parcours.Modules;
            return RedirectToAction("Edit", parcoursVm);
        }
        public async Task<ActionResult> AjoutModule(int? IdModule, int? IdParcours)
        {
            if (IdModule == null || IdParcours == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parcours parcours = await db.Parcours.FindAsync(IdParcours);
            Module module = await db.Modules.FindAsync(IdModule);
            if (parcours == null || module == null)
            {
                return HttpNotFound();
            }
            if (!parcours.Modules.Any(x => (x.Id == IdModule)))
            {
                parcours.Modules.Add(module);
            }
            await db.SaveChangesAsync();
            ParcoursVM parcoursVm = new ParcoursVM();
            parcoursVm.Designation = parcours.Designation;
            parcoursVm.Id = parcours.Id;
            parcoursVm.NbHeures = parcours.NbHeures;
            parcoursVm.Modules = parcours.Modules;
            return RedirectToAction("Edit", parcoursVm);
            // L'élément sélectionné par défaut doit être resélectionné pour être pris en compte
        }

    }
}
