using Jobe_Offers_WebSite.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);

            if(job == null)
            {
                return HttpNotFound();
            }

            Session["JobId"] = JobId;

            return View(job);
        }

        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];

            // Pour Verifer que ce n'est pas fait duex fois la meme job . 
            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();

            if(check.Count < 1)
            {
                var job = new ApplyForJob();

                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;

                db.ApplyForJobs.Add(job);

                db.SaveChanges();

                ViewBag.Result = "تمة الإضافة بنجاح";

            }
            else
            {
                // Passe un erreur Action to View . 
                ViewBag.Result = "المعذرة، لقد سبق وتقدمت بنفس الوظيفة ";
            }

            return View();
        }

        // Sette methode Permet de Retunrer Tous Les Message Dans une Listes . 
        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(a => a.UserId == UserId).ToList();

            return View(jobs);
        }

        // Get
        public ActionResult Edit(int id)
        {
            ApplyForJob app = db.ApplyForJobs.Find(id);
            if (app == null)
            {
                return HttpNotFound();
            }
            return View(app);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplyForJob app)
        {
            if (ModelState.IsValid)
            {

                app.ApplyDate = DateTime.Now;

                db.Entry(app).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(app);
        }

        // GET: /Delete/5
        public ActionResult Delete(int id)
        {
            ApplyForJob app = db.ApplyForJobs.Find(id);
            if (app == null)
            {
                return HttpNotFound();
            }
            return View(app);
        }

        // POST: /Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ApplyForJob appl)
        {
            ApplyForJob app = db.ApplyForJobs.Find(appl.Id);
            db.ApplyForJobs.Remove(app);
            db.SaveChanges();
            return RedirectToAction("GetJobsByUser");
        }

        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);

            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }

        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();

            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserID
                       select app;

            var grouped = from j in Jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());
        }


        // Contacter le site . 
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("fadiba318@gmail.com", "Ff123456");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("fadiba318@gmail.com"));
            mail.Subject = contact.Subject;

            mail.IsBodyHtml = true;

            string body = "إسم المرسل:" + contact.Name + "<br>"+
                            "بريد المرسل :" + contact.Email + "<br>" +
                            "عنوان الريالة :" + contact.Subject + "<br>" +
                            "نص الرسالة : <b>" + contact.Message + "</b>";

            mail.Body = body;

            // envoyer message sur email . Host
            var smtpClient = new SmtpClient("smtp.gmail.com", 25);
            //smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            
            //smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);

            return RedirectToAction("Index");
        }


        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(
                                    a => a.JobTitle.Contains(searchName)
                                    || a.JobContent.Contains(searchName)
                                    || a.Category.CategoryName.Contains(searchName)
                                    || a.Category.CategoryDescription.Contains(searchName)
                                    ).ToList();
            return View(result);
        }

    }
}