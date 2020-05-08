using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SqrtCalculator.Models;

namespace SqrtCalculator.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string firstNumber, string secondNumber)
        {
          // Check if values are numbers and if false set error
          decimal number = 0;
          bool canConvertFirst = decimal.TryParse(firstNumber, out number);
          bool canConvertSecond = decimal.TryParse(secondNumber, out number);
          if (!canConvertFirst || !canConvertSecond) 
          {
            ViewBag.Error = "not string";
            return View();
          }

          // Collect values and convert to ints
          decimal firstNum = decimal.Parse(firstNumber);
          decimal secondNum = decimal.Parse(secondNumber);
      
          // Check if values are less than zero and set error if true
          if (firstNum < 0 || secondNum < 0) 
          {
            ViewBag.Error = "negative number";
            return View();
          }

          // Get square root of both numbers
          decimal firstNumRoot = (decimal)Math.Sqrt((double)firstNum);
          decimal secondNumRoot = (decimal)Math.Sqrt((double)secondNum);

          // Check if square roots are equal and if true return error
          if (firstNumRoot == secondNumRoot)
          {
            ViewBag.Error = "equal root";
            return View();
          }

          // Else return the view with neccessary values
          if (firstNumRoot > secondNumRoot)
          {
            ViewBag.HigherNum = firstNum;
            ViewBag.HigherRoot = Math.Round((double)firstNumRoot, 2);
            ViewBag.LowerNum = secondNum;
            ViewBag.LowerRoot = Math.Round((double)secondNumRoot, 2);
            return View();
          }
          else 
          {
            ViewBag.HigherNum = secondNum;
            ViewBag.HigherRoot = Math.Round((double)secondNumRoot, 2);
            ViewBag.LowerNum = firstNum;
            ViewBag.LowerRoot = Math.Round((double)firstNumRoot, 2);
            return View();
          }
          
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
