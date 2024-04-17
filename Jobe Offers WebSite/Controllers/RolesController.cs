﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.Entity;

namespace Jobe_Offers_WebSite.Controllers
{

    [Authorize(Roles = "Admins")]

    public class RolesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.Find(id);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    db.Roles.Add(role);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(role);
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.Find(id);

            if (role == null)
            {
                HttpNotFound();
            }

            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id, Name")] IdentityRole role)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    // System.Data.Entity.EntityState.Modified;
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(role);
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.Roles.Find(id);

            if (role == null)
            {
                HttpNotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole myrole)
        {
            try
            {
                // TODO: Add delete logic here

                IdentityRole role = db.Roles.Find(myrole.Id);
                db.Roles.Remove(role);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
