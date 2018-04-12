(function ($) {

    function setValidationValues(options, ruleName, value) {
        options.rules[ruleName] = value;
        if (options.message) {
            options.messages[ruleName] = options.message;
        }
    }

    $.validator.addMethod("notlessthan", function (value, element, params) {
        //if (!this.optional(element)) {
            var otherProp = $('#' + params.otherproperty)
            //return (otherProp.val() < value);
            if (value == null || value == "" || otherProp.val() == null || otherProp.val() == "") {
                return true;
            }
        return (otherProp.val() <= value);
            
        //}
    });
    //$.validator.unobtrusive.adapters.addSingleVal("notlessthan", "otherproperty");
    $.validator.unobtrusive.adapters.add("notlessthan", ["otherproperty"], function (options) {
        options.rules["notlessthan"] = {
            otherproperty:options.params.otherproperty
        }
        if (options.message) {
        options.messages["notlessthan"] = options.message;
        }

     });

    //when the checkbox contains sting 'needVaild' in data-val-required ，add validation
    $.validator.unobtrusive.adapters.add("required", function (options) {
        if (options.element.type.toUpperCase() == "CHECKBOX" && options.element.id.indexOf("needVaild") != -1) {
            setValidationValues(options, "required", true);
        }
    });
}(jQuery));

