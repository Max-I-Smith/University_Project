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
    public class EnrollmentController : Controller
    {
        private UniversityDatabaseEntities db = new UniversityDatabaseEntities();

        // GET: Enrollment
        public ActionResult Index()
        {
            return View(db.Enrollments.ToList());
        }

        //GET: Enrollment/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ScheduledClassId = new SelectList(db.ScheduledClasses, "ScheduledClassId", "CourseId");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName");
            return View();
        }

        //POST: Enrollment/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentId,StudentId,ScheduledClassId,EnrollmentDate")] Enrollment Enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(Enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ScheduledClassId = new SelectList(db.ScheduledClasses, "ScheduledClassId", "CourseId", Enrollment.ScheduledClassId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", Enrollment.StudentId);
            return View(Enrollment);
        }

        //GET: Enrollment/Edit
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment Enrollment = db.Enrollments.Find(id);
            if (Enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ScheduledClassId = new SelectList(db.ScheduledClasses, "ScheduledClassId", "CourseId", Enrollment.ScheduledClassId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", Enrollment.StudentId);
            return View(Enrollment);
        }

        //POST: Enrollment/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentId,StudentId,ScheduledClassId,EnrollmentDate")] Enrollment Enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");


            }

            ViewBag.ScheduledClassId = new SelectList(db.ScheduledClasses, "ScheduledClassId", "CourseId", Enrollment.ScheduledClassId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", Enrollment.StudentId);
            return View(Enrollment);
        }

        //GET: Enrollment/Delete
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment Enrollment = db.Enrollments.Find(id);
            if (Enrollment == null)
            {
                return HttpNotFound();
            }
            return View(Enrollment);
        }

        //POST: Enrollment/Delete
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment Enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(Enrollment);
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