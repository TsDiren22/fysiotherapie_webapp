#pragma checksum "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "ecfbe692e8106380db6445e9ff54ce8f91110c18575999b11040c7f8c15f60f5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Patientfile_TreatmentView), @"mvc.1.0.view", @"/Views/Patientfile/TreatmentView.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
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
#line 1 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
using AvansFysioAppDomain.Domain;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"ecfbe692e8106380db6445e9ff54ce8f91110c18575999b11040c7f8c15f60f5", @"/Views/Patientfile/TreatmentView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"c8913e621310fd12a5406378aa7d8c08d1d57d78a38f2674306c2f3e4dca7f18", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Patientfile_TreatmentView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<AvansFysioAppDomain.Domain.Treatment>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Patientfile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditTreatment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteTreatment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Patient", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DetailView", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-secondary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("    <h2>Treatments</h2>\r\n\r\n");
#nullable restore
#line 5 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
     foreach (Treatment treatment in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"textBlocks2\">\r\n            <table style=\"width: 100%\">\r\n                <tr>\r\n                    <th>Description:</th>\r\n                    <td>");
#nullable restore
#line 11 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                   Write(treatment.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Date of treatment:</th>\r\n                    <td>");
#nullable restore
#line 15 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                   Write(treatment.DateOfTreatment.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Date treatment added:</th>\r\n                    <td>");
#nullable restore
#line 19 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                   Write(treatment.DateCreated.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 21 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                 if (treatment.Particularities != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <th>Particularities:</th>\r\n                        <td>");
#nullable restore
#line 25 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                       Write(treatment.Particularities);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n");
#nullable restore
#line 27 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <th>Treatment:</th>\r\n                    <td>");
#nullable restore
#line 30 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                   Write(treatment.TypeId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n                <tr>\r\n                    <th>Room:</th>\r\n                    <td>");
#nullable restore
#line 34 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                   Write(treatment.Room);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 36 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                 if (treatment.DateCreated.ToString("dd-MM-yyyy") == DateTime.Now.ToString("dd-MM-yyyy") && ViewBag.IsPatient == null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <th></th>\r\n                        <td>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ecfbe692e8106380db6445e9ff54ce8f91110c18575999b11040c7f8c15f60f59576", async() => {
                WriteLiteral("Edit treatment");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 40 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                                                                                         WriteLiteral(treatment.Id);

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
            WriteLiteral(" | ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ecfbe692e8106380db6445e9ff54ce8f91110c18575999b11040c7f8c15f60f512061", async() => {
                WriteLiteral("Delete treatment");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 40 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                                                                                                                                                                                                        WriteLiteral(treatment.Id);

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
            WriteLiteral("</td>\r\n                    </tr>\r\n");
#nullable restore
#line 42 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\r\n        </div>\r\n");
#nullable restore
#line 45 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
 if (ViewBag.Id != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ecfbe692e8106380db6445e9ff54ce8f91110c18575999b11040c7f8c15f60f515473", async() => {
                WriteLiteral("Patient details");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 48 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
                                                          WriteLiteral(ViewBag.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 49 "C:\Users\diren\OneDrive\Bureaublad\Jaar2\SSWFIndividueel\AvansFysioApp\AvansFysioApp\Views\Patientfile\TreatmentView.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<AvansFysioAppDomain.Domain.Treatment>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
