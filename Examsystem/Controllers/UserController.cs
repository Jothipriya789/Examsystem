using Examsystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examsystem.Controllers
{
    public class UserController : Controller
    {
        OnlineExaminationEntities db = new OnlineExaminationEntities();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Candidate candidate)
        {
            if (db.Candidates.Any(x => x.Userid == candidate.Userid))
            {
                ViewBag.Notification = "this account is already existed";
                return View();
            }
            else
            {
                db.Candidates.Add(candidate);
                db.SaveChanges();
                Session["Username"] = candidate.Userid.ToString();
                Session["User_fname"] = candidate.Userfname.ToString();
                Session["User_Lname"] = candidate.Userlname.ToString();
                Session["User_phoneeno"] = candidate.phoneno.ToString();
                Session["User_email"] = candidate.email.ToString();
                Session["user_password"] = candidate.password.ToString();
                Session["confirm_password"] = candidate.confirmpassword.ToString();
                return RedirectToAction("showexam");
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Candidate candidate)
        {
            var checklogin = db.Candidates.Where(x => x.Userid.Equals(candidate.Userid) && x.password.Equals(candidate.password)).FirstOrDefault();
            if (checklogin != null)
            {
                Session["Username"] = candidate.Userid.ToString();
                TempData["uid"] = candidate.Userid;
                Session["user_password"] = candidate.password.ToString();
                return RedirectToAction("showexam");
            }
            else
            {
                ViewBag.Notification = "wrong username or password";
            }
            return RedirectToAction("showexam");
        }
        public ActionResult showexam()
        {
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }
        public ActionResult StartExam(string id)
        {

            using (var context = new OnlineExaminationEntities())
            {
                var d = (from e in db.exams where e.category_id == id && e.levelid == 1 select e).SingleOrDefault();
                var data = context.exams.FirstOrDefault(x => x.category_id == id && x.levelid == 1);
                List<Question> li = db.Questions.Where(x => x.category_id == id && x.levelid == 1).ToList();
                Queue<Question> queue = new Queue<Question>();
                foreach (Question a in li)
                {
                    queue.Enqueue(a);
                }
                TempData["questions "] = queue;
                TempData["score"] = 0;
                TempData["i"] = 1;
                TempData["id"] = id;
                TempData["pscore"] = d.passmarks;
                TempData.Keep();
                if (data != null)
                {

                    ViewBag.examid = d.exam_id;
                    ViewBag.examtext = d.exam_description;
                    ViewBag.categoryid = d.category_id;
                    ViewBag.lid = d.levelid;
                    ViewBag.duration = d.duration;
                    ViewBag.noofque = d.Noofquestion;
                    ViewBag.pm = d.passmarks;
                    ViewBag.marks = d.marks;
                    ViewBag.tmarks = d.totalmarks;
                    TempData["level"] = d.levelid;
                    TempData["examid"] = d.exam_id;
                    TempData.Keep();
                    return View(d);
                }
                else
                    return View();
            }
        }
        [HttpGet]
        public ActionResult examques(string id)
        {
            Question q = null;
            if (TempData["questions "] != null)
            {
                Queue<Question> qlist = (Queue<Question>)TempData["questions "];
                if (qlist.Count > 0)
                {
                    q = qlist.Peek();
                    qlist.Dequeue();
                    TempData["questions "] = qlist;
                    TempData.Keep();
                }
                else
                {
                    return RedirectToAction("Endexam");
                }

            }
            return View(q);

        }
        [HttpPost]
        public ActionResult examques(Question question)
        {
            string correctans = null;
            if (question.QA != null)
            {
                correctans = "A";
            }
            else if (question.QB != null)
            {
                correctans = "B";
            }
            else if (question.QC != null)
            {
                correctans = "C";

            }
            else if (question.QD != null)
            {
                correctans = "D";
            }
            if (correctans.Equals(question.Qcorrectans))
            {
                TempData["score"] = Convert.ToInt32(TempData["score"]) + 1;
            }

            TempData.Keep();
            return RedirectToAction("examques");
        }
        public ActionResult Endexam()
        {
            var level = Convert.ToInt32(TempData["level"]);
            if (level == 3)
            {
                return RedirectToAction("End");
            }
            return View();
        }
        public ActionResult level2(int id)
        {
            var p = Convert.ToInt32(TempData["pscore"]);
            if (id >= p)
            {
                return RedirectToAction("StartExam1");
            }
            else
            {
                return RedirectToAction("Home", "Index");
            }
           
        }
        public ActionResult StartExam1()
        {

            var c = TempData["id"].ToString();
            ViewBag.exam =c;
            var l = Convert.ToInt32(TempData["i"]) + 1;
            using (var context = new OnlineExaminationEntities())
            {
                var d = (from e in db.exams where string.Equals(e.category_id,c) && e.levelid == l select e).SingleOrDefault();
                var data = context.exams.FirstOrDefault(x => string.Equals(x.category_id,c) && x.levelid == l);
                List<Question> li = db.Questions.Where(x => string.Equals(x.category_id,c) && x.levelid == l).ToList();
                Queue<Question> queue = new Queue<Question>();
                foreach (Question a in li)
                {
                    queue.Enqueue(a);
                }
                TempData["questions "] = queue;
                TempData["score"] = 0;
                TempData.Keep();
                if (data != null)
                {
                    ViewBag.examid = data.exam_id;
                    ViewBag.examtext = d.exam_description;
                    ViewBag.categoryid = d.category_id;
                    ViewBag.lid = d.levelid;
                    ViewBag.duration = d.duration;
                    ViewBag.noofque = d.Noofquestion;
                    ViewBag.pm = d.passmarks;
                    ViewBag.marks = d.marks;
                    ViewBag.tmarks = d.totalmarks;
                    TempData["level"] = d.levelid;
                    TempData["pscore"] = d.passmarks;
                    TempData["examid"] = d.exam_id;
                    TempData["i"] = 2;
                    TempData.Keep();
                    return View(d);
                }
                else
                    return View();
            }
        }
        public ActionResult End()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Results ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Results(Report report)
        {
            OnlineExaminationEntities db1 = new OnlineExaminationEntities();
            var c = Convert.ToInt32(TempData["score"]);
            var u = Convert.ToInt32(TempData["uid"]).ToString();
            var e = Convert.ToInt32(TempData["examid"]).ToString();
            report.result_score = c;
            report.user_id = u;
            report.exam_id = e;
            var level = Convert.ToInt32(TempData["level"]);
            if (level == 3)
            {
                report.result_status = "excellent";
            }
            else if (level == 2)
            {
                report.result_status = "good";
            }
            else if (level == 1)
            {
                report.result_status = "better try";
            }
            if (ModelState.IsValid)
            {
                db1.Reports.Add(report);
                db1.SaveChanges();
                return View();
            }
            return View();
        }

    }
}