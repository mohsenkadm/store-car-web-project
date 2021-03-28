const baseUrl = "/";
function call_ajax_json(method, url, param, object, call_back_func) {
    var userToken = getCookie("token1");
    mouseevent("progress");
    $.ajax({
      method: method,
      url: baseUrl + url + param,
      cache: true, async: true,
      dataType: 'json', 
      contentType: 'application/json; charset=UTF-8',
      data: JSON.stringify(object),
      headers: {
        'Authorization': `Bearer ${userToken}`,
      },
        success: (respons) => {
           
            mouseevent("default");
            if (respons.success) {
                if (typeof (call_back_func) == 'function') {
                    if (respons.data != undefined)
                        call_back_func(respons.data);
                    else
                        call_back_func();
                }
                else if (typeof (call_back_func) == 'string' && call_back_func == 'return') {
                    return respons.data;
                }
                if (respons.msg != null && respons.msg != undefined)
                    toust.success(respons.msg);
            }
            else {
                if (respons.msg != null && respons.msg != undefined)
                    toust.error(respons.msg);
            }
        },
        error: (e) => {
            mouseevent("default");
            toust.error('حدث خطأ عند الأتصال');
        }
    });
  }
function call_ajax(method, url, object, call_back_func) {
    var userToken = getCookie("token1");
    mouseevent("progress");
    $('.preloader').fadeIn();
      $.ajax({
      method: method,
      url: baseUrl + url ,
      cache: true, async: true,
          data: object,
      headers: {
        'Authorization': `Bearer ${userToken}`,
      },
          success: (respons) => {
         //   /  debugger;
              mouseevent("default");
              $('.preloader').fadeOut();
              if (respons.success) {
                  if (typeof (call_back_func) == 'function') {
                      if (respons.data != undefined)
                          call_back_func(respons.data);
                      else
                          call_back_func();
                  }
                  else if (typeof (call_back_func) == 'string'
                      && call_back_func == 'return') {
                      return respons.data;
                  }
                  if (respons.msg != null && respons.msg != undefined)
                      toust.success(respons.msg);
              }
              else {
                  if (respons.msg != null && respons.msg != undefined)
                      toust.error(respons.msg);
              }

          },
          error: (e) => {
              mouseevent("default");
              $('.preloader').fadeOut();
             toust.error('حدث خطأ عند الأتصال');
          }
      });
}
function call_Action(url,i) {
    //debugger;
    if (i === 1) {
        $('#notmodel').modal('hide');
    }
   else  if (i === 2) {
        $('#commentmodel').modal('hide');
    }
    else if (i === 3) {
        $('#likemodel').modal('hide');
    }
    var userToken = getCookie("token1");
        mouseevent("progress");
        $.ajax({
            method: 'GET',
            url: baseUrl + url,
            cache: true, async: true,
            headers: {
                'Authorization': `Bearer ${userToken}`,
            },
            success: (respons) => {
                $('.preloader').fadeIn();
                mouseevent("default");
                var from = respons.indexOf("<!-- CUT FROM HERE -->");
                document.body.innerHTML = respons.substring(from, respons.length - 16);
                window.scrollTo(0, 0);
                var mydiv = document.getElementById("mydiv");
                var scripts = mydiv.getElementsByTagName("script");
               
                for (var i = 0; i < scripts.length; i++) {
                   
                    eval(scripts[i].innerText);
                }
                $('.preloader').fadeOut();
            }
        });

}
function mouseevent(type) {
    $("body").css("cursor", type);
    //type =progress ,default
}
function setCookie(cname, cvalue, exdays) {
    //debugger;
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24  *60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function getCookie(cname) {
  //  debugger;
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
var id1;
var connection = new signalR.HubConnectionBuilder().withUrl("/Signalr").build();
connection.on('displaynot', function () {
    call_ajax("GET", "Blogs/GetCountNotification", null, SetCountNotifaction);
});
connection.on('displaymessage', function () {
    call_ajax("GET", "Chat/GetMessagechat", null, GetMessagechat);
    setCountMessage();
});

connection.start();
function datw(data) {
    if (data.length === 0) {
        toust.error("لا توجد منشورات");
        return;
    }
    $.each(data, function (i, item) {
        console.log(data);
        var islike = '#fff';
        var online = '';
        if (item.isliked === true) {
            islike = '#57cbcc';
        }
        if (item.isonline === true) {
            online = '<i class="tf-ion-ios-circle-filled onlineradio"></i>';
        }
        //debugger;
        var typeimage;
        var str = 'images/imag_post/' + item.imagepath;
        if (item.imagecount>1) {
            typeimage = " <div class='filtr-item '>" +
                " <div   style='margin-bottom:0;'>" +
                "   <img  src='" + str + "' style='width:100%; height:30%;' alt='image'>" +
                " </div>" +
                "</div>" +
                "<div style='text-align: center;'> <a class='btn btn-main'  onclick =\"call_ajax('GET', 'Blogs/Getimages/" + item.post_id +"', null, buildimage)\" data-toggle='modal' data-target='#imageall'  data-effect='mfp-with-zoom' data-scroll>" +
                "  " + item.imagecount + "+ عرض المزيد من الصور" +
                "</a> </div>";
        } else {
            typeimage = " <div class='filtr-item '>" +
                " <div class='portfolio-block' style='margin-bottom:0;'>" +
                "   <img  src='" + str + "' style='width:100%; height:30%;' alt='image'>" +
                "     <div class='caption'>" +
                " <a class='search-icon image-popup' onclick =\"setimageinsidemodel(\'" + str + "')\" data-toggle='modal' data-target='#image'  data-effect='mfp-with-zoom' data-scroll>" +
                "   <i class='tf-ion-android-search'></i>" +
                "</a>" +
                " <h4>تكبير الصورة</h4>" +
                " </div>" +
                " </div>" +
                "</div>";
        };
        //var res = str.slice(32, str.length);
        var rows = " <article class='wd   wow fadeInUp' data-wow-duration='500ms' data-wow-delay='400ms'>" +
            "<div  class='post-block' style='border-radius: 15px;'>" +
            " <div class='content' >" +
            " <div class='price-title text-right' style='padding:0px;' >" +
            "<h2> " + online + "<a onclick=\"call_Action(\'Blogs/profileUser/" + item.user_id + "')\"> " + item.userName + "</a></h2>" +
            " <br/>" +
            " <h3>" + item.title + " </h3>" +
            " <span style='float:none;'> " + item.date + "/ تاريخ النشر  </span>" +
            " <p><strong class='value'>$" + item.price + "</strong>/ السعر</p>" +
            " <p><strong class='value'>" + item.model + "</strong>/الموديل</p>" +
            "  <p>الشركة \ " + item.company_id + "</p>" +
            " <div class='con-info clearfix text-right' style='float:none;'>" +
            "  <span style='float:none;'>" + item.address + "</span>" +
            "    <i class='tf-map-pin' style='float:none;'></i>" +
            "</div>" +
            " <div class='con-info clearfix text-right' style='float:none;'>" +
            "   <span style='float:none;'>" + item.phone + " /رقم الهاتف </span>" +
            "  <i class='tf-ion-ios-telephone-outline' style='float:none;'></i>" +
            " </div>" +
            "</div >" +
            "<p class='text-right'>" + item.commend + "</p>" + "</div >" +
            typeimage+
            "<div class='divdown'>" +
            "  <span data-toggle='modal' id='like_count'  onclick='getlikes(" + item.post_id + ")' data-target='#likemodel' style='float: right; cursor:pointer;'><a class='value1' id='setlike" + item.post_id + "'>" + item.count_like + "</a> اعجابات </span>" +
            " <span data-toggle='modal' id='command_count'  onclick='getcommends(" + item.post_id + ")' data-target='#commentmodel' style=' cursor:pointer;'> <a class='value1' id='setcommend" + item.post_id + "'>" + item.count_comment + "</a> تعليقات </span>" +
            " </div>" +
            " <button class='btn2 btn  '  type='button'  onclick=\"call_Action(\'Chat/Chat/" + item.user_id +"')\"  style=' float:left;  '>تواصل <i class='icon tf-ion-ios-chatbubble-outline'></i></button>" +
            "<button class='btn2 btn '  type='button'  onclick='getcommends(" + item.post_id + ")'   data-toggle='modal' data-target='#commentmodel' > تعليق <i class='icon tf-ion-ios-chatbubble-outline'></i>   </button>" +
            "<button type='button' id='btnlike" + item.post_id + "' style='color:" + islike + ";float: right;' onclick='postlike(" + item.post_id + ")' class='btn2 btn ' >  اعجبني <i class='icon tf-ion-thumbsup'></i> </button>"
            + "<label onclick='getcommends(" + item.post_id + ")'   data-toggle='modal' data-target='#commentmodel' style='width:95%; margin-left:10px; margin-right:10px; border-radius:20px; background-color:#353b43; text-align: right; padding:7px; padding-right:18px;' > ... اكتب تعليقك </label>" +
            "</div>" +
            " </article>";
        $('#posts').append(rows);
    });
};
function setimageinsidemodel(image) {
    //debugger;
    var src = image
   // var res = src.slice(15, src.length);
    $('#setimage1').empty();
    var rows1 = "<img class='mySlides'  src='" + image + "' style='width:100%; display: block;'>";
    $('#setimage1').append(rows1);
};
function datprofile(data) {
    if (data.length === 0) {
        toust.error("لا توجد منشورات");
        return;
    }
    $.each(data, function (i, item) {
        var islike = '#fff';
        var online = '';
        if (item.isliked === true) {
            islike = '#57cbcc';
        }
        if (item.isonline === true) {
            online = '<i class="tf-ion-ios-circle-filled onlineradio"></i>';
        }
        var typeimage;
        var str = 'images/imag_post/' + item.imagepath;
        if (item.imagecount > 1) {
            typeimage = " <div class='filtr-item '>" +
                " <div   style='margin-bottom:0;'>" +
                "   <img  src='" + str + "' style='width:100%; height:30%;' alt='image'>" +
                " </div>" +
                "</div>" +
                "<div style='text-align: center;'> <a class='btn btn-main'  onclick =\"call_ajax('GET', 'Blogs/Getimages/" + item.post_id + "', null, buildimage)\" data-toggle='modal' data-target='#imageall'  data-effect='mfp-with-zoom' data-scroll>" +
                "  " + item.imagecount + "+ عرض المزيد من الصور" +
                "</a> </div>";
        } else {
            typeimage = " <div class='filtr-item '>" +
                " <div class='portfolio-block' style='margin-bottom:0;'>" +
                "   <img  src='" + str + "' style='width:100%; height:30%;' alt='image'>" +
                "     <div class='caption'>" +
                " <a class='search-icon image-popup' onclick =\"setimageinsidemodel(\'" + str + "')\" data-toggle='modal' data-target='#image'  data-effect='mfp-with-zoom' data-scroll>" +
                "   <i class='tf-ion-android-search'></i>" +
                "</a>" +
                " <h4>تكبير الصورة</h4>" +
                " </div>" +
                " </div>" +
                "</div>";
        };
      //  var str = item.image;
      //  var res = str.slice(43, str.length);
        var rows = " <article class='wd   wow fadeInUp' data-wow-duration='500ms' data-wow-delay='400ms' >" +
            "<div  class='post-block' style='border-radius: 15px;'>" +
            " <div class='content' >" +
            " <div class='price-title text-right' style='padding:0px;' >" +
            "<h2>" + online + "<a >" + item.userName + "</a></h2>" +
            " <br />" +
            " <h3>" + item.title + " </h3>" +
            " <span style='float:none;'> " + item.date + " / تاريخ النشر  </span>" +
            " <p><strong class='value'>$" + item.price + "</strong>/ السعر</p>" +
            " <p><strong class='value'>" + item.model + "</strong>/الموديل</p>" +
            "  <p>الشركة \ " + item.company_id + "</p>" +
            " <div class='con-info clearfix text-right' style='float:none;'>" +
            "  <span style='float:none;'>" + item.address + "</span>" +
            "    <i class='tf-map-pin' style='float:none;'></i>" +
            "</div>" +
            " <div class='con-info clearfix text-right' style='float:none;'>" +
            "   <span style='float:none;'>" + item.phone + " /رقم الهاتف </span>" +
            "  <i class='tf-ion-ios-telephone-outline' style='float:none;'></i>" +
            " </div>" +
            "</div >" +
            "<p class='text-right'>" + item.commend + "</p>" + "</div >" +
            typeimage +
            "<div class='divdown'>" +
            "  <span data-toggle='modal' id='like_count'  onclick='getlikes(" + item.post_id + ")' data-target='#likemodel' style='float: right; cursor:pointer;'><a class='value1' id='setlike" + item.post_id + "'>" + item.count_like + "</a> اعجابات </span>" +
            " <span data-toggle='modal' id='command_count'  onclick='getcommends(" + item.post_id + ")' data-target='#commentmodel' style=' cursor:pointer;'> <a class='value1' id='setcommend" + item.post_id + "'>" + item.count_comment + "</a> تعليقات </span>" +
            " </div>" +
            "   <button type='button' id='promet_btn'  class='btn btn-transparent'  data-toggle='modal' data-target='#promotmodel'  style=' border:0px; border-top: 1px solid #4e595f; border-bottom: 1px solid #4e595f; margin-bottom: 10px; width: 33%; '> <i style='margin-right:10%;'  class='tf-megaphone'></i> ترويج المنشور </button>" +
            "   <button type='button' id='delete_btn' onclick='delete_btn(" + item.post_id + ")' class='btn btn-transparent'  style=' border:0px; border-top: 1px solid #4e595f; border-bottom: 1px solid #4e595f; margin-bottom: 10px;width:33%; '> <i style='font-size: 120%;margin-right:10%;' class='tf-ion-trash-b'></i> حذف المنشور </button>" +
            " <button type='button' id='update_btn' onclick='update_btn(" + item.post_id + ")' class='btn btn-transparent' style = 'border:0px; border-top: 1px solid #4e595f; border-bottom: 1px solid #4e595f; margin-bottom: 10px; float: right; width: 33%;' > <i style='margin-right:10%;' class='tf-pencil2'></i> تعديل المنشور  </button >"
            + "<label onclick='getcommends(" + item.post_id + ")'   data-toggle='modal' data-target='#commentmodel' style='width:95%; margin-left:10px; margin-right:10px; border-radius:20px; background-color:#353b43; text-align: right; padding:7px; padding-right:18px;' > ... اكتب تعليقك </label>" +
            "</div>" +
            " </article>";

        $('#posts').append(rows);
    });
};
function likes(data) {
    $('#like').empty();
    if (data.length === 0) {
        toust.error("لا توجد اعجابات");
        return;
    }
    $.each(data, function (i, item) {
        var url = '/Blogs/profileUser/' + item.user_id;
        var rows = " <h3><a onclick=\"call_Action(\'Blogs/profileUser/" + item.user_id + "',3)\"> " + item.userName + "</a></h3>" +
            +"<span style = 'float:none;' > " + item.date + "/ تاريخ الاعجاب  </span>";
        $('#like').append(rows);
    });
};
function command(data) {
    $('#commands').empty();
    if (data.length === 0) {
        toust.error("لا توجد تعليقات");
    }
    $.each(data, function (i, item) {
        var url = '/Blogs/profileUser/' + item.user_id;
        var rows = "<div class='price-title text-right'>" +
            " <h3><a onclick=\"call_Action(\'Blogs/profileUser/" + item.user_id + "',2)\"> " + item.userName + "</a></h3>" +
            "   <span style='float:none;'> " + item.date + "/ تاريخ النشر التعليق  </span>" +
            "</div>" +
            " <p style='padding: 1%;background-color:#292F36;' class=' text-right'>" + item.commend
        " </p>";
        //id1 = item.post_id;
        $('#commands').append(rows);
    });
    var row = "<div class='clearfix'>" +
        "<div class='col-lg-9 col-md-9 col-sm-9 col-xs-8' >" +
        " <div class='form-group'>" +
        "<input placeholder='اكتب تعليقك' type='text' class='bo form-control text-right' autocomplete='off' name='commendinput' id='commendinput' />" +
        " </div>" +
        "</div >" +
        "<div class='col-lg-2 col-md-2 col-sm-2 col-xs-2'>" +
        "<button type='button'  name='commendbtn' id='commendbtn' onclick='postcommends()'  class='btn btn-transparent' >  نشر </button>" +
        " </div>" +
        "</div >";
    $('#commands').append(row);
};
//event enter commend

function note(data) {
    $('#note').empty();
    console.log(data);
    if (data.length === 0) {
        toust.error("لا توجد اشعارات");
        return;
    }
    $.each(data, function (i, item) {
        
        var color = "#292F36";        var seen1 = item.seen;
        if (seen1) {
            color = "#353b43";
        }
        if (item.type_note === 1) {
            var rows = "<div id='" + item.id + "note'   class='price-title text-right' style='background:" + color + ";' >" + "<div class='row' style='margin-right: 0px;'>" +
                "<div class='form-group col-md-3 col-sm-3 col-xs-3' > <a  onclick=\"deletenot(" + item.id + ")\" style='padding: 7px 11px; border-radius: 50%;' class='btn btn-transparent' ><i style='font-size: 177%;' class='tf-ion-trash-b'></i></a>"
                + "<a onclick=\"call_Action(\'Blogs/Notifecation/" + item.post_id + "',1)\" style='padding: 7px 11px; border-radius: 50%;     margin-left: 7%;'  class='btn btn-transparent' > <i style='font-size: 177%;' class='tf-ion-ios-eye-outline'></i></a>" + "</div> "
                + "<div class='form-group col-md-9 col-sm-9 col-xs-9' >" +
                "<p>" + item.userName + " تم الاعجاب على منشورك من قبل </p>" +
                +" <span style = 'float:none;' >" + item.date + "/ تاريخ الاشعار  </span>"
                + " </div>" +
                "</div>" + "</div>";
        }
        else {
            var rows = "<div id='" + item.id + "note'  class='price-title text-right' style='background:" + color + ";'>" + "<div class='row' style='margin-right: 0px;'>" +
                "<div class='form-group col-md-3 col-sm-3 col-xs-3' > <a  onclick=\"deletenot(" + item.id + ")\" style='padding: 7px 11px; border-radius: 50%;' class='btn btn-transparent' ><i style='font-size: 177%;' class='tf-ion-trash-b'></i></a>"
                + "<a  onclick =\"call_Action(\'Blogs/Notifecation/" + item.post_id + "',1)\" style='padding: 7px 11px; border-radius: 50%; margin-left: 7%;' class='btn btn-transparent' ><i style='font-size: 177%;' class='tf-ion-ios-eye-outline'></i></a>" + "</div> "
                + "<div class='form-group col-md-9 col-sm-9 col-xs-9' >" +
                "<p>" + item.userName + " تم التعليق على منشورك من قبل </p>" +
                +" <span style = 'float:none;' >" + item.date + "/ تاريخ الاشعار  </span>"
                + " </div>" +
                "</div>" + "</div>";
        }
        $('#note').append(rows);
    
    });
    call_ajax("POST", "Blogs/SetNotification", null, SetCountNot);
};

function deletenot(id) {
    var object = {
        notifcation_id: id,
    };
    call_ajax("DELETE", "Blogs/Deletenotifcation", object, null);
    var id = "#" + id + 'note';
    $(id).fadeOut();
}
function message(data) {
    $('#message').empty();
    console.log(data);
    if (data.length === 0) {
        toust.error("لا توجد رسايل");
        return;
    }
    $.each(data, function (i, item) {
        var color = "#292F36";
        var seen2 = item.seen;
        if (seen2) {
            color = "#353b43";
        }
        var rows = "<div id='" + item.user_sender_id + "div1' class='price-title text-right' style='background:" + color + ";' >" + "<div class='row' style='margin-right: 0px;'>" +
            "<div class='form-group col-md-3 col-sm-3 col-xs-3' > <a  onclick=\"deletemessages(" + item.user_sender_id + ")\" style='padding: 7px 11px; border-radius: 50%;' class='btn btn-transparent' ><i style='font-size: 177%;' class='tf-ion-trash-b'></i></a>"
                + "<a data-dismiss='modal' onclick=\"call_Action(\'Chat/Chat/" + item.user_sender_id +"')\"  style='padding: 7px 11px; border-radius: 50%; margin-left: 7%;' class='btn btn-transparent' ><i style='font-size: 177%;' class='tf-ion-ios-eye-outline'></i> </a>" + "</div> "
                + "<div class='form-group col-md-9 col-sm-9 col-xs-9' >" +
                "<p>" + item.user_name_s + " تم ارسال رسالة اليك من قبل </p>" 
                + "<p style='float:none;' >" + item.message_txt + " </p>"
                +"<span style='float:none;' >" + item.message_date + "/ تاريخ الرسالة  </span>"
                + "</div>" +
                "</div>" + "</div>";
        $('#message').append(rows);
    });
    call_ajax("POST", "Chat/SetCountMessage", null, setCountMessage);
};
function deletemessages(id) {
    var object = {
         user_id:id,
    };
    call_ajax("DELETE", "Chat/RemoveMessages", object, null);
    var id ="#"+ id + 'div1';
    $(id).fadeOut();
}

function GetMessagechat(data) {
    if (data.length === 0) {
        toust.error("لا توجد رسائل الى الان");
        return;
    }
    var rows;
    $('#messges').empty();
    rows = "<div class=' text-center'><a  id='" + data.id + "'  class='btn btn-transparent' style='margin-top: 10px; margin-bottom: 20px;'  >عرض المزيد من الرسائل</a><div/>";
    $('#messges').append(rows);
    rows = "";
    console.log(data.id);
    $.each(data, function (i, item) {

        if (item.reciver === true) {
            rows = "<div class=' l'>" + "<div class='dl col' onclick=\"visiblebtn(\'#" + item.id + "btn','#" + item.id + "date')\"> <label> " + item.message_txt + "</label> </div></div>" +
                " <span style='float:left;'  class='date' id='" + item.id + "date'>" + item.message_date + "/تاريخ الارسال </span>";
        }
        else {
            var styleseenoutline = 'display:none;';
            var styleseeninline = 'display:none;';
            if (item.seen) {
                styleseenoutline = 'display:none;';
                styleseeninline = 'display:inline;';
            }
            else {
                styleseenoutline = 'display:inline;';
                styleseeninline = 'display:none;';
            }
            rows = "<div class='row r'  id='" + item.id + "div'>" + "<a  id='" + item.id + "checkmark' style='" + styleseeninline + "' class='tf-ion-ios-checkmark check'></a> " +
                "<a id='" + item.id + "checkmarkoutline' style='" + styleseenoutline + "'  class='tf-ion-ios-checkmark-outline check '></a> " +
                " <div class='row dr' onclick=\"visiblebtn(\'#" + item.id + "btn','#" + item.id + "date')\"> <label class='col' >" + item.message_txt + "</label>    </div>" +
                "<div id='" + item.id + "btn' class='col btntrash'>" +
                " <a onclick=\"deletemessage(" + item.id + ")\"  style='padding: 7px 11px;  border-radius: 50%;' class='  btn btn-transparent'><i style='font-size: 177%;' class='tf-ion-trash-b'></i></a>" +
                " </div>" +
                "</div>" +
                "<span class='date' id='" + item.id + "date' style='float:right;'>" + item.message_date + " /تاريخ الارسال </span>";
        }

        $('#messges').append(rows);
    });
    window.scrollTo(0, document.body.scrollHeight);
}
function deletemessage(id_message) {
    var object = {
        id: id_message,
    };
    call_ajax("DELETE", "Chat/RemoveMessage", object, null);

    var iddiv = "#" + id_message + 'div';
    var iddate = "#" + id_message + 'date';
    $(iddiv).fadeOut();
    $(iddate).fadeOut();
}

function userchatinfo (data) {
    $('#userchatinfoid').empty();
    var online = '';
    if (data.isonline === true) {
         online = '<i style="margin-right: 2%;" class="tf-ion-ios-circle-filled onlineradio"></i>';
    }
    //$.each(data, function (i, item) {
    var rows = "<h3 style='margin-top: -32px;'>" +online+ data.userName + " </h3>" +
        "<div class='clearfix' style='float:none;'>" +
        "       <span style='float:none;'>" + data.email + " / ايميل </span>" +
            " <i class='tf-ion-ios-email-outline' style='float:none;'></i>" +
            "  </div>";
        $('#userchatinfoid').append(rows);
    //});
};
function userinfo(data) {
    $('#info').empty();
    var online = '';
    if (data.isonline === true) {
        online = '<i style="margin-right: 2%;" class="tf-ion-ios-circle-filled onlineradio"></i>';
    }
        //$.each(data, function (i, item) {
        var rows = "  <div class='price-title text-right' style='background-color:#171a1d'>" +
            "  <h3> "  +online + data.userName + " / الأسم </h3 >" +
            "  <br />" +
            "<div class='con-info clearfix text-right' style='float:none;'>" +
            "       <span style='float:none;'>" + data.email + " / ايميل </span>" +
            " <i class='tf-ion-ios-email-outline' style='float:none;'></i>" +
            "  </div>" +
            "  <div class='con-info clearfix text-right' style='float:none;'>" +
            "  <span style='float:none;'>" + data.counts + "/ عدد المنشورات  </span>" +
            "   </div>" +
            "  </div >";
        $('#info').append(rows);
    //});
};
var state = {
    'querySet': null, //
    'page': 1,
    'rows': 5,
    'window': 7,
} 
function pagination(querySet, page, rows) {
    var trimStart = (page - 1) * rows
    var trimEnd = trimStart + rows
    var trimmedData = querySet.slice(trimStart, trimEnd);
    var pages = Math.round(querySet.length / rows);
    return {
        'querySet': trimmedData,
        'pages': pages,
    }
}
function pageButtons(pages, i) {
    var wrapper = document.getElementById('pagination-wrapper')
    wrapper.innerHTML = ``
    console.log('Pages:', pages)
    var maxLeft = (state.page - Math.floor(state.window / 2))
    var maxRight = (state.page + Math.floor(state.window / 2))
    if (maxLeft < 1) {
        maxLeft = 1
        maxRight = state.window
    }
    if (maxRight > pages) {
        maxLeft = pages - (state.window - 1)
        if (maxLeft < 1) {
            maxLeft = 1
        }
        maxRight = pages
    }
    for (var page = maxLeft; page <= maxRight; page++) {
        wrapper.innerHTML += `<li> <button  value=${page} class="page ">${page}</button> </li>`
    }
    if (state.page != 1) {
        wrapper.innerHTML = `<li> <button value=${1} class="page ">&#171; </button> </li>` + wrapper.innerHTML
    }
    if (state.page != pages) {
        wrapper.innerHTML += `<li> <button value=${page} class="page "> &#187;</button> </li>`
    }

    $('.page').on('click', function () {
        $('#posts').empty()
        state.page = Number($(this).val())
        if (i === 1) {
            call_ajax("GET", "Blogs/GetPosts", null, buildTable);
        }
        else if (i === 2) {
            var o = { i: 1 }
            call_ajax("GET", "Blogs/GetPostsProfile", o, buildTableprofile);
        }
        else if(i===3){
            var o = { i: 2 }
            call_ajax("GET", "Blogs/GetPostsProfile", o, buildTableprofileuser);
        }
    })

}
function buildTable(data) {
    //state.page = 1;
    state.querySet = data;
    var dat = pagination(state.querySet, state.page, state.rows);
    datw(dat.querySet);
    pageButtons(dat.pages, 1);
}
function buildTableprofile(data) {
    state.page = 1;
    state.querySet = data;
    var dat = pagination(state.querySet, state.page, state.rows);
    datprofile(dat.querySet);
    pageButtons(dat.pages, 2);
}
function buildTableprofileuser(data) {
    state.page = 1;
    state.querySet = data;
    var dat = pagination(state.querySet, state.page, state.rows);
    datw(dat.querySet);
    pageButtons(dat.pages, 3);
}
function getcommends(id) {
    var o = {
        post_id: id,
    };
    id1 = id;
    call_ajax("GET", "Blogs/GetCommend", o, command);
};
function getlikes(id) {
    var o = {
        post_id: id,
    };

    call_ajax("GET", "Blogs/GetLike", o, likes);
};
function delete_btn(id) {
    var o = {
        post_id: id,
    };
    call_ajax("Delete", "Blogs/Deletepost", o, null);
};
function update_btn(id) {
    var o = {
        post_id: id,
    };
    call_Action("Blogs/Update_posts/" + o.post_id);
};
function postlike(id) {
    var object = {
        post_id: id,
    };
    var color = document.getElementById('btnlike' + id).style.color;
    if (color == "rgb(87, 203, 204)") {
        document.getElementById('btnlike' + id).style.color = '#fff';
    } else {
        document.getElementById('btnlike' + id).style.color = '#57cbcc';
    }
    call_ajax("POST", "Blogs/PostLike", object, setlike);
};
function setlike(data) {
    document.getElementById('setlike' + data.post_id).innerText = data.count_like;
};
function setcommend(data) {
    // debugger;
    document.getElementById('setcommend' + data.post_id).innerText = data.count_comment;
    getcommends(id1);
};

function postcommends() {
    var object = {
        commend: $("#commendinput").val(),
        post_id: id1,
    };
    if (object.commend === "" || object.commend.trim() === "") {
        toust.error("يرجى كتابة التعليق");
        return;
    }
    call_ajax("POST", "Blogs/PostCommend", object, setcommend);

};
function GetNotification() {
    call_ajax("GET", "Blogs/GetNotification", null, note);
};
function GetMessage() {
    call_ajax("GET", "Chat/GetMessage", null, message);

};
function logut() {
   
    
    call_ajax("Get", "Account/Logout", null, afterlogout);
};
function afterlogout() {
    document.cookie = "token1= ; expires = Thu, 01 Jan 1970 00:00:00 GMT";
    call_Action("home/index")
  
}
function SetCountNotifaction(data) {
    //debugger;
    if (data.count< 0) {
        document.getElementById('GetCountNotifaction').style.visibility = "hidden";
        return;
    }
    else {
       document.getElementById('GetCountNotifaction').style.visibility = "visible";
        document.getElementById('GetCountNotifaction').innerText = data.count;
    }
};
function SetCountNot() {
    call_ajax("GET", "Blogs/GetCountNotification", null, SetCountNotifaction);
}
function SetCountMessage(data) {
    //debugger;
    if (data.count < 0) {
        document.getElementById('GetCountMessage').style.visibility = "hidden";
        return;
    }
    else {
        document.getElementById('GetCountMessage').style.visibility = "visible";
        document.getElementById('GetCountMessage').innerText = data.count;
    }
};
function visiblebtn(idbtn,iddate) {
    if ($(idbtn).is(":visible")) {
        $(idbtn).fadeOut();
    }
    else {
        $(idbtn).fadeIn();
    }
    if ($(iddate).is(":visible")) {
     
        $(iddate).fadeOut();
    }
    else {
       
        $(iddate).fadeIn();
    }
}
function setCountMessage() {
    call_ajax("GET", "Chat/GetCountMessage", null, SetCountMessage);
}

function upluad(data) {
    UploadFile(data.post_id);
}
function UploadFile(id) {
    // debugger;
    var fileUpload = $("#files").get(0);
    var files = fileUpload.files;
    var data = new FormData();
    for (var i = 0; i != files.length; i++) {
        data.append(files[i].name, files[i]);
    }
    $.ajax({
        type: "POST", url: "/Blogs/UploadFileAsync/" + id, contentType: false, processData: false,
        data: data, async: false,
        success: function (message) {
            toust.success("تم  تحميل الصور بنجاح");
        },
        error: function () {
            toust.error("عذرا حدث خطا اثناء  تحميل الصور");
        },
    });
}
function sh(s) {
    slideIndex = s + 1;
    showDivs(slideIndex);
}
function buildimage(data) {
    $('#setimage2').empty();
    $.each(data, function (i, item) {
        var rows1 = "<img class='mySlides  img-responsive' src='images/imag_post/" + item.image_path + "' style='width:100%; height:30%;'  alt='image''>";
        $('#setimage2').append(rows1);
    });
    var row2 = "<button class='w3-button w3-black w3-display-left' onclick='plusDivs(-1)'>&#10094;</button>" +
        " <button class='w3-button w3-black w3-display-right' onclick = 'plusDivs(1)' >&#10095;</button >";
    $('#setimage2').append(row2);
    sh(-1       );
}
function setimage(src) {
    $("#setimage2").attr("src", src);
}

function forgat_pass() {
    $("#login").hide();
    $("#email").fadeIn();
}
function after_write_email() {
    $("#email").hide();
    $("#confirm").fadeIn();
}
function after_confirm() {
    $("#confirm").hide();
    $("#newpass").fadeIn();
}
function after_updata_pass() {
    $("#newpass").hide();
    $("#login").fadeIn();
}

