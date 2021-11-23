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
            else if (db.Candidates.Any(x => x.email == candidate.email))
            {
                ViewBag.Notification = "this email  is already existed";
                return View();
            }
            else
            {
                try
                {
                    db.Candidates.Add(candidate);
                    db.SaveChanges();
                    Session["Username"] = candidate.Userid.ToString();
                    Session["Userfname"] = candidate.Userfname.ToString();
                    return RedirectToAction("showexam");
                }
                catch
                {
                    ViewBag.Notification = "this account is already existed";
                    return View();
                }
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
                Session["Userfname"] = checklogin.Userfname.ToString();
                TempData["uid"] = candidate.Userid;
                TempData["uf"] = checklogin.Userfname;
                TempData["lf"] = checklogin.Userlname;
                TempData["email"] = checklogin.email;
                TempData.Keep();
                return RedirectToAction("showexam");
            }
            else
            {
                ViewBag.Notification = "wrong username or password";
                return View();
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
                TempData["sno"] = 0;
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
                    TempData["pscore"] = d.passmarks;
                    TempData["des"] = d.exam_description;
                    TempData.Keep();

                    return View(d);
                }
                else
                {
                    ViewBag.not = "No exam found";
                    return View();
                }
                return View();
            }
        }
        [HttpGet]
        public ActionResult examques(string id)
        {
            var i = Convert.ToInt32(TempData["sno"]);
            Question q = null;
            if (TempData["questions "] != null)
            {
                Queue<Question> qlist = (Queue<Question>)TempData["questions "];
                if (qlist.Count > 0)
                {
                    TempData["sno"] = i+1;
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
            try
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
                else
                {
                    correctans = "e";
                }

                if (correctans.Equals(question.Qcorrectans))
                {
                    TempData["score"] = Convert.ToInt32(TempData["score"]) + 1;
                }
               
                TempData.Keep();
                return RedirectToAction("examques");
            }
            catch(Exception ex)
            {
                ViewBag.not = ex;
            }
            return View();
        }
        public ActionResult Endexam()
        {
            var id = Convert.ToInt32(TempData["score"]);
            var p = Convert.ToInt32(TempData["pscore"]);
            var level = Convert.ToInt32(TempData["level"]);
            TempData["score"] = id;
            TempData.Keep();
            if (level == 3)
            {
                return RedirectToAction("End");
            }
            if (id >= p)
            {
                ViewBag.msg = "pass";
                ViewBag.msg1 = "congratulations on completed this level ";
                return View();
            }
            else
            {
                ViewBag.msg1 = "sorry! better luck next time ";
                ViewBag.msg = "fail";
                return View();
            }
           
            
            return View();
        }
        public ActionResult level2(int id)
        {
            TempData["e"] = id;
            TempData.Keep();
            var p = Convert.ToInt32(TempData["pscore"]);
            if (id >= p)
            {
                return RedirectToAction("StartExam1");
            }
            else
            {
                return RedirectToAction("showexam");
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
                    TempData["des"] = d.exam_description;
                    TempData["i"] = 2;
                    TempData.Keep();
                    return View(d);
                }
                else
                {
                    ViewBag.not = "No exam found";
                    return View();
                }
            }
        }
        public ActionResult End()
        {
            var id = Convert.ToInt32(TempData["e"]);
            var p = Convert.ToInt32(TempData["pscore"]);
            if (id >= p)
            {

                ViewBag.msg1 = "congratulations on completed 3 levels";
                ViewBag.msg = "pass";

                return View();
            }
            else
            {
                ViewBag.msg = "fail";
                ViewBag.msg1 = "sorry! better luck next time ";
              
                return View();
            }

            return View();
        }
        [HttpGet]
        public ActionResult Results()
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
            ViewBag.rs = report.result_score;
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
            try
            {
                if (ModelState.IsValid)
                {
                    db1.Reports.Add(report);
                    db1.SaveChanges();
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.Notification = ex;
                return View();
            }
            return View();
        }

    }
}