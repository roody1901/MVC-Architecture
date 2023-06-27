using Microsoft.AspNetCore.Mvc;
using BankingSystem.Models;
namespace BankingSystem.Controllers
{
    public class AdminController : Controller
    {
        private IAdminRepository _adminRepository;
        public AdminController (IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public ViewResult AddNewAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewAdmin(Admin admins)
        {           
            
            try
            {  
                if(ModelState.IsValid)
                {
                    if (admins.Id == 0 && admins.Passwords == 0)
                    {
                        ViewBag.Messgae = "Please Enter some values";
                    }

                    else
                    {
                        bool verification = _adminRepository.CheckIdAdmin(admins.Id);

                        if (verification == true)
                        {
                            ViewBag.Message = "Id Already Registered";
                        }
                        else if (admins != null)
                        {
                            if (_adminRepository.AddNewAdmin(admins))
                            {
                                ViewBag.Message = "Admin Created";
                                //return RedirectToAction("CheckLogin");

                            }
                        }

                    }

                }
                   
                
                return View();
            }
                
            catch (Exception ex)
            {
                    return View(ex.Message);
            }
            
       
        }

        public ViewResult CheckLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckLogin(Admin admins)
        {
           bool ans =  _adminRepository.CheckLogin(admins);
            if(admins.Id == 0 || admins.Passwords == 0)
            {
                ViewBag.Message = "Please fill all details";
            }
            else
            {
                if (ans == true)
                {
                    ViewBag.Message = "Login Success";
                    return RedirectToAction("GetAll", "Customer");
                }
                else
                {
                    ViewBag.Message = "Wrong Credentials!!";

                }
            }
          
         
            return View();
        }
    }
}
