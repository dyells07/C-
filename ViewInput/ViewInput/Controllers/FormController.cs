using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ViewInput.Models;

namespace ViewInput.Controllers
{
    public class FormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        private List<Dictionary<string, string>> _userList = new List<Dictionary<string, string>>();

        [HttpPost]
        public IActionResult Display(string name, string address)
        {
            Dictionary<string, string> user = new Dictionary<string, string>
        {
            { "Name", name },
            { "Address", address }
        };
            _userList.Add(user);

            ViewBag.UserList = _userList;
           return View();
        }
    //    private List<Dictionary<string, string>> _userList = new List<Dictionary<string, string>>();

    //    [HttpPost]
    //    public IActionResult Collection(string name, string address)
    //    {
    //        Dictionary<string, string> user = new Dictionary<string, string>
    //{
    //    { "Name", name },
    //    { "Address", address }
    //};
    //        _userList.Add(user);

    //        return RedirectToAction("Display");
    //    }

        //public IActionResult Display()
        //{
        //    ViewBag.UserList = _userList;
        //    return View();
        //}

        //public IActionResult Display(UserModel userInput)
        //{
        //    return View(userInput);
        //}
    }
}
