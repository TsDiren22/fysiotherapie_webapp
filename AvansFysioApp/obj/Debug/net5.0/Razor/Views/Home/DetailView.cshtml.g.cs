#pragma checksum "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f891a36c17e25684ac1f19ba0f7707380f2cfc29"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_DetailView), @"mvc.1.0.view", @"/Views/Home/DetailView.cshtml")]
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
#line 1 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
using AvansFysioAppDomain.Domain;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f891a36c17e25684ac1f19ba0f7707380f2cfc29", @"/Views/Home/DetailView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ff812432d7ca83cd671183c1bfc40a71555f5aaf", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_DetailView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Patient>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditPatientView", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UpdateFileView", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddFileView", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "PatientList", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"form-group\">\r\n    <table style=\"width: 100%\">\r\n        <tr>\r\n            <th><h3>Patient:</h3></th>\r\n            <td>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f891a36c17e25684ac1f19ba0f7707380f2cfc294786", async() => {
                WriteLiteral("Edit Patient");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 8 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                                                  WriteLiteral(Model.PatientId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>Name:</th>\r\n            <td>");
#nullable restore
#line 12 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
           Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>Email:</th>\r\n            <td>");
#nullable restore
#line 16 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
           Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>Telephone:</th>\r\n            <td>");
#nullable restore
#line 20 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
           Write(Model.Phone);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>Occupation:</th>\r\n            <td>");
#nullable restore
#line 24 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
           Write(Model.Occupation);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>Student or worker ID:</th>\r\n            <td>");
#nullable restore
#line 28 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
           Write(Model.OccupationId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>Birthday:</th>\r\n            <td>");
#nullable restore
#line 32 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
           Write(Model.Birthday.Value.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <th>Gender:</th>\r\n            <td>");
#nullable restore
#line 36 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
           Write(Model.Gender);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n        <tr>\r\n            <td>&nbsp;</td>\r\n            <td>&nbsp;</td>\r\n            <td>&nbsp;</td>\r\n        </tr>\r\n\r\n\r\n");
#nullable restore
#line 45 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
         foreach (PatientFile pf in ViewBag.PatientFile)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
             if (pf != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <th><h3>Patient file:</h3></th>\r\n                    <td>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f891a36c17e25684ac1f19ba0f7707380f2cfc2910298", async() => {
                WriteLiteral("Edit patient file");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 51 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                                                         WriteLiteral(pf.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Patient:</th>\r\n                    <td>");
#nullable restore
#line 55 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.Patient.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Age:</th>\r\n                    <td>");
#nullable restore
#line 59 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.Age);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Description complaints:</th>\r\n                    <td>");
#nullable restore
#line 63 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.DescriptionComplaintsGlobal);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Diagnosis number:</th>\r\n                    <td>");
#nullable restore
#line 67 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.DiagnosisNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Intake by:</th>\r\n                    <td>");
#nullable restore
#line 71 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.IntakeBy.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Head Supervision by:</th>\r\n                    <td>");
#nullable restore
#line 75 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.SupervisionBy.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Head practitioner:</th>\r\n                    <td>");
#nullable restore
#line 79 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.HeadPractitioner.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Date of register:</th>\r\n                    <td>");
#nullable restore
#line 83 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.DateOfRegister.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>End date:</th>\r\n                    <td>");
#nullable restore
#line 87 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.DateOfEnd.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Remark:</th>\r\n                    <td>");
#nullable restore
#line 91 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.Remark);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Treatment plan:</th>\r\n                    <td>");
#nullable restore
#line 95 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.TreatmentPlan);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Treatments:</th>\r\n                    <td>");
#nullable restore
#line 99 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                   Write(pf.Treatments);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 101 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 102 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
             if (pf == null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f891a36c17e25684ac1f19ba0f7707380f2cfc2917906", async() => {
                WriteLiteral("Add to a patient file");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 107 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
                
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 108 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Home\DetailView.cshtml"
             
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\r\n</div>\r\n\r\n<br />\r\n<br />\r\n\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f891a36c17e25684ac1f19ba0f7707380f2cfc2919656", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Patient> Html { get; private set; }
    }
}
#pragma warning restore 1591
