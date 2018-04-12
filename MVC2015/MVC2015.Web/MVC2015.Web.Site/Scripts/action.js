// get operation url, like createUrl, updateUrl, deleteUrl
function getUrl(identity, key) {
    var urlIdentity = "data-" + identity;
    var urlElements = $("[data-element=" + urlIdentity + "] [data-url]");

    var urlObj = {};
    for (var i = 0; i < urlElements.length; i++) {
        var keyValue = $(urlElements[i]).attr("data-url").split("-");
        var urlName = keyValue[0];
        var url = keyValue[1];
        urlObj[urlName] = url;
    }

    if (key == null && key == undefined) {
        return urlObj;
    }
    else {
        return urlObj[key];
    }
}

// get operation url, like createUrl, updateUrl, deleteUrl
function getText(identity, key) {
    var textIdentity = "data-" + identity;
    var textElements = $("[data-element=" + textIdentity + "] [data-text]");

    var textObj = {};
    for (var i = 0; i < textElements.length; i++) {
        var keyValue = $(textElements[i]).attr("data-text").split("-");
        var textName = keyValue[0];
        var text = keyValue[1];
        textObj[textName] = text;
    }

    if (key == null && key == undefined) {
        return textObj;
    }
    else {
        return textObj[key];
    }
}


// refresh grid 
var pageIndex = 1;
function refreshGrid(pageNum, identity, refreshUrl, callback) {
    var gridIdentity = "grid-" + identity;
    var searchIdentity = "search-" + identity;
    var criearia = $("[data-element=" + searchIdentity + "] #PreSearchCriteria").val();
    if (pageNum) {
        criearia = criearia.replace(/PageNum=\d*/, "PageNum=" + pageNum);
    }
    var $gridElement = $("[data-element=" + gridIdentity + "]");
    if (refreshUrl == undefined || refreshUrl == null) {
        refreshUrl = getUrl(identity, "fresh");
    }
    loadHtml(refreshUrl, $gridElement, criearia, callback);
}

// save the pre search criteria
function savePreSearch(identity) {
    var $searchForm = $("[data-element=search-" + identity + "] form");
    var $preSearch = $searchForm.find("#PreSearchCriteria");
    $preSearch.val("");
    //$("#DXScript").val("");
    //$("#DXScript").remove();
    var preSearchVal = $searchForm.find("input,select").not("#DXScript,#DXCss,#TreeView_NSHF,#TreeView_NIHF").serialize();
    $preSearch.val(preSearchVal)
}

function resetPreSearch(identity) {
    var bool = validQueryCondition();
    if (bool) {
        savePreSearch(identity);
    }
}

function getFormModal(identity, identity2) {
    var finalIdentity = "";
    if (identity2 != null && identity2 != undefined) {
        finalIdentity = "data-element=popout-" + identity + "-" + identity2;
    } else {
        finalIdentity = "data-element=popout-" + identity;
    }
    var formModal = $("[" + finalIdentity + "] .modal");
    return formModal;
}

function getForm(identity, identity2) {
    var formModal = getFormModal(identity, identity2);
    var form = formModal.find("form");
    return form;
}

function defaultEdit(updateUrl, identity) {
    // var updateUrl = getUrl(identity, "update") + "?id=" + params["id"];
    var form = $("[data-element=popout-" + identity + "]" + " #form-modal");
    showFormModal(form, getText(identity, "edit"), updateUrl, {}, updateUrl, function (result, $modal, $body, $form) { });
}

function defaultNew(createUrl, identity) {
    //var createUrl = getUrl(identity, "create");
    var form = $("[data-element=popout-" + identity + "]" + " #form-modal");
    showFormModal(form, getText(identity, "new"), createUrl, {}, createUrl, function (result, $modal, $body, $form) { });
}

function defatulSave(form, e, identity) {
    e.preventDefault();
    post(form.attr("action"), form.serialize(), function (result) {
        //alert(result);
        var form = $("[data-element=popout-" + identity + "]" + " #form-modal");
        try {
            if (eval(result) == true) {
                var form = $("[data-element=popout-" + identity + "]" + " #form-modal");
                form.modal("hide");
                refreshGrid(1, identity);
            }
            //showMessageFromCookie();
        } catch (e) {
            $("#form-modal-body").html(result);
        }
    });
}

$('[data-element] #form-modal').on('hidden.bs.modal', function () {
    $(this).find("form").off("submit");
    $(this).find(".modal-body").html('<img class="loading" src="../../../Images/Main/loading.gif" alt="" />');
    //$("#modal-inner-form")
})

var events = ["mouseover", "mousedown", "mouseenter", "mouseleave", "mousemove", "mouseup", "mouseout",
    "keydown", "keypress", "keyup", "focus", "focusin", "focusout", "error",
    "change", "resize", "scroll", "select", "unload", "dblclick"];
function bindDataEvent() {
    for (var i = 0; i < events.length; i++) {
        var eventType = events[i];
        $("[data-element]").delegate("[data-event]", eventType, function (event) {
            var self = $(this);
            var parent = self.parents("[data-element]");
            var eventType = event.type;
            var eventName = eval("(" + self.attr("data-event") + ")")[event.type];
            if (eventName == undefined) return;

            var param = eval("(" + self.attr("data-param") + ")");
            var parentIdentity = parent.attr("data-element");
            var identity = parentIdentity.split("-")[1];
            var dealFunName = parentIdentity.replace("-", "_") + "_" + eventName;
            var paramObj = { "identity": identity, "parent": parent, "parentIdentity": parentIdentity, "params": param, "eventName": eventName, "eventType:": eventType, "event": event };
            var strDealFun = dealFunName + "(self, paramObj)";
            try {
                eval(strDealFun); //trigger event handler function
            } catch (e) {
                console.log("function " + dealFunName + " is not exist! or function inner has error!");
            }
        });
    }
}

function clear(div) {
    var $div;
    if (div.jquery != null && div.jquery != undefined) { // div is a jquery object
        $div = div;
    } else { // div is a selector
        $div = $(div);
    }
    // clear inputs
    var inputs = $div.find("input");
    for (var i = 0; i < inputs.length; i++) {
        switch (inputs[i].type) {
            case "text":
                inputs[i].value = "";
                break;
            case "checkbox":
                inputs[i].checked = false;
                break;
            case "radio":
                inputs[i].checked = false;
                break;
        }
    }
    // clear selects
    var selects = $div.find("select");
    for (var i = 0; i < selects.length; i++) {
        selects[i].selectedIndex = 0;
    }
}

//GridView的PageRowSize将要执行的方法
function SetGridViewPageRowSize(s, e, callback) {
    if (s == undefined) {
        return;
    }
    var urlMsg = s.callbackUrl.split("/");
    var urlMsgLength = urlMsg.length - 1;
    var requestUrl = "";
    for (var i = 0; i < urlMsgLength; i++) {
        requestUrl += urlMsg[i] + "/"
    }
    requestUrl += "SetGridViewPageRowSizeInCookies";
    var pagesize = aspxGetControlCollection().Get(s.name).pageRowSize;
    $.ajax({
        url: requestUrl,
        data: { gridViewName: s.name, pageRowSize: pagesize },
        type: "get",
        success: function(){},
        complete: function () {
            if (callback && callback != undefined) {
                callback(s, e);
            }
        }
    });
}

//Reset GridView PageRowSize,then refreshGrid
function resetPageRowSize(identity, gridViewName) {

    var refreshUrl = getUrl(identity, "fresh") + "/ClearPageRowSize";
    $.ajax({
        url: refreshUrl,
        data: { gridViewName: gridViewName },
        type: "get",
        success: function () {

        },
        complete: function () { refreshGrid(1, identity); }
    });
}

// valid form-search query condition
// if can not get the $form , bool = true;
function validQueryCondition() {
    var $form = $("#form-search");
    try {
        var bool = $form.validate().form();
        if (bool) {
            // remove validate when the valid success
            $form.data("validator").resetForm();
            $form.find(".validation-summary-errors")
                .addClass("validation-summary-valid")
                .removeClass("validation-summary-errors");
            $form.find(".field-validation-error")
                .addClass("field-validation-valid")
                .removeClass("field-validation-error")
                .removeData("unobtrusiveContainer")
                .find(">*")  // If we were using valmsg-replace, get the underlying error
                .removeData("unobtrusiveContainer");
        }
    }
    catch (e) {
        bool = true;
    }
    return bool;
}
$(function () {
    // save pre search when loaded
    $("[data-element^=search]").each(function () {
        var identity = $(this).attr("data-element").split("-")[1];
        savePreSearch(identity);
    })

    // clear search
    $("[data-action=clear-search]").on("click", function () {
        var clearBtn = $(this);
        var searchDiv = clearBtn.parents("[data-element^=search]");
        // clear inputs
        var inputs = searchDiv.find("input");
        for (var i = 0; i < inputs.length; i++) {
            switch (inputs[i].type) {
                case "text":
                    inputs[i].value = "";
                    break;
                case "hidden":
                    inputs[i].value = "";
                    break;
                case "checkbox":
                    inputs[i].checked = false;
                    break;
                case "radio":
                    inputs[i].checked = false;
                    break;
            }
        }
        // clear selects
        var selects = searchDiv.find("select");
        for (var i = 0; i < selects.length; i++) {
            selects[i].selectedIndex = 0;
        }

        var searchElement = clearBtn.parents("[data-element^=search]");
        var identity = searchElement.attr("data-element").split("-")[1];
        savePreSearch(identity);
        refreshGrid(1, identity);
    });

    // search
    $("[data-action=search]").on("click", function () {
        var bool = validQueryCondition();
        if (bool) {
            var searchBtn = $(this);
            var searchElement = searchBtn.parents("[data-element^=search]");
            var identity = searchElement.attr("data-element").split("-")[1];
            savePreSearch(identity);
            refreshGrid(1, identity);
        }
    });

    // show edit modal
    $("[data-element^=grid]").delegate("[data-action=edit]", "click", function () {
        var editBtn = $(this);
        if (editBtn.attr("data-event") != undefined) {
            return;
        }
        var itemId = editBtn.attr("data-item").split("-")[1];
        var grid = editBtn.parents("[data-element^=grid]");
        var identity = grid.attr("data-element").split("-")[1];
        var updateUrl = getUrl(identity, "update") + "?id=" + itemId;
        //BindSave(updateUrl, identity);
        //showModal("#form-modal", getText(identity, "edit"), updateUrl);
        defaultEdit(updateUrl, identity);
    });

    // remove
    $("[data-element^=grid]").delegate("[data-action=remove]", "click", function () {
        var removeBtn = $(this);
        if (removeBtn.attr("data-event") != undefined) {
            return;
        }
        var itemId = removeBtn.attr("data-item").split("-")[1];
        var grid = removeBtn.parents("[data-element^=grid]");
        var identity = grid.attr("data-element").split("-")[1];
        var deleteUrl = getUrl(identity, "delete") + "?id=" + itemId;
        commonDelete(getText(identity, "delete"), deleteUrl, function (message) {
            refreshGrid(1, identity);
        });
    });

    // show add modal
    $("[data-action=add]").bind("click", function () {
        var addBtn = $(this);
        if (addBtn.attr("data-event") != undefined) {
            return;
        }
        var search = addBtn.parents("[data-element^=search]");
        var identity = search.attr("data-element").split("-")[1];
        var createUrl = getUrl(identity, "create");
        defaultNew(createUrl, identity);
    });

    $("[data-element]").delegate("[data-event]", "click", function (event) {
        var self = $(this);
        var parent = self.parents("[data-element]");
        var eventType = event.type;
        var eventName = eval("(" + self.attr("data-event") + ")")[event.type];
        if (eventName == undefined) return;

        var param = eval("(" + self.attr("data-param") + ")");
        var parentIdentity = parent.attr("data-element");
        var identity = parentIdentity.split("-")[1];
        var dealFunName = parentIdentity.replace("-", "_") + "_" + eventName;
        var paramObj = { "identity": identity, "parent": parent, "parentIdentity": parentIdentity, "params": param, "eventName": eventName, "eventType:": eventType, "event": event };
        var strDealFun = dealFunName + "(self, paramObj)";

        try {
            eval(strDealFun); //trigger event handler function
        } catch (e) {
            switch (eventName) {
                case "edit":
                    var editBtn = self;
                    var itemId = editBtn.attr("data-item").split("-")[1];
                    var updateUrl = getUrl(identity, "update") + "?id=" + itemId;
                    defaultEdit(updateUrl, identity);
                    break;
                case "remove":
                    var removeBtn = self;
                    var itemId = removeBtn.attr("data-item").split("-")[1];
                    var deleteUrl = getUrl(identity, "delete") + "?id=" + itemId;
                    commonDelete(getText(identity, "delete"), deleteUrl, function (message) {
                        refreshGrid(1, identity);
                    });
                    break;
                case "add":
                    var createUrl = getUrl(identity, "create");
                    defaultNew(createUrl, identity);
                    break;
                case "cancel":
                    hideLoading();
                    break;
                default:
                    console.log("not exist eventName!");
                    break;
            }
        }
    });

    $("[data-element]").delegate("[data-event]", "submit", function (event) {
        var self = $(this);
        var parent = self.parents("[data-element]");
        var eventType = event.type;
        var eventName = eval("(" + self.attr("data-event") + ")")[event.type];
        if (eventName == undefined) return;

        var param = eval("(" + self.attr("data-param") + ")");
        var parentIdentity = parent.attr("data-element");
        var identity = parentIdentity.split("-")[1];
        var dealFunName = parentIdentity.replace("-", "_") + "_" + eventName;
        var paramObj = { "identity": identity, "parent": parent, "parentIdentity": parentIdentity, "params": param, "eventName": eventName, "eventType:": eventType, "event": event };
        var strDealFun = dealFunName + "(self, paramObj)";
        try {
            eval(strDealFun); //trigger event handler function
        } catch (e) {
            switch (eventName) {
                case "save":
                    defatulSave(self, event, identity);
                    break;
                default:
                    console.log("not exist eventName!");
                    break;
            }
        }
    });

    bindDataEvent();

})

$(function () {
    // datepicker
    //$("body").delegate("input[data-type=datepicker]", "load", function () {
    //    $(this).datepicker({
    //        showButtonPanel: true
    //    });
    //})
    $("body").delegate("input[data-type=datepicker]", "click focus", function () {
        if (!$(this).hasClass("hasDatepicker")) {
            $(this).datepicker({
                showButtonPanel: true,
                dateFormat: 'yy-mm-dd'
            });
        }
    });
});

/*
    @method autocomplete_action 
    data-action="autocomplete" : fixed
    data-target="ServiceById" : hidden element Id, where stores the key value 
    data-onselect="test" : event after selection
    data-paramId="test,test11" : params Id from DOM; you can also set fixed value params in data-url
    data-url="@Url.Action("GetUserList", "User", new { @Area = "Account", @id = 1, @name = '33' })" 
        : action url, return {"query":"","suggestions":["usertest4","usertest6"],"data":["23","25"]}
    data-needCheck="false" : default true
*/
function autocomplete_action() {
    $("[data-action=autocomplete]").each(function () {
        var that = $(this),
            func,
            needCheck = true,
            param = new Object(), // get from attr 'data-paramId'
            param2, // get from attr 'params'
            isAutoClear = false;

        if (that.attr("data-params") != undefined) {
            try {
                param2 = eval("(" + that.attr("data-params") + ")");
            }
            catch (e) {
                param2 = new Object();
            }
        }

        if (that.attr("data-isAutoClear") != undefined) {
            isAutoClear = eval(that.attr("data-isAutoClear"));
        }

        try {
            func = eval(that.attr("data-onselect"));
        } catch (e) {
        }

        var check = that.attr("data-needCheck");
        if (check != undefined && check.toLowerCase() == "false") {
            needCheck = false;
        }

        var paramID = that.attr("data-paramId");
        if (paramID != undefined) {
            var paramIDs = paramID.split(',');
            for (var p = 0; p < paramIDs.length; p++) {
                var item = $("#" + paramIDs[p]);
                if (item != undefined && item.length > 0) {
                    item.unbind("change").change(function () {
                        //aaron update 13/12/27 auto clear the textbox when its dependent param value change
                        var itemId = $(this).attr("id");
                        if (isAutoClear) {
                            var autoCompletes = $("[data-action=autocomplete]").filter("[data-paramId]");
                            for (var j = 0; j < autoCompletes.length; j++) {
                                var autoComplete = $(autoCompletes[j]);
                                var data_paramId = autoComplete.attr("data-paramId");
                                if (data_paramId != null && data_paramId.split(',').contain(itemId)) { // contain in common.js
                                    autoComplete.val("");
                                    var target = autoComplete.attr("data-target");
                                    $("#" + target).val("");
                                }
                            }
                        }

                        autocomplete_action();
                    });
                    eval("param." + paramIDs[p] + "='" + item.val() + "'");
                }
            }
        }

        // march param and param2
        for (var key in param2) {
            param[key] = param2[key];
        }

        var onselect = function (selection) {
            var target = that.attr("data-target");
            if (target != undefined) {
                if (selection != undefined) {
                    $("#" + target).val(selection.data);
                } else {
                    $("#" + target).val("");
                }
            }

            if (typeof (func) === "function") {
                func();
            }
        }

        //aaron update 13/12/19, set null to target when textbox's value = ""
        that.bind("change", function () {
            var target = that.attr("data-target");
            if (that.val() == "" && target != undefined) {
                $("#" + target).val(null);
            }
        });

        var options = {
            serviceUrl: that.attr("data-url"),
            onSelect: onselect,
            params: param,
            needCheck: needCheck,
            width: 'auto'
        };
        var result = that.autocomplete(options);
    });
}
autocomplete_action();

$(function () {
    $("#DXScript").remove();
    $("#DXScript").val("");
});

$(function () {
    //$("#form-search").bind("submit", function (e) {
    //    e.preventDefault();
    //    var self = $(this);
    //    var searchDiv = self.parents("[data-element^=search]")

    //    if (searchDiv.length == 0) {
    //        return;
    //    }

    //    var identity = searchDiv.attr("data-element");
    //    var gridIdentity = "grid-" + identity.split("-")[1];
    //    var grid = $("[data-element=" + gridIdentity + "]");
    //    loadHtml(self.attr("action"), grid, self.serialize());
    //});
    bindFormSubmit();
})

function bindFormSubmit() {
    $("#form-search").bind("submit", function (e) {
        e.preventDefault();
        var self = $(this);
        var searchDiv = self.parents("[data-element^=search]")

        if (searchDiv.length == 0) {
            return;
        }

        var identity = searchDiv.attr("data-element");
        var gridIdentity = "grid-" + identity.split("-")[1];
        var grid = $("[data-element=" + gridIdentity + "]");
        loadHtml(self.attr("action"), grid, self.serialize());
    });
}

//placeholder fix for input
(function ($) {
    $.fn.setPlaceholder = function () {
        this.filter("input").each(function () {
            var t = $(this);
            t.after(function () {
                return createPlaceholder(t);
            });
            bindEvent(t);
        })
        return this;
    };

    function createPlaceholder(t) {
        var input = "<input class='[%class%]' style='[%style%];color:gray;' type='text' />";
        input = input.replace('[%class%]', t.attr("class"))
            .replace('[%style%]', t.attr("style"));
        t.hide();
        return input;
    }

    function bindEvent(t) {
        var p = t.next(); //get placeholder item
        p.val(t.attr('placeholder'));
        if (t.val() != "") {
            t.show();
            p.hide();
        }
        p.focus(function () {
            p.hide();
            t.show().focus();
        });
        t.blur(function () {
            if (t.val() == "") {
                t.hide();
                p.show();
            }
        });
    }

    $("input[type='text'],input[type='password']").each(function (i, e) {
        var p = $(e).attr("placeholder");
        if (!!p) {
            $(e).setPlaceholder();
        }
    })
}(jQuery));