#pragma checksum "D:\store car  with translate\store car web project with translate\Views\Blogs\Notifecation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9e0347f1d7c39867b2fcbb00f23c196f7e7b8094"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Blogs_Notifecation), @"mvc.1.0.view", @"/Views/Blogs/Notifecation.cshtml")]
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
#line 1 "D:\store car  with translate\store car web project with translate\Views\_ViewImports.cshtml"
using store_car_web_project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\store car  with translate\store car web project with translate\Views\_ViewImports.cshtml"
using store_car_web_project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9e0347f1d7c39867b2fcbb00f23c196f7e7b8094", @"/Views/Blogs/Notifecation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e61161af3d62039dd94ca84a50e5832ff2540f59", @"/Views/_ViewImports.cshtml")]
    public class Views_Blogs_Notifecation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\store car  with translate\store car web project with translate\Views\Blogs\Notifecation.cshtml"
  
    ViewData["Title"] = "Notifecation";
    Layout = "~/Views/Shared/_MyLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<section id=""blog"" class=""section"">
    <div class=""container"">
        <div class=""row"">
            <div class=""title text-center wow fadeInDown"">
                <h2>Latest <span class=""color"">Notifications</span></h2>
                <div class=""border""></div>
            </div>
            <div id=""posts"" class=""clearfix"">
            </div>
        </div>
    </div>
</section>
<div id=""mydiv"">
    <script>
        $(document).ready(function () {
            $('#posts').empty()
            call_ajax(""GET"", ""Blogs/GetNotifecationPosts"", null, datw);
        });
    </script>
</div>
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
