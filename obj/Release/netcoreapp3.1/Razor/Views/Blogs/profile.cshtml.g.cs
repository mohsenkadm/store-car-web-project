#pragma checksum "D:\store car web project\Views\Blogs\profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "04c073ed0ce40e7a391b30538e7b5f734f78367c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Blogs_profile), @"mvc.1.0.view", @"/Views/Blogs/profile.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"04c073ed0ce40e7a391b30538e7b5f734f78367c", @"/Views/Blogs/profile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e61161af3d62039dd94ca84a50e5832ff2540f59", @"/Views/_ViewImports.cshtml")]
    public class Views_Blogs_profile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\store car web project\Views\Blogs\profile.cshtml"
  
    ViewData["Title"] = "profile";
    Layout = "~/Views/Shared/_MyLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<section class=""fadeInDown"">
    <div id=""animation""  style=""width: 30%; margin: 0 auto; margin-top: 2%;""></div>
</section>
<section id=""prifile"" class=""section"">
    <div class=""container"">
        <div class=""row"">
            <div class=""title text-center wow fadeInDown"">
                <h2>معلومات <span class=""color"">الصفحة</span></h2>
                <div class=""border""></div>
            </div>
            <div id=""info"" class=""service-block text-center wow fadeInUp"" style=""background-color:#171a1d; width:70%;  margin:0 auto;"">
            </div>
        </div>
    </div>
</section>
<section id=""create_posts"" class=""section-bg"">
    <div class=""container"">
        <div class=""row"">
            <!-- section title -->
            <div class=""title text-center wow fadeInDown"">
                <h2>انشاء <span class=""color"">منشور </span></h2>
                <div class=""border""></div>
            </div>
            <!-- /section title -->
            <div class=""text-center wow fadeI");
            WriteLiteral(@"nDown"">

                <a class=""btn btn-main""  onclick=""call_Action('Blogs/posts')"" style=""margin-bottom:2%;"">انشاء منشور</a>

            </div>
        </div> <!-- end row -->
    </div> <!-- end container -->
</section>
<section id=""blog"" class=""section"">
    <div class=""container"">
        <div class=""row"">
            <!-- section title -->
            <div class=""title text-center wow fadeInDown"">
                <h2>أخر <span class=""color"">المنشورات</span></h2>
                <div class=""border""></div>
            </div>
            <!-- /section title -->
            <div id=""posts"" class=""clearfix"">

            </div>
            <nav aria-label=""Page navigation"" class=""text-center"">
                <ul class=""pagination pagination-lg"" id=""pagination-wrapper"">
                </ul>
            </nav>
        </div> <!-- end row -->
    </div> <!-- end container -->
</section>
<div id=""mydiv"">
    <script>
        $(document).ready(function () {
            var o = {");
            WriteLiteral(@" i: 1 }
            call_ajax(""GET"", ""Blogs/GetPostsProfile"", o, buildTableprofile);
            call_ajax(""GET"", ""Account/GetUserInfo"", o, userinfo);
        });
        var animation = bodymovin.loadAnimation({
            container: document.getElementById('animation'),
            renderer: 'svg',
            loop: true,
            autoplay: true,
            path: ""lib/lottie/profile.json""
        })
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
