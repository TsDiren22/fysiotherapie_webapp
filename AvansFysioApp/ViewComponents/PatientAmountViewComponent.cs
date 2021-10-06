using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioApp.Models;
using AvansFysioAppDomainServices.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace AvansFysioApp.ViewComponents

{
    public class PatientAmountViewComponent : ViewComponent
    {
        private IRepo repository;

        public PatientAmountViewComponent(IRepo repository)
        {
            this.repository = repository;
        }   

        public IViewComponentResult Invoke()
        {
            var amount = repository.PatientAmount();
            return View(amount);
        }
    }
}
