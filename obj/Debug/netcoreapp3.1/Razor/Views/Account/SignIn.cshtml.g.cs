#pragma checksum "D:\store car  with translate\store car web project\Views\Account\SignIn.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2afd6fec61e5fab45acd1a166a5cd69dc5758524"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_SignIn), @"mvc.1.0.view", @"/Views/Account/SignIn.cshtml")]
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
#line 1 "D:\store car  with translate\store car web project\Views\_ViewImports.cshtml"
using store_car_web_project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\store car  with translate\store car web project\Views\_ViewImports.cshtml"
using store_car_web_project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2afd6fec61e5fab45acd1a166a5cd69dc5758524", @"/Views/Account/SignIn.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e61161af3d62039dd94ca84a50e5832ff2540f59", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_SignIn : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\store car  with translate\store car web project\Views\Account\SignIn.cshtml"
  
    ViewData["Title"] = "SignIn";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<article class=""wow fadeInDown"" data-wow-duration=""500ms"" data-delay=""400ms""
         style="" padding-top:85px; margin:0 auto; width:350px; "">
    <div class=""service-block text-center"" style=""background-color:#171a1d"">
        <div class=""search-icon text-center"">
            <i style=""font-size:90px;"" class=""tf-ion-coffee""></i>
        </div>
        <h3>sign in</h3>
        <br />
        <div id=""contact-form"">
            <div id=""login"">
                <div class=""form-group"">
                    <input type=""text"" placeholder=""user name"" autocomplete=""off"" class=""form-control  text-right"" name=""name"" id=""name"" />
                </div>
                <div class=""form-group"">
                    <input type=""password"" placeholder=""password"" autocomplete=""off"" class=""form-control text-right"" name=""pass"" id=""pass"" />
                </div>
                <div id=""cf-submit"">
                    <input type=""submit"" id=""login-btn"" class=""btn btn-transparent btn-animated"" value=""Login"" /");
            WriteLiteral(@">
                </div>
                <div class=""inline-block"" style=""margin-top:20px; margin-bottom:-20px;"">
                    <a style=""cursor:pointer; float:left;"" onclick=""call_Action('Account/SignUp')"">Don't have an account? Register now</a>
                    <a style=""cursor:pointer; float:right; margin-left:75px;"" onclick=""forgat_pass()"">I forgot the password?</a>
                </div>
            </div>
            <div id=""email"" style=""display:none;"">
                <div class=""form-group"">
                    <input type=""text"" placeholder=""email"" autocomplete=""off"" class=""form-control"" name=""emailtxt"" id=""emailtxt"" />
                </div>
                <div id=""cf-submit"">
                    <input type=""submit"" id=""email-btn"" class=""btn btn-transparent btn-animated"" value=""Send the code"" />
                </div>
            </div>
            <div id=""confirm"" style=""display:none;"">
                <div class=""form-group"">
                    <input type=""text"" pl");
            WriteLiteral(@"aceholder=""confirmation code"" autocomplete=""off"" class=""form-control"" name=""code"" id=""code"" />
                </div>
                <div id=""cf-submit"">
                    <input type=""submit"" id=""confirm-btn"" class=""btn btn-transparent btn-animated"" value=""Account Confirmation"" />
                </div>
            </div>
            <div id=""newpass"" style=""display:none;"">
                <div class=""form-group"">
                    <input type=""text"" placeholder=""New Password"" autocomplete=""off"" class=""form-control"" name=""newpassword"" id=""newpassword"" />
                </div>
                <div id=""cf-submit"">
                    <input type=""submit"" id=""updatepass-btn"" class=""btn btn-transparent btn-animated"" value=""Password change"" />
                </div>
            </div>
        </div>
    </div>
</article>
<div id=""mydiv"">
    <script>
        $(""#updatepass-btn"").on(""click"", (e) => {
            e.preventDefault();
            var object = {
                pass: $(""#ne");
            WriteLiteral(@"wpassword"").val(),
                email: $(""#emailtxt"").val(),
            };
            if (object.pass === """" || object.pass.trim() === """") {
                toust.warning(""Please enter a password"");
                return;
            }
            call_ajax(""POST"", ""Account/Update_Pass"", object, after_updata_pass );
        });
        $(""#email-btn"").on(""click"", (e) => {
            e.preventDefault();
            var object = {
                email: $(""#emailtxt"").val(),
            };
            if (object.email === """" || object.email.trim() === """") {
                toust.warning(""Please enter an email"");
                return;
            }
            call_ajax(""POST"", ""Account/ForgatePassword"", object, after_write_email);
        });
        $(""#confirm-btn"").on(""click"", (e) => {
            e.preventDefault();
            var object = {
                code: $(""#code"").val(),
                username: '',
                Email: $(""#emailtxt"").val()
            };
  ");
            WriteLiteral(@"          if (object.code === """" || object.code.trim() === """") {
                toast.warning(""Please enter a confirmation code"");
                return;
            }
            call_ajax(""POST"", ""Account/ConfirmAccount"", object, after_confirm);
        });
        function after_login(token) {
            setCookie(""token1"", token, 0.5);
            call_Action(""home/index"");
        }
        $(""#login-btn"").on(""click"", (e) => {
            e.preventDefault();
            var object = {
                username: $(""#name"").val(),
                password: $(""#pass"").val(),
            };
            if (object.username === """" || object.username.trim() === """") {
                toust.warning(""Please enter your username"");
                return;
            }
            else if (object.password === """" || object.password.trim() === """") {
                toust.warning(""Please enter a password"");
                return;
            }
            call_ajax(""POST"", ""Account/Login"", ob");
            WriteLiteral("ject, after_login);\r\n        });\r\n    </script>\r\n</div>");
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