#pragma checksum "D:\store car  with translate\store car web project with translate\Views\Blogs\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "415b104a4df6c03719a0916a893d3b799b220a71"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Blogs_Index), @"mvc.1.0.view", @"/Views/Blogs/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"415b104a4df6c03719a0916a893d3b799b220a71", @"/Views/Blogs/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e61161af3d62039dd94ca84a50e5832ff2540f59", @"/Views/_ViewImports.cshtml")]
    public class Views_Blogs_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\store car  with translate\store car web project with translate\Views\Blogs\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_MyLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<section id=""create_posts"" class=""section-bg"">
    <div class=""container"">
        <div class=""row"">
            <div class=""title text-center header-prmary-sub wow fadeInDown"">
                <h2>Create a  <span class=""color"">Post </span></h2>
                <div class=""border""></div>
            </div>
            <div class=""text-center wow fadeInDown"">
                <a class=""btn btn-main"" onclick=""call_Action('Blogs/posts')"" style=""margin-bottom:2%;"">Create a Post</a>
            </div>
        </div> 
    </div> 
</section> 
<section id=""blog"" class=""section"">
    <div class=""container"">
        <div class=""row"">
            <div class=""title text-center header-prmary-main wow fadeInDown"">
                <h2>Latest<span class=""color"">Posts</span></h2>
                <div class=""border""></div>
            </div>
            <div id=""posts"" class=""clearfix"">
            </div>
            <div aria-label=""Page navigation""  class=""text-center"">
                <ul class=""pagina");
            WriteLiteral(@"tion pagination-lg"" id=""pagination-wrapper"">
                </ul>
            </div>
        </div>
    </div>
</section>
<div id=""mydiv"">
    <script>
        $(document).ready(function () {
            call_ajax(""GET"", ""Blogs/GetPosts"", null, buildTable);
           
        });

        $(""#commendinput"").keyup(function (e) {
            if (e.keyCode == 13) {
            debugger;
                event.preventDefault();
                $(""#commendbtn"").click();
            }
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
