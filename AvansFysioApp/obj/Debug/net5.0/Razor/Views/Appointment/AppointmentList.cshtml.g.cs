#pragma checksum "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Appointment\AppointmentList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c4debcbf1d64691f9c7d806e19aa143fac8bed0e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Appointment_AppointmentList), @"mvc.1.0.view", @"/Views/Appointment/AppointmentList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\_ViewImports.cshtml"
using AvansFysioApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\_ViewImports.cshtml"
using AvansFysioApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Appointment\AppointmentList.cshtml"
using AvansFysioAppDomain.Domain;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c4debcbf1d64691f9c7d806e19aa143fac8bed0e", @"/Views/Appointment/AppointmentList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ff812432d7ca83cd671183c1bfc40a71555f5aaf", @"/Views/_ViewImports.cshtml")]
    public class Views_Appointment_AppointmentList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Appointment>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<h2>Appointments</h2>\r\n\r\n");
#nullable restore
#line 7 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Appointment\AppointmentList.cshtml"
 foreach (Appointment a in @Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"textBlocks2\">\r\n        <table width=\"100%\">\r\n            <tr>\r\n                <th>Physiotherapist:</th>\r\n                <td>");
#nullable restore
#line 13 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Appointment\AppointmentList.cshtml"
               Write(a.HeadPhysiotherapist.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Date:</th>\r\n                <td>");
#nullable restore
#line 17 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Appointment\AppointmentList.cshtml"
               Write(a.AppointmentBegin.Day);

#line default
#line hidden
#nullable disable
            WriteLiteral("-");
#nullable restore
#line 17 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Appointment\AppointmentList.cshtml"
                                       Write(a.AppointmentBegin.Month);

#line default
#line hidden
#nullable disable
            WriteLiteral("-");
#nullable restore
#line 17 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Appointment\AppointmentList.cshtml"
                                                                 Write(a.AppointmentBegin.Year);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Time:</th>\r\n                <td>");
#nullable restore
#line 21 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Appointment\AppointmentList.cshtml"
               Write(a.AppointmentBegin.TimeOfDay);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 21 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Appointment\AppointmentList.cshtml"
                                               Write(a.AppointmentEnd.TimeOfDay);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n");
#nullable restore
#line 25 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Appointment\AppointmentList.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Appointment>> Html { get; private set; }
    }
}
#pragma warning restore 1591
