using Examsystem.Models;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Examsystem.Controllers
{
    public class AdminController : Controller
    {
        OnlineExaminationEntities db = new OnlineExaminationEntities();
     
       
        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult Logoutadmin()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(admin admin)
        {
            var checklogin = db.admins.Where(x => x.admin_id.Equals(admin.admin_id) && x.admin_password.Equals(admin.admin_password)).FirstOrDefault();
            if (checklogin != null)
            {
                Session["Admin_id"] = admin.admin_id.ToString();
                Session["Admin"] = checklogin.admin_name.ToString();
                Session["Admin_password"] = admin.admin_password.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "wrong username or password";
            }
            return View();
        }
        
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Category category)
        {
            if (db.Categories.Any(x => x.category_id == category.category_id))
            {
                ViewBag.Notification = "This subject is already existed";
                return View();
            }
            else if (db.Categories.Any(x => x.category_name == category.category_name))
            {
                ViewBag.Notification = "This subject is already existed";
                return View();
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Categories.Add(category);
                        db.SaveChanges();
                        return View();
                    }
                }
                catch
                {
                    ViewBag.Notification = "This subject is already existed";
                    return View();
                }

           }
            return View();
        }


        public ActionResult Getdetailsstudents()
        {
            List<Candidate> candidates = db.Candidates.ToList();
            ViewBag.TotalStudents = candidates.Count();
            return View(candidates);
        }
        public ActionResult Index()
        {
            return View();
        }
        public FileResult DownloadExcel()
        {
            string path = "/excelfolder/Questions.xlsx";
            return File(path, "application/vnd.ms-excel", "Questionss.xlsx");
        }

        [HttpPost]
        public JsonResult UploadExcel(Question question, HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {

                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/excelfolder/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];
                    string sheetName = "Sheet1";
                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<Question>(sheetName) select a;
                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.Q_text != "" && a.QA != "")
                            {
                                Question TU = new Question();
                                TU.Qid = a.Qid;
                                TU.Q_text = a.Q_text;
                                TU.QA = a.QA;
                                TU.QB = a.QB;
                                TU.QC = a.QC;
                                TU.QD = a.QD;
                                TU.Qcorrectans = a.Qcorrectans;
                                TU.category_id = a.category_id;
                                TU.levelid = a.levelid;
                                db.Questions.Add(TU);
                                db.SaveChanges();
                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a.Q_text == "" || a.Q_text == null) data.Add("<li> Text is required</li>");
                                if (a.QA == "" || a.QA == null) data.Add("<li> option 1 is required</li>");
                                if (a.QB == "" || a.QB == null) data.Add("<li> option 2 is required</li>");
                                if (a.QC == "" || a.QC == null) data.Add("<li> option 3 is required</li>");
                                if (a.QD == "" || a.QD == null) data.Add("<li> option 4 is required</li>");
                                if (a.Qcorrectans == "" || a.Qcorrectans == null) data.Add("<li>Correct answer is required</li>");
                                if (a.category_id == null) data.Add("<li> category id is required</li>");
                                if (a.levelid == null) data.Add("<li>level id is required</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }
                    }

                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {

                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Getdetailsques()
        {
            List<Question> questions = db.Questions.ToList();
            return View(questions);
        }
        public ActionResult Delete(int id)
        {
            using (var context = new OnlineExaminationEntities())
            {
                var data = context.Questions.FirstOrDefault(x => x.Qid == id);
                if (data != null)
                {
                    context.Questions.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Getdetailscat");
                }
                else
                    return View();
            }
        }
        public ActionResult Addexam()
        {
            var catlist = db.Categories.ToList();
            ViewBag.Catlist = new SelectList(catlist, "category_id", "category_name");
            return View();
        }
        [HttpPost]
        public ActionResult Addexam(exam exam)
        {
            int i = 3;
            var catlist = db.Categories.ToList();
            ViewBag.Catlist = new SelectList(catlist, "category_id", "category_name");
            if (db.exams.Any(x => x.exam_id == exam.exam_id))
            {
                ViewBag.Notification1 = "This exam id is already existed";
                return View();
            }
          
            else if (db.exams.Any(x => x.category_id.Equals(exam.category_id) && x.levelid.Equals(exam.levelid)))
            {
                ViewBag.Notification1 = "This exam is already existed ";
                return View();
            }
            try
            {
             if (ModelState.IsValid)
                {

                    db.exams.Add(exam);
                    db.SaveChanges();
                    return View();
                }
            }
            catch
            {
                ViewBag.Notification1 = "This exam is already existed ";
                return View();
            }
            return View();
        }
        public ActionResult GetdetailsExams()
        {
            List<exam> exams = db.exams.ToList();
            return View(exams);
        }
        public ActionResult Edit(string id)
        {
            var catlist = db.Categories.ToList();
            ViewBag.Catlist = new SelectList(catlist, "category_id", "category_name");
            var cat = db.exams.Where(s => s.exam_id == id).SingleOrDefault();
            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(exam exam,string id)
        {
            var catlist = db.Categories.ToList();
            ViewBag.Catlist = new SelectList(catlist, "category_id", "category_name");
            var cat = db.exams.FirstOrDefault(s => s.exam_id ==id);
            if (cat != null)
            {
                cat.exam_description = exam.exam_description;
                cat.category_id = exam.category_id;
                cat.levelid = exam.levelid;
                cat.duration = exam.duration;
                cat.Noofquestion = exam.Noofquestion;
                cat.marks = exam.marks;
                cat.passmarks = exam.passmarks;
                cat.totalmarks = exam.totalmarks;
                db.SaveChanges();
                return RedirectToAction("GetdetailsExams");
            }
            else
                return View();

            
            
        }
        public ActionResult Deleteexam(string id)
        {
            using (var context = new OnlineExaminationEntities())
            {
                var data = context.exams.FirstOrDefault(x => x.exam_id == id);
                if (data != null)
                {
                    context.exams.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("GetdetailsExams");
                }
                else
                    return View();
            }
        }
        public ActionResult Results(string id)
        {
            var d = (from e in db.Reports where string.Equals(e.user_id, id) select e).SingleOrDefault();
            var data = db.Reports.FirstOrDefault(x => string.Equals(x.user_id, id));
            if (data != null)
            {
                ViewBag.reportid = d.result_id;
                ViewBag.score = d.result_score;
                ViewBag.status = d.result_status;
                ViewBag.uid = d.user_id;
                ViewBag.eid = d.exam_id;
                return View(d);
            }
            else
            {
                return View();
            }
        }
    }
}