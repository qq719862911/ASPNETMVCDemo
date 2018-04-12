//Messenger init
Messenger.options = {
    extraClasses: 'messenger-fixed messenger-on-top',
    theme: 'block',//flat air
    maxMessages: 1
    //'parentLocations': ['.page', 'body'] //class, tagname
}

Messenger.showSuccess = function (msg, action) {
    Messenger().post({
        message: msg || '',
        hideAfter: 2,
        showCloseButton: true,
        actionAfterHide: action,
        type: "success" //info success error
    });
}

Messenger.showError = function (msg, action) {
    if (msg && msg.indexOf("<script") == 0) {
        $("body").append(msg);
        return;
    }
    
    Messenger().post({
        message: msg || '',
        hideAfter: 4,
        showCloseButton: true,
        actionAfterHide: action,
        type: "error" //info success error
    });
}

function gotoUrl(url) {
    return function () { location = url };
}

function commonDelete(confirmMsg, deleteUrl, callback) {
    showLoadingBackground();
    var msg;

    msg = Messenger().post({
        message: confirmMsg,
        hideAfter: 0,
        showCloseButton: true,
        actions: {
            confirm: {
                label: '确定',
                action: function () {
                    commonDeleteAction(deleteUrl, callback);
                    //hideLoading();
                    return msg.cancel();
                }
            },
            cancel: {
                label: '取消',
                action: function () {
                    hideLoading();
                    return msg.cancel();
                }
            }
        }
    });
}

//bootstrap-datetimepicker init
$(function () {
    $("body").delegate("input.form-date", "click focus", function () {
        $(this).attr("readonly", "readonly");
        $(this).datetimepicker({
            language: 'zh-CN',
            weekStart: 0,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 1,
            minView: 0,
            forceParse: 0,
            format: 'yyyy-mm-dd hh:ii',
            unselectable:'on'
        });
    });
});

$(function () {
    $("body").delegate("input.form-date-hhmm", "click focus", function () {
        $(this).attr("readonly", "readonly");
        $(this).datetimepicker({
            language: 'zh-CN',
            weekStart: 0,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 1,
            minView: 0,
            forceParse: 0,
            format: 'hh:ii',
            unselectable:'on'
        });
    });
});

function setSort(sortField) {
    var SortBy = $("#SortBy");
    var SortDirection = $("#SortDirection");
    var sortFieldOld = SortBy.val();
    SortBy.val(sortField);
    var sortDirection = SortDirection.val();
    if (sortDirection == "") {
        SortDirection.val("ASC");
    } else {
        if (sortFieldOld == sortField) {
            if (sortDirection == "ASC") {
                SortDirection.val("DESC");
            }
            else {
                SortDirection.val("ASC");
            }
        } else {
            SortDirection.val("ASC");
        }
    }
    $("#form-search").submit();
}

function loadHtml(url, target, params, callback) {
    $.ajax({
        url: url,
        dataType: "html",
        type: "POST",
        data: params,
        beforeSend: function () {
            showLoading();
        },
        success: function (result) {
            if (target.jquery != null && target.jquery != undefined) { // jquery object
                target.html(result);
            } else if (typeof target == "string") { // selector
                $(target).html(result);
            } else {
                alert("targe arg error");
            }
            if (callback) { callback(target); }
        },
        error: function (e) {
            buildError()
        },
        complete: function () {
            hideLoading();
        }
    });
}

function get(url, data, handler, dataType) {
    $.ajax({
        url: url,
        data: data,
        type: "GET",
        dataType: dataType == undefined ? "html" : dataType,
        beforeSend: function () {
            showLoading();
        },
        success: function (result) {
            handler(result)
        },
        error: function (e) {
            buildError();
        },
        complete: function () {
            hideLoading();
            $('form').removeData('validator');
            $('form').removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse('form');
        }
    });
}

function post(url, data, handler, dataType) {
    $.ajax({
        url: url,
        data: data,
        type: "POST",
        dataType: dataType == undefined ? "html" : dataType,
        beforeSend: function () {
            showLoading();
        },
        success: function (result) {
            handler(result);
        },
        error: function () {
            buildError();
        },
        complete: function () {
            hideLoading();
            $('form').removeData('validator');
            $('form').removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse('form');
        }
    });
}

function commonDeleteAction(deleteUrl, callback) {
    try {
        post(deleteUrl, {}, function (result) {
            try {
                if (result && result.indexOf("<script") == 0) {
                    $("body").append(result);
                    return;
                }
                result = eval("(" + result + ")");
                if (result.IsSuccess != null && result.IsSuccess == false) {
                    showresult("error", result.result); // operate failure
                } else if (result.IsSuccess != null && result.IsSuccess == true) {
                    //if (callback) { callback(result); }
                    showresult("info", result.result);
                }
                //else {
                //    //if (callback) { callback(result); }
                //    buildInfo(); // operate success
                //};
            }
            catch (e) {
            }
            //showMessageFromCookie();
            if (callback) { callback(result); }

            //if (eval("(" + result + ")") == true) {
            //} else { // compatibility for old message tip
            //}
        });
    } catch (e) { }
}


function showModal(modalId, title, requestUrl, callback) {
    var $modal = $(modalId);
    var $body = $modal.find(".modal-body");
    var $header = $modal.find(".modal-title");
    $header.html(title);
    $modal.modal("show");

    try {
        get(requestUrl, {}, function (result) {
            $body.html(result);
            if (callback) { callback($modal, result); }
        });
    } catch (e) { }

}

function showModalPost(modalId, title, requestUrl, data, callback) {
    var $modal = $(modalId);
    var $body = $modal.find(".modal-body");
    var $header = $modal.find(".modal-title");
    $header.html(title);
    $modal.modal("show");

    try {
        post(requestUrl, data, function (result) {
            $body.html(result);
            if (callback) { callback($modal, result); }
        });
    } catch (e) { }

}

function showFormModal(modal, title, requestUrl, requestData, postUrl, callback, requestType) {
    var $modal;
    if (modal.jquery != null || modal.jquery != undefined) { // jquery object
        $modal = modal;
    } else {
        $modal = $(modal); // selector
    }
    var $body = $modal.find(".modal-body");
    var $header = $modal.find(".modal-title");
    var $form = $modal.find("form");
    $header.html(title);
    $form.attr("action", postUrl);
    $modal.modal("show");

    if (requestType != undefined && requestType != null && requestType.toLowerCase() == "post") {
        post(requestUrl, requestData, function (result) {
            $body.html(result);
            if (callback) { callback($modal, $form, $body, result); }
        });
    } else {
        try {
            get(requestUrl, requestData, function (result) {
                $body.html(result);
                if (callback) { callback($modal, $form, $body, result); }
            });
        } catch (e) { }
    }
}

function hideModal(modal, callback) {
    var $modal;
    if (modal.jquery != null && modal.jquery != undefined) {
        $modal = modal;
    } else {
        $modal = $(modal);
    }
    var $body = $modal.find(".modal-body");
    var $form = $modal.find("form"); // is exist
    $modal.modal("hide");
    $body.html('<img class="loading" src="../../../Images/Main/loading.gif" alt="" />');
    if (callback) { callback($modal, $form); }
}

function addParamToUrl(url, key, value) {
    if (url.indexOf("?") == -1) {
        if (value == undefined)
            url += "?" + key + "=";
        else
            url += "?" + key + "=" + encodeURI(value);
    } else {
        if (value == undefined)
            url += "&" + key + "=";
        else
            url += "&" + key + "=" + encodeURI(value);
    }
    return url;
}

function getQueryString(name) {
    var reg = new RegExp('(^|&)' + name + '=([^&]*)(&|$)', 'i');
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    return "";
}