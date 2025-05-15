using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using ToDoList.Models;
using ToDoList.Interface;
using Microsoft.VisualBasic;


namespace ToDoList.Controllers
{
    
    public class MyController : Controller
    {
        //private readonly string _connectionString = "Server=DESKTOP-R02066R;Database=TodolistDB;Trusted_Connection=true;trustServerCertificate=true\r\n";
        public IActionResult Index()
        {
            return View();
        }


        private readonly IUnitOfWork _unitofwork;

        public MyController(IUnitOfWork unitofwork)
        {
            try
            {
                _unitofwork = unitofwork;
            }
            catch (Exception ex)
            {
                throw new Exception("Error initializing MyController : ", ex);
            }
        }



        [HttpGet]
        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> register(Login l)
        {
            try
            {
             bool isRegistered = await _unitofwork.meet.RegisterAsync(l);

                if (!isRegistered)
                {
                    ModelState.AddModelError("Email", "Email already exists. Please use a different one.");
                    return View();
                }
                else
                {
                    TempData["RegisterSuccess"] = "true";
                    return View();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error displaying On MyController register HttpGet Action : ", ex);
            }

        }


        [HttpGet]

        public IActionResult login()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult login(string Email,string Password)
        {
            try
            {
                var user = _unitofwork.meet.login(Email, Password);
                if(user!=null)
                {
                    HttpContext.Session.SetString("username",user.Username);
                    HttpContext.Session.SetInt32("Id", user.Id);
                    TempData["LoginSuccess"] = "true";
                    return View();
                }
                else
                {
                    ViewBag.Message = "Invalid email or password";
                    return View();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error displaying On MyController login Action : ");
            }
        }

        [HttpGet]
        public async Task< IActionResult> Display()
        {
            try
            {
                var username = HttpContext.Session.GetString("username");
                var userId = HttpContext.Session.GetInt32("Id");

                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("login", "My");
                }


                var Taskall = await _unitofwork.meet.GetAll(userId.Value);
                ViewBag.IsLogginedIn = username;


                return View(Taskall.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Error displaying On MyController Display Action : ", ex);
            }
        }




        [HttpGet]
        public IActionResult insertdata(int Id)
        {

            try
            {
                var username = HttpContext.Session.GetString("username");
                

                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("login", "My");
                }

                if(Id>0)
                { 
                Totask t;
                t = _unitofwork.meet.GetById(Id) ?? new Totask();
                    ViewBag.IsLogginedIn = username;
                return View(t);
                }
                else
                {
                    ViewBag.val = "true";
                    ViewBag.IsLogginedIn = username;
                    return View(new Totask() { DueDate = DateTime.Today });
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error displaying On MyController insertdata HttpGet Action : ", ex);
            }
        }


        [HttpPost]
        public IActionResult insertdata(Totask t)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("Id");

                if (userId == null)
                {
                    return RedirectToAction("login");
                }

                t.UserId = userId.Value;    

                if (t.Id > 0)
                {
                    _unitofwork.meet.Update(t);
                    TempData["Message"] = "Task updated successfully!";

                }
                else
                {
                    _unitofwork.meet.Add(t);
                    TempData["Message"] = "Task created successfully!";
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception("Error displaying On MyController insertdata HttpPost Action : ", ex);
            }
        }

        public IActionResult deletedata(Totask t)
        {
            try
            {
                _unitofwork.meet.Delete(t);

                return RedirectToAction("Display");
            }
            catch (Exception ex)
            {
                throw new Exception("Error displaying On MyController deletedata Action : ", ex);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }


        //[HttpGet]
        //public IActionResult insertdata(int Id)
        //{
        //    Totask T = new Totask();

        //    using(SqlConnection con = new SqlConnection(_connectionString))
        //    {
        //        string sql = "select * from Tasks where Id=@Id";
        //        T = con.QueryFirstOrDefault<Totask>(sql, new { Id });

        //        TempData["update"] = Id;
        //        ViewBag.Id = Id;
        //    }
        //    return View(T);
        //}

        //[HttpPost]
        //public IActionResult insertdata(Totask T)
        //{
        //    using (SqlConnection con = new SqlConnection(_connectionString))
        //    {
        //    if (T.Id>0)
        //        {

        //            string sql = "update Tasks set Title=@Title,Description=@Description,DueDate=@DueDate,Priority=@Priority,Status=@Status where Id=@Id";
        //            int r = con.Execute(sql, T);
        //            if (r > 0)
        //            {
        //                return RedirectToAction("Display");
        //            }
        //            else
        //            {
        //                ViewBag.Message = "<script>alert(Data Update Failed)</script>";
        //            }
        //        }
        //        else
        //        {
        //            T.Id = 0;
        //            ViewBag.Id = 0;

        //            string sql = "insert into Tasks(Title,Description,DueDate,Priority,Status) values(@Title,@Description,@DueDate,@Priority,@Status)";
        //            int r = con.Execute(sql, T);

        //            if (r > 0)
        //            {
        //                return RedirectToAction("Display");
        //            }
        //            else
        //            {
        //                ViewBag.Message = "Data Insertion Failed";
        //            }
        //        }
        //    }
        //    return View();
        //}


        //public IActionResult Display()
        //{

        //    List<Totask> taskList = new List<Totask>();

        //    using (SqlConnection con = new SqlConnection(_connectionString))
        //    {
        //           string sql = "select * from Tasks";
        //            taskList= con.Query<Totask>(sql).ToList();

        //    }

        //    return View(taskList);
        //}

    }
}
