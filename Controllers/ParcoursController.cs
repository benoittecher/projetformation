﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationFormation.Models;

namespace WebApplicationFormation.Controllers
{
    public class ParcoursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Parcours
        public async Task<ActionResult> Index()
        {
            return View(await db.Parcours.ToListAsync());
        }

        // GET: Parcours/Details/5
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

        // GET: Parcours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parcours/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NbHeures")] Parcours parcours)
        {
            if (ModelState.IsValid)
            {
                db.Parcours.Add(parcours);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(parcours);
        }

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
            return View(parcours);
        }

        // POST: Parcours/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
    }
}