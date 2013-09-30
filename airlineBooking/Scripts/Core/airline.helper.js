(function ($) {
    UtilityClass = function () {
    };

    UtilityClass.prototype = {
        init: UtilityClass,

        inputError: function (arrayElementName) {
            for (var index = 0; index < arrayElementName.length; index++) {
                var name = arrayElementName[index].split("Error", 1);
                if ($("#" + arrayElementName[index] + "").text() != "") {
                    $("#" + name.toString() + "").addClass("error");
                }
            }
        },

        addErrorInInput: function (arrayElementName) {
            for (var index = 0; index < arrayElementName.length; index++) {
                var name = arrayElementName[index].split("Error", 1);
                $("#" + name.toString() + "").addClass("error");
            }
        },

        checkHiraganaOneInput: function (input) {
            if (input.match(/^[\u3040-\u309F]+$/)) {
                return true;
            } else {
                return false;
            }
        },

        checkHiragana: function (input, errorMessage , errorMessage2, arrayElementId) {
            if (input.match(/^[\u3040-\u309F]+$/)) {
                return true;
            } else {
                for (var index = 0; index < arrayElementId.length; index++) {
                    if (index == 0) {
                        if ($("#" + arrayElementId[index]).val().match(/^[\u3040-\u309F]+$/)) {
                        } else {
                            $("#" + arrayElementId[index].toString() + "").attr("title", errorMessage);
                            $("#" + arrayElementId[index].toString() + "").addClass("error");
                        }
                    } else {
                        if ($("#" + arrayElementId[index]).val().match(/^[\u3040-\u309F]+$/)) {
                        } else {
                            $("#" + arrayElementId[index].toString() + "").attr("title", errorMessage2);
                            $("#" + arrayElementId[index].toString() + "").addClass("error");
                        }
                        
                    }
                }
                return false;
            }
        },

        addClassErrorInInput: function (arrayElementName) {
            for (var index = 0; index < arrayElementName.length; index++) {
                $("#" + arrayElementName[index].toString() + "").addClass("error");
            }
        },

        getURLParameter: function (name) {
            var result = decodeURI(
                (RegExp(name + '=' + '(.+?)(&|$)').exec(location.search) || [, null])[1]
            );
            if (result == "null")
                return null;
            return result;
        },

        addErrorInToolTip: function (arrayElementName, errorMessage) {
            for (var index = 0; index < arrayElementName.length; index++) {
                $("#" + arrayElementName[index].toString() + "").attr("title", errorMessage);
                $("#" + arrayElementName[index].toString() + "").addClass("error");
            }
        },

        restoreTitle: function (arrayElementName) {
            for (var index = 0; index < arrayElementName.length; index++) {
                $("#" + arrayElementName[index].toString() + "").attr("title", "");
                $("#" + arrayElementName[index].toString() + "").removeClass("error");
            }
        },

        removeClassError: function (arrayRemoveClassError) {
            for (var index = 0; index < arrayRemoveClassError.length; index++) {
                var name = arrayRemoveClassError[index].split("Error", 1);
                $("#" + name.toString() + "").removeClass("error");
                $("#" + name.toString() + "").attr("title", "");
            }
        },

        isError: function (arrayElementName) {
            var count = 0;
            for (var index = 0; index < arrayElementName.length; index++) {
                if ($.trim($("#" + arrayElementName[index] + "").text()) != "") {
                    count++;
                }
            }
            if (count > 0) {
                return false;
            }
            return true;
        },

        addToolTip: function (arrayElementName) {
            for (var index = 0; index < arrayElementName.length; index++) {
                var nameError = arrayElementName[index];
                var name = arrayElementName[index].split("Error", 1);
                $("#" + name.toString() + "").attr("title", $("#" + nameError.toString() + "").text());
            }
        },

        isNumberFloatInput: function (elementName, errorMessageNumber, errorMessageLessThan) {
            var countError = 0;
            if ($("#" + elementName + "").val() != "") {
                if (parseInt($("#" + elementName + "").val()) < 0) {
                    $("#" + elementName + "").attr("title", errorMessageLessThan);
                    $("#" + elementName.toString() + "").addClass("error");
                    countError++;
                }
                if (!($.isNumeric($("#" + elementName + "").val()))) {
                    $("#" + elementName + "").attr("title", errorMessageNumber);
                    $("#" + elementName + "").addClass("error");
                    countError++;
                }
                
            }
            if (countError == 0)
                return true;
            return false;
        },

        isNumberFloat: function (arrayElementName, errorMessageNumber, errorMessageLessThan) {
            var countError = 0;
            for (var index = 0; index < arrayElementName.length; index++) {
                if ($("#" + arrayElementName[index] + "").val() != "") {

                    if (!($.isNumeric($("#" + arrayElementName[index] + "").val()))) {
                        $("#" + arrayElementName[index] + "").attr("title", errorMessageNumber);
                        $("#" + arrayElementName[index].toString() + "").addClass("error");
                        countError++;
                    }

                    if (parseInt($("#" + arrayElementName[index] + "").val()) < 0) {

                        $("#" + arrayElementName[index] + "").attr("title", errorMessageLessThan);
                        $("#" + arrayElementName[index].toString() + "").addClass("error");
                        countError++;
                    }
                }
            }
            if (countError == 0)
                return true;
            return false;
        },

        inputNumber: function (arrayElementName) {
            for (var index = 0; index < arrayElementName.length; index++) {
                $("#" + arrayElementName[index] + "").keydown(function (event) {
                    // Allow: backspace, delete, tab and escape
                    if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 ||
                        // Allow: Ctrl+A
                        (event.keyCode == 65 && event.ctrlKey === true) ||
                        // Allow: home, end, left, right
                        (event.keyCode >= 35 && event.keyCode <= 39)) {
                        // let it happen, don't do anything
                        return;
                    }
                    else {
                        // Ensure that it is a number and stop the keypress
                        if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                            event.preventDefault();
                        }
                    }
                });
            }
        },

        isNull: function (element) {
            return (typeof element === "undefined" || element === null);
        },

        showMessage: function (message, fcallback, value) {
            var result = confirm(message);

            if (result == true) {
                fcallback(value);
            } else {

            }
        },

        isNullOrEmptyInArray: function (arrayError, arrayMessageError) {
            var counter = 0;
            for (var index = 0; index < arrayError.length; index++) {
                if ($.trim($("#" + arrayError[index] + "").val()) == "") {
                    $("#" + arrayError[index] + "").attr("title", arrayMessageError);
                    $("#" + arrayError[index] + "").addClass("error");
                    console.log($("#" + arrayError[index] + "").val());
                    counter++;
                }
            }
            if (counter > 0) {
                return false;
            } else {
                return true;
            }
        },

        isNullOrEmpty: function (element) {
            return (Helpers.isNull(element) || element === "");
        },

        isNumber: function (n) {
            if ($.trim(n) != "") {
                var temp = (!/[^0-9]/g.test(n));
                if (!temp) {

                    return false;
                }
            }
            return true;
        },
        
        isNumberInArrayZipCode: function (arrayElement) {
            var countError = 0;
            for (var index = 0; index < arrayElement.length; index++) {
                if ($.trim($("#" + arrayElement[index] + "").val()) != "") {
                    var temp = (!/[^0-9]/g.test($("#" + arrayElement[index] + "").val()));
                    if (!temp) {
                        countError++;
                    }
                }
            }
            if (countError >= 1) {
                return false;
            }
            return true;
        },

        isNumberInArray: function (arrayElement, errorMessage) {
            var countError = 0;
            for (var index = 0; index < arrayElement.length; index++) {
                if ($.trim($("#" + arrayElement[index] + "").val()) != "") {
                    var temp = (!/[^0-9]/g.test($("#" + arrayElement[index] + "").val()));
                    if (!temp) {
                        $("#" + arrayElement[index].toString() + "").attr("title", errorMessage);
                        $("#" + arrayElement[index].toString() + "").addClass("error");
                        countError++;
                    }
                }
            }
            if (countError >= 1) {
                return false;
            }
            return true;
        },

        isValidDate: function (dtYear, dtMonth, dtDay) {
            var arrayDate = [dtYear, dtMonth, dtDay];
            for (var i = 0; i < arrayDate.length; i++) {
                if ($.trim(arrayDate[i]) != "") {
                    var temp = (!/[^0-9]/g.test(arrayDate[i]));
                    if (!temp) {
                        return false;
                    }
                }
            }

            if (dtYear != "" && dtMonth != "" && dtDay != "") {
                if (dtMonth < 1 || dtMonth > 12)
                    return false;
                else if (dtDay < 1 || dtDay > 31)
                    return false;
                else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
                    return false;
                else if (dtMonth == 2) {
                    var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
                    if (dtDay > 29 || (dtDay == 29 && !isleap))
                        return false;
                }
            }
            return true;

        },

        isValidEmailJapanese: function (emailValue) {
            if (!/[^A-Za-z0-9@A-Za-z.A-Za-z]/g.test($.trim(emailValue))) {
                return false;
            }
            return true;
        },

        isValidJapanese: function (textJapanese) {
            //if (!/[^A-Za-z0-9]/g.test(textJapanese.trim())) {
            //    return false;
            //}
            return true;
        },
        
        testMailvalid: function (mailValue) {
            if (!Helpers.isNull(mailValue)) {
                if ($.trim(mailValue) != "") {
                    var temp = (/^[a-zA-Z0-9!-/:-@¥[-`{-~]+$/g.test(mailValue));
                    if (!temp) {
                        return false;
                    }
                }
            }
            return true;
        },

        isValidPasswordJapanese: function (passwordValue) {
            if (!Helpers.isNull(passwordValue)) {
                if ($.trim(passwordValue) != "") {
                    var temp = (/^[a-zA-Z0-9!-/:-@¥[-`{-~]+$/g.test(passwordValue));
                    if (!temp) {
                        return false;
                    }
                }
            }
            return true;
        },

        //check validate in spec
        isValidPassword: function (codeValue, currenPasswordValue, newPasswordValue, confirmPasswordValue) {
            var textError = [];
            if (currenPasswordValue == null) {
                textError.push("XXF001 現在のパスワードは必須です");
            } else if (newPasswordValue == null) {
                textError.push("XXF001 新パスワードは必須です");
            } else if (newPasswordValue.length < 5) {
                textError.push("XXF037 パスワードは最低５文字以上指定してください");
            } else if (confirmPasswordValue == null) {
                textError.push("XXF001 新パスワード（確認用）は必須です");
            } else if (confirmPasswordValue.length < 5) {
                textError.push("XXF037 パスワードは最低５文字以上指定してください");
            } else if (newPasswordValue !== confirmPasswordValue) {
                textError.push("XXF038 パスワード確認の内容が新パスワードと一致していません");
            } else if (newPasswordValue === currenPasswordValue || codeValue === newPasswordValue) {
                textError.push("XXF052 新パスワードには初回パスワードまたは現在のパスワードと同じパスワードは入力できません");
            }
            return textError;
        },

        convertDate: function (dateValue) {
            var fromFormat = 'dd/MM/yyyy',
                toFormat = 'ddd, MMM dd yyyy';
        },

        //extend from an object
        extend: function (protobj, skipBaseConstructor) {
            protobj = protobj || {};
            var subClass = null;
            var baseConstructor = this;
            if (typeof (baseConstructor) != "function") {
                baseConstructor = this.init;
            }

            if (protobj.init) {
                subClass = function () {
                    if (!skipBaseConstructor) {
                        baseConstructor.apply(this, arguments);
                    }
                    protobj.init.apply(this, arguments);
                };
            } else {
                subClass = function () {
                    if (!skipBaseConstructor) {
                        baseConstructor.apply(this, arguments);
                    }
                };
                protobj.init = baseConstructor;
            }
            subClass.prototype = subClass.prototype || {};
            $.extend(true, subClass.prototype, this.prototype, protobj);
            subClass.extend = this.extend;
            return subClass;
        }
    };

    Helpers = new UtilityClass();

    //Core functions
    Helpers.AjaxCore = Helpers.extend({
        JSON: 'json',
        HTML: 'html',
        POST: 'POST',
        GET: 'GET',
        SLASH: '/',
        AND: '&',
        QUESTION_MARK: '?',

        //called when an ajax request is completed
        ajaxComplete: function () {
            if (this.mask) {
                $.unblockUI();
                this.mask = null;
            }
        },

        getRootUrl: function () {
            var rootUrl = this.SLASH;

            return rootUrl;
        },

        //get url from controller, action
        buildUrl: function (controller, action) {
            var rootUrl = this.getRootUrl();
            var url = rootUrl + controller + this.SLASH + action;
            return url;
        },

        checkError: function (response, jqXhr) {
            var result = null;

            if (typeof (response) == "string"
                && jqXhr.getResponseHeader("Content-Type") !== null
                && jqXhr.getResponseHeader("Content-Type") !== undefined
                && jqXhr.getResponseHeader("Content-Type").indexOf('application/json') >= 0) {
                result = eval('(' + response + ')');
            } else {
                result = response;
            }

            if (result) {
                if (Helpers.isNullOrEmpty(result.ErrorMessage) == false) {
                    this.showErrorMessage(result.ErrorMessage);
                    return true;
                }

                if (Helpers.isNullOrEmpty(result.redirect) == false) {
                    window.location = result.redirect;
                    return true;
                }

                return false;
            }

            return false;
        },



        showErrorMessage: function (errorMessage) {
            alert(errorMessage);
        },

        //send ajax request
        ajax: function (options) {
            var controller = options.controller;

            var pathName = window.location.pathname.substring(1, window.location.pathname.lastIndexOf('/'));

            var splitTemp = pathName.split("/");
            if (splitTemp[0] == controller) {
                pathName = null;
            } else if (pathName == "/") {
                pathName = null;
            }
            else {
                for (var index = 0; index < splitTemp.length; index++) {
                    if (splitTemp[index] == controller) {
                        pathName = window.location.pathname.substring(1, window.location.pathname.lastIndexOf('/') - controller.length - 1);
                    }
                }
            }

            var url = options.url,
                dataType = (typeof options.dataType === 'undefined') ? this.JSON : options.dataType,
                contentType = (typeof options.contentType === 'undefined') ? "application/json" : options.contentType,
                async = (typeof options.async === 'undefined') ? true : options.async,
                cache = (typeof options.cache === 'undefined') ? false : options.cache,
                traditional = options.traditional == undefined ? false : options.traditional;


            if (!url) {
                if (pathName == null) {
                    url = this.buildUrl(options.controller, options.action);
                } else {
                    url = this.buildUrl(pathName + "/" + options.controller, options.action);
                }

            }

            if (options.showMask) {
                this.mask = {
                    css: { backgroundColor: 'transparent', border: 'none', zIndex: 10002 },
                    message: '<div class="loading-ajax" />'
                };
                $.blockUI(this.mask);
            }

            $.ajax({
                url: url,
                data: options.data,
                dataType: dataType,
                type: options.type,
                cache: cache,
                async: async,
                traditional: traditional,
                context: this,
                contentType: contentType,
                success: function (result, textStatus, jqXhr) {
                    var isSuccessful = true;

                    if (result) {
                        if (this.checkError(result, jqXhr)) {
                            isSuccessful = false;
                        }
                    }

                    if (isSuccessful) {
                        try {
                            options.success.call(this, result);
                        } catch (error) {
                            this.showErrorMessage(error, error);
                        }
                    }

                    this.ajaxComplete();
                },
                error: function (jqXhr, textStatus, errorThrown) {
                    this.checkError(jqXhr.responseText, jqXhr);
                    this.ajaxComplete();
                }
            });

            return false;
        },

        //send ajax request with data in JSON format and GET verb
        getJson: function (options) {
            var defaultOptions = { dataType: this.JSON, type: this.GET };
            var ajaxOptions = $.extend({}, defaultOptions, options);
            this.ajax(ajaxOptions);
        },

        //send ajax request with data in JSON format and POST verb
        postJson: function (options) {
            var defaultOptions = { dataType: this.JSON, type: this.POST };
            var ajaxOptions = $.extend({}, defaultOptions, options);
            this.ajax(ajaxOptions);
        },

        //send ajax request with data in HTML format and GET verb
        getHtml: function (options) {
            var defaultOptions = { dataType: this.HTML, type: this.GET };
            var ajaxOptions = $.extend({}, defaultOptions, options);
            this.ajax(ajaxOptions);
        },

        //send ajax request with data in HTML format and POST verb
        postHtml: function (options) {
            var defaultOptions = { dataType: this.HTML, type: this.POST };
            var ajaxOptions = $.extend({}, defaultOptions, options);
            this.ajax(ajaxOptions);
        },

        //navigate to a url built from controller, action
        redirectToAction: function (options) {
            var url = this.buildUrl(options.controller, options.action);
            if (options.params) {
                if (options.params.length > 1)
                    options.params.unshift(this.QUESTION_MARK);
                url = url + this.SLASH + options.params.join(this.AND);
            }
            window.location = url;
        }
    });

    Helpers.ajaxHelper = new Helpers.AjaxCore();

})(jQuery, window.Ewsn = window.Ewsn || {});
$(function () {
    $("#content").css({ "height": $(window).height() - $('#header').height() });
});
$(window).resize(function () {
    $("#content").css({ "height": $(window).height() - $('#header').height() });
});
$(document).unbind('keydown').bind('keydown', function (event) {
    var doPrevent = false;
    if (event.keyCode === 8) {
        var d = event.srcElement || event.target;
        if ((d.tagName.toUpperCase() === 'INPUT' && (d.type.toUpperCase() === 'TEXT' || d.type.toUpperCase() === 'PASSWORD' || d.type.toUpperCase() === 'FILE'))
             || d.tagName.toUpperCase() === 'TEXTAREA') {
            doPrevent = d.readOnly || d.disabled;
        }
        else {
            doPrevent = true;
        }
    }

    if (doPrevent) {
        event.preventDefault();
    }
});