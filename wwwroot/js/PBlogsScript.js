
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
                mouseevent("default");
                var from = respons.indexOf("<!-- CUT FROM HERE -->");
                document.body.innerHTML = respons.substring(from, respons.length - 16);
                window.scrollTo(0, 0);
                var mydiv = document.getElementById("mydiv");
                var scripts = mydiv.getElementsByTagName("script");
               
                for (var i = 0; i < scripts.length; i++) {
                   
                    eval(scripts[i].innerText);
                }
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
connection.start();
function datw(data) {
    if (data.length === 0) {
        toust.error("لا توجد منشورات");
        return;
    }
    $.each(data, function (i, item) {
        console.log(data);
        var islike = '#fff';
        if (item.isliked === true) {
            islike = '#57cbcc';
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
            "<h2><a onclick=\"call_Action(\'Blogs/profileUser/" + item.user_id +"')\"> "+ item.userName + "</a></h2>" +
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
            " <a class='btn2 btn  ' style=' float:left;  '>تواصل <i class='icon tf-ion-ios-chatbubble-outline'></i></a>" +
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
        if (item.isliked === true) {
            islike = '#57cbcc';
        }
        var str = item.image;
        var res = str.slice(43, str.length);
        var rows = " <article class='wd   wow fadeInUp' data-wow-duration='500ms' data-wow-delay='400ms' >" +
            "<div  class='post-block' style='border-radius: 15px;'>" +
            " <div class='content' >" +
            " <div class='price-title text-right' style='padding:0px;' >" +
            "<h2><a >" + item.userName + "</a></h2>" +
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
            " <div class='filtr-item '>" +
            " <div class='portfolio-block' style='margin-bottom:0;'>" +
            "   <img class='' src='" + res + "' style='width:100%; height:30%;' alt=''>" +
            "     <div class='caption'>" +
            " <a class='search-icon image-popup'  onclick =\"setimageinsidemodel(\'" + res + "')\" data-toggle='modal' data-target='#image'  data-effect='mfp-with-zoom' data-scroll>" +
            "   <i class='tf-ion-android-search'></i>" +
            "</a>" +
            " <h4>تكبير الصورة</h4>" +
            " </div>" +
            " </div>" +
            "</div>" +
            "<div class='divdown'>" +
            "  <span data-toggle='modal' id='like_count'  onclick='getlikes(" + item.post_id + ")' data-target='#likemodel' style='float: right; cursor:pointer;'><a class='value1' id='setlike" + item.post_id + "'>" + item.count_like + "</a> اعجابات </span>" +
            " <span data-toggle='modal' id='command_count'  onclick='getcommends(" + item.post_id + ")' data-target='#commentmodel' style=' cursor:pointer;'> <a class='value1' id='setcommend" + item.post_id + "'>" + item.count_comment + "</a> تعليقات </span>" +
            " </div>" +
            "   <button type='button' id='delete_btn' onclick='delete_btn(" + item.post_id + ")' class='btn btn-transparent'  style=' border:0px; border-top: 1px solid #4e595f; border-bottom: 1px solid #4e595f; margin-bottom: 10px;width:50%; '> <i class='tf-ion-ios - trash-outline '></i>  حذف المنشور   </button>" +
            " <button type='button' id='update_btn' onclick='update_btn(" + item.post_id + ")' class='btn btn-transparent' style = 'border:0px; border-top: 1px solid #4e595f; border-bottom: 1px solid #4e595f; margin-bottom: 10px; float: right; width: 50%;' > <i class='tf-pencil2'></i> تعديل المنشور  </button >"
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
        "<input placeholder='اكتب تعليقك'  class='bo form-control text-right' autocomplete='off' name='commend' id='commend' />" +
        " </div>" +
        "</div >" +
        "<div class='col-lg-2 col-md-2 col-sm-2 col-xs-2'>" +
        "<button type='button'   onclick='postcommends()'  class='btn btn-transparent' >  نشر </button>" +
        " </div>" +
        "</div >";
    $('#commands').append(row);
};
function note(data) {
    $('#note').empty();
    console.log(data);
    if (data.length === 0) {
        toust.error("لا توجد اشعارات");
        return;
    }
    $.each(data, function (i, item) {
        
        var color = "#292F36";
       
        var seen = item.seen;
        if (seen) {
            color = "#353b43";
        }
        if (item.type_note === 1) {
            var rows = "<div  class='price-title text-right' style='background:" + color + ";' >" + "<div class='row' style='margin-right: 0px;'>" +
                "<div class='form-group col-md-3 col-sm-3 col-xs-3' > <a onclick=\"call_Action(\'Blogs/Notifecation/" + item.post_id +"',1)\"  class='btn btn-transparent' >عرض</a>" + "</div> "
                + "<div class='form-group col-md-9 col-sm-9 col-xs-9' >" +
                "<p>" + item.userName + " تم الاعجاب على منشورك من قبل </p>" +
                +" <span style = 'float:none;' >" + item.date + "/ تاريخ الاشعار  </span>"
                + " </div>" +
                "</div>" + "</div>";
        }
        else {
            var rows = "<div  class='price-title text-right' style='background:" + color + ";'>" + "<div class='row' style='margin-right: 0px;'>" +
                "<div class='form-group col-md-3 col-sm-3 col-xs-3' > <a  onclick=\"call_Action(\'Blogs/Notifecation/" + item.post_id +"',1)\" class='btn btn-transparent' >عرض</a>" + "</div> "
                + "<div class='form-group col-md-9 col-sm-9 col-xs-9' >" +
                "<p>" + item.userName + " تم التعليق على منشورك من قبل </p>" +
                +" <span style = 'float:none;' >" + item.date + "/ تاريخ الاشعار  </span>"
                + " </div>" +
                "</div>" + "</div>";
        }
        $('#note').append(rows);
        call_ajax("POST", "Blogs/SetNotification", null, null);
    });
    call_ajax("GET", "Blogs/GetCountNotification", null, SetCountNotifaction);
};
function userinfo(data) {
   
    $('#info').empty();
    $.each(data, function (i, item) {
        var rows = "  <div class='price-title text-right' style='background-color:#171a1d'>" +
            "  <h3> " + item.userName + " / الأسم </h3 >" +
            "  <br />" +
            "<div class='con-info clearfix text-right' style='float:none;'>" +
            "       <span style='float:none;'>" + item.email + " / ايميل </span>" +
            " <i class='tf-ion-ios-email-outline' style='float:none;'></i>" +
            "  </div>" +
            "  <div class='con-info clearfix text-right' style='float:none;'>" +
            "  <span style='float:none;'>" + item.counts + "/ عدد المنشورات  </span>" +
            "   </div>" +
            "  </div >";
        $('#info').append(rows);
    });
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

    state.querySet = data;
    var dat = pagination(state.querySet, state.page, state.rows);
    datw(dat.querySet);
    pageButtons(dat.pages, 1)
}
function buildTableprofile(data) {

    state.querySet = data;
    var dat = pagination(state.querySet, state.page, state.rows);
    datprofile(dat.querySet);
    pageButtons(dat.pages, 2);
}
function buildTableprofileuser(data) {
   
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
        commend: $("#commend").val(),
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
function logut() {
   
    document.cookie = "token1= ; expires = Thu, 01 Jan 1970 00:00:00 GMT"
    call_Action("home/index")
  
    call_ajax("Get", "Account/Logout", null, null);
 
};
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