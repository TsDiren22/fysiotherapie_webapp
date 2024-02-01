using System;
using System.Linq;
using AvansFysioAppDomainServices.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace AvansFysioApp.ViewComponents

{
    public class AppointmentsViewComponent : ViewComponent
    {
        private SessionIRepo sessionIRepo;

        public AppointmentsViewComponent(SessionIRepo sessionIRepo)
        {
            this.sessionIRepo = sessionIRepo;
        }

        public IViewComponentResult Invoke()
        {
            var amount = sessionIRepo.Sessions().Count(x => x.AppointmentBegin.ToShortDateString() == DateTime.Now.ToShortDateString());
            return View(amount);
        }
    }
}