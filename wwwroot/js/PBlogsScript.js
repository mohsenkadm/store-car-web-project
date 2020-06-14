const baseUrl = "https://localhost:44385/";
var userToken = "-1";
function call_ajax_json(method, url, param, object, call_back_func) {
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
        toast.error('حدث خطأ عند الأتصال');
      }
    });
  }
  
function call_ajax(method, url, object, call_back_func) {
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
        toast.error('حدث خطأ عند الأتصال');
      }
    });
}
function mouseevent(type) {
    $("body").css("cursor", type);
    //type =progress ,default
}
