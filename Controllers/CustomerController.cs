using Microsoft.AspNetCore.Mvc;
using BankingSystem.Models;
namespace BankingSystem.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

      
        //GET ALL 

       //FOR OPENING GETALL AS DEFAULT PAGE 'NO NEED TO ENTER URL'
        public ActionResult GetAll()
        {

            try
            {
                return View(_customerRepository.GetAll());
            }
            catch (Exception ex)
            {
                return ViewBag.Message = ex.Message;
            }
         
        }
        [HttpPost]
        public void GetAll(string GetBy)
        {
            if(GetBy !=null)
            {
                int ids = Convert.ToInt32(GetBy);
                Response.Redirect("GetCustomerById/" + ids);
            }   
        }
     
        public ActionResult GetCustomerById(int id)
        {
            try
            {
                return View(_customerRepository.GetAll().Find(cust => cust.Id == id));
            }
            catch(Exception ex)
            {
                return ViewBag.Message = ex.Message;
            }

            // return View(_customerRepository.GetCustomerById(1001));        

        }

    

        //public void GetCustomer(int ids)
        //{
        //   ActionResult ans = GetCustomerById(ids);
        //}
        
        //ADD EMPLOYEE 
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer cust)
        {
            try
            {              
                if (ModelState.IsValid)
                {
                    int bal = Convert.ToInt32(cust.Balance);
                    bool verify = _customerRepository.CheckIdCust(cust);
                    bool verify1 = _customerRepository.CheckAcnt(cust);
                    if (verify == true)
                    {
                        ViewBag.Message = "Duplicate Id!!";
                        ModelState.Clear();
                        return View();
                    }
                    else if(verify1 == true)
                    {
                        ViewBag.Message = "Duplicate Account Number!!";
                        ModelState.Clear();
                        return View();

                    }
                    else if(verify == true && verify1 == true)
                    {
                        ViewBag.Message = "Duplicate Id and Account Number!!";
                    }
                    
                   else if (cust != null && bal >= 100)
                    {
                        if (_customerRepository.AddCustomer(cust) )
                        {
                            ViewBag.Message = "Added Succesfully";
                           ModelState.Clear();           //TO CLEAR ALL DATA AFTER CREATING USER
                         //   return RedirectToAction("GetAll");
                        }

                    }
                    else
                    {
                        ViewBag.Bal = "Account Opening balance should be more than 100Rs.";
                    }

                }
                return View();
            }
            catch (Exception ex)
            {
                return ViewBag.Message = ex.Message;
            }
        }



        //UPDATE EMPLOYEE
        public ActionResult Edit(int id)
        {

            return View(_customerRepository.GetAll().Find(cust => cust.Id == id));
        }


        [HttpPost]
        public ActionResult Edit(int id, Customer cust)
        {
            try
            {
               if(cust != null)
                {
                    if (_customerRepository.UpdateCustomer(cust))
                    {                   
                        return RedirectToAction("GetAll");
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                return ViewBag.Message=ex.Message;
            }
        }



        //DELETE EMPLOYEE
        public ActionResult Delete(int id)
        {

            return View(_customerRepository.GetAll().Find(cust => cust.Id == id));
        }


        [HttpPost]
        public ActionResult Delete(int id, Customer cust)
        {
            try
            {
                if (_customerRepository.DeleteCustomer(id))
                {
                    ViewBag.AlertMsg = "Employee details deleted successfully";

                }
                return RedirectToAction("GetAll");
            }
            catch
            {
                return View();
            }
        }

        public ViewResult CheckCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckCustomer(Customer cust)
        {
            bool ans = _customerRepository.CheckCustomer(cust);
            
           
            if (ans == true)
            {                //ViewBag.Message =cust.Id;
                //  return Response.Redirect("custo/cust.Id");
                Response.Redirect("WithDrawal/"+cust.Id);

            }
            else if (cust.Id != 0 && cust.AccountNumber !=0)
            {
                ViewBag.Message = "Wrong Credentials!!";
            }

            return View();
        }

        public ViewResult WithDrawal(int id)
        {
            return View(_customerRepository.GetAll().Find(cust => cust.Id == id));
        }

        [HttpPost]

        public IActionResult WithDrawal(Customer cust,string amt)
        {
            try
            {              
                
                var data = Convert.ToInt32(cust.Balance);
                var amtn = Convert.ToInt32(amt);
                if (amtn > 0 && amtn <= data)
                {
                    var up = (data - amtn);
                    cust.Balance = Convert.ToString(up);

                    if (_customerRepository.WithDrawal(cust))
                    {
                        //  Response.Redirect("Customer/GetCusto/"+cust.Id);
                        ViewBag.Success = "Transaction Successfull";
                        ViewBag.Id = "Your Customer Id:-  " + cust.Id;
                        ViewBag.Balance = "Your Available Balance:-  " + cust.Balance;
                        ViewBag.Message = "Thank you";



                    }
                }
                else if(amtn <= 0)
                {
                    ViewBag.Message = "Amount Should be Greater Than Zero";

                }
                else
                {
                    ViewBag.Message = "Insufficient Balance";
                    
                }
                return View();
               
            }
            catch
            {
                return View();
            }
        }
    }
}
