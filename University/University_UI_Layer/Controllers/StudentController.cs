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
    public class StudentsController : Controller
    {
        private UniversityDatabaseEntities db = new UniversityDatabaseEntities();

        // GET: Student
        public ActionResult Index()
        {
            ViewBag.FullName = new SelectList(db.Students, "SSID", "FullName");
            return View(db.Students.ToList());
        }

        // GET: Student/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //GET: Student/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        //POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,Major,Address,City,State,Zipcode,Phone,Email,PhotoURL,SSID")] Student student, HttpPostedFileBase photoUrl)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                //default value for the image path - noImage.png
                string imageName = "studentImageDefault.jpg";

                //Check the input from the form and see if it is not null
                if (photoUrl != null)
                {
                    //get the fle name and save to a variable 
                    imageName = photoUrl.FileName;

                    //get extension or file type from the uploaded file
                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    //assemble a list of acceptable file types
                    string[] goodExts = new string[] { ".jpeg", ".jpg", ".png", ".gif" };

                    //Then compare the file type against the list of acceptable file types
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        //If valid file type, rename the image to a GUID - to make sure each  file name that we create is unique
                        imageName = Guid.NewGuid() + ext;

                        //Save the file to the web server
                        photoUrl.SaveAs(Server.MapPath("~/Content/studentImages/" + imageName));
                    }
                    else
                    {
                        //This is for when the file is not the right ext
                        imageName = "studentImageDefault.jpg";
                    }

                }
                //Save to the database no matter whether the file is noImage or it is valid and we have an image to save
                student.PhotoUrl = imageName;
                #endregion

                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
                return View(student);
            }
        }

        //GET: Student/Edit
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View(student);
        }

        //POST: Student/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,Major,Address,City,State,Zipcode,Phone,Email,PhotoURL,SSID")] Student student, HttpPostedFileBase photoUrl)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                //check file upload to see if the user has chosen a new file
                if (photoUrl != null)
                {
                    //get file name and assign to a variable
                    string imageName = photoUrl.FileName;

                    //find the extension
                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    //declare a collection of good exts
                    string[] goodExts = new string[] { ".jpeg", ".jpg", ".png", ".gif" };

                    //check the variable against the food exts - if good rename and save the file
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        //Rename the file
                        imageName = Guid.NewGuid() + ext;

                        //save it to the web server
                        photoUrl.SaveAs(Server.MapPath("~/Content/studentImages/" + imageName));

                        //Delete the old file - ensure we are not deleting the noImage.png
                        //More functionality to come in MVC3
                        //string currentFile = Request.Params["PhotoURL"];
                        //if (currentFile != "noImage.png" && currentFile != null)
                        //{
                        //    //delete the previously associated image from the web server
                        //    System.IO.File.Delete(Server.MapPath("~/Content/studentImages/" + currentFile));
                        //}
                    }//only if the image meets all of the criteria - send the image name to the database 
                    student.PhotoUrl = imageName;
                }
                //if the image does not meet our criteria OR no file was included, the HiddenFor() in the Edit View will
                //maintain the current image with no manual interaction from the user.
                #endregion

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
                return View(student);
            }

        }

        //GET: Student/Delete
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        //POST: Student/Delete
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Student student = db.Students.Find(id);
            if (student.SSID == 2)
            {
                student.SSID = 3;
            }
            else
            {
                student.SSID = 2;
            }
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