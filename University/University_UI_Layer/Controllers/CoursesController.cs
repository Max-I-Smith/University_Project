using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using University_DATA_EF;

namespace University_UI_Layer.Controllers
{
    public class CoursesController : Controller
    {
        private UniversityDatabaseEntities db = new UniversityDatabaseEntities();

        // GET: Course
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        //GET: Course/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //POST: Course/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="CourseID,CourseName,CourseDescription,CreditHours,Curriculum,Notes,IsActive")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        //GET: Course/Edit
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course Course = db.Courses.Find(id);
            if (Course == null)
            {
                return HttpNotFound();
            }
            return View(Course);
        }

        //POST: Course/Edit
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,CourseName,CourseDescription,CreditHours,Curriculum,Notes,IsActive")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");


            }

            return View(course);
        }

        //GET: Course/Delete
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course Course = db.Courses.Find(id);
            if (Course == null)
            {
                return HttpNotFound();
            }
            return View(Course);
        }

        //POST: Course/Delete
        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course Course = db.Courses.Find(id);
            db.Courses.Remove(Course);
            db.SaveChanges();
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