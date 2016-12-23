using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimelyGreetingsLite.Repository;
using TimelyGreetingsLite.Models;
using System.Web.Security;

namespace TimelyGreetingsLite.Controllers
{
    public class UserController : Controller
    {
        UserRepository usrRepo = new UserRepository();

        // GET: LIST ALL USERS
        public ActionResult Index()
        {            
            return View(usrRepo.GetAllUsers());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {            
            return View(usrRepo.GetAllUsers());
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ADD USER
        [HttpPost]
        public ActionResult Create(User usr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usrRepo.AddUser(usr);

                    ViewBag.Message = "Records added successfully.";

                }

                return View();
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to create user. " + ex.Message, ex.InnerException);
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View(usrRepo.GetUserByID(id));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User obj)
        {
            try
            {
                usrRepo.UpdateUser(obj);

                return RedirectToAction("Index");
            }            
            catch (Exception ex)
            {
                throw new Exception("Unable to update user. " + ex.Message, ex.InnerException);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {                
                if (usrRepo.DeleteUser(id))
                {
                    ViewBag.AlertMsg = "User details deleted successfully";

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to delete user. " + ex.Message, ex.InnerException);
            }
        }

        //// POST: User/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, User obj)
        //{
        //    try
        //    { 
        //        return RedirectToAction("Index");
        //    }            
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Unable to delete user. " + ex.Message, ex.InnerException);
        //    }
        //}


        public ActionResult Login()
        {
            return PartialView("_LoginPartial");
        }

        [HttpPost]
        public ActionResult Login(string userName, string pw)
        {
            //http://stackoverflow.com/questions/18594316/custom-authentication-and-asp-net-mvc
            try
            {
                bool isAuthenticated = false;

                Int64? userID = usrRepo.AuthenticateUser(userName, pw);

                if (userID != null && userID > 0)
                {
                    // USER IS SUCCESSFULLY AUTHENTICATED
                    isAuthenticated = true;
                }
                else
                {
                    ViewBag.AuthenticationError = "Login failed.";
                }               

                if (isAuthenticated)
                {
                    string userInfo = userName + "|" + userID.Value.ToString();
                    FormsAuthentication.SetAuthCookie(userInfo, false);
                    ModelState.Clear();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //return RedirectToAction("Login", "User");
                    ModelState.Clear();
                    return PartialView("_LoginPartial");
                }

               
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Login user. " + ex.Message, ex.InnerException);
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
