using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MasciApps_Ass2.Models;

namespace MasciApps_Ass2.Controllers
{
    public class MenuController : Controller
    {
        private MenuEntities db = new MenuEntities();

        // GET: Menu
        public async Task<ActionResult> Index()
        {
            List<Item> items = db.Items.ToList(); 
            return View(items);
        }

        // GET: Menu/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Menu/Create
        public ActionResult Create()
        {
            ViewBag.ItemLabelId = new SelectList(db.ItemLabels, "ItemLabelId", "Name");
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "ItemTypeId", "Name");
            return View();
        }

        // POST: Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ItemId,ItemTypeId,ItemLabelId,Name,Price,ImageUrl,ShortDescription,LongDescription")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ItemLabelId = new SelectList(db.ItemLabels, "ItemLabelId", "Name", item.ItemLabelId);
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "ItemTypeId", "Name", item.ItemTypeId);
            return View(item);
        }

        // GET: Menu/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemLabelId = new SelectList(db.ItemLabels, "ItemLabelId", "Name", item.ItemLabelId);
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "ItemTypeId", "Name", item.ItemTypeId);
            return View(item);
        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ItemId,ItemTypeId,ItemLabelId,Name,Price,ImageUrl,ShortDescription,LongDescription")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ItemLabelId = new SelectList(db.ItemLabels, "ItemLabelId", "Name", item.ItemLabelId);
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "ItemTypeId", "Name", item.ItemTypeId);
            return View(item);
        }

        // GET: Menu/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Item item = await db.Items.FindAsync(id);
            db.Items.Remove(item);
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
