#pragma checksum "D:\store car web project\Views\Home\counter.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5742df1e41867281f53cbf607e7833800c8b8bd7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_counter), @"mvc.1.0.view", @"/Views/Home/counter.cshtml")]
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
#line 1 "D:\store car web project\Views\_ViewImports.cshtml"
using store_car_web_project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\store car web project\Views\_ViewImports.cshtml"
using store_car_web_project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5742df1e41867281f53cbf607e7833800c8b8bd7", @"/Views/Home/counter.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e61161af3d62039dd94ca84a50e5832ff2540f59", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_counter : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\store car web project\Views\Home\counter.cshtml"
  
    ViewData["Title"] = "counter";
    Layout = "~/Views/Shared/_MyLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section id=""counter"" class=""parallax-section bg-1 section overly"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-md-3 col-sm-6 col-xs-12 text-center wow fadeInDown"" data-wow-duration=""500ms"">
                <div class=""counters-item"">
                    <i class=""tf-ion-android-happy""></i>
                    <span data-speed=""3000"" data-to=""320"">320</span>
                    <h3>Happy Clients</h3>
                </div>
            </div>
            <div class=""col-md-3 col-sm-6 col-xs-12 text-center wow fadeInDown"" data-wow-duration=""500ms"" data-wow-delay=""200ms"">
                <div class=""counters-item"">
                    <i class=""tf-ion-archive""></i>
                    <span data-speed=""3000"" data-to=""565"">565</span>
                    <h3>Projects completed</h3>
                </div>
            </div>
            <div class=""col-md-3 col-sm-6 col-xs-12 text-center wow fadeInDown"" data-wow-duration=""500ms"" data-wow-delay=""400ms"">
      ");
            WriteLiteral(@"          <div class=""counters-item"">
                    <i class=""tf-ion-thumbsup""></i>
                    <span data-speed=""3000"" data-to=""95"">95</span>
                    <h3>Positive feedback</h3>

                </div>
            </div>
            <div class=""col-md-3 col-sm-6 col-xs-12 text-center wow fadeInDown"" data-wow-duration=""500ms"" data-wow-delay=""600ms"">
                <div class=""counters-item kill-margin-bottom"">
                    <i class=""tf-ion-coffee""></i>
                    <span data-speed=""3000"" data-to=""2500"">2500</span>
                    <h3>Cups of Coffee</h3>
                </div>
            </div>

        </div>
    </div>
</section>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
