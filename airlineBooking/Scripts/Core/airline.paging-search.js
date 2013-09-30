(function ($) {
    // Define function for paging search result
    // fnPerparingData : The callback return the data send to controller action
    // searchingResultContianer : html element that will be displayed contents get from servers searching
    $.fn.pagingSearchResult = function (fnPrepareParamsData, searchingResultContianerId) {
        return new pagingSearchResult($(this), fnPrepareParamsData, searchingResultContianerId);
    };

    function pagingSearchResult(owner, fnPrepareParamsData, searchingResultContianerId) {
        this.parent = owner;
        this.listPagedListContainers = $(owner).find(".pagedListContainer");
        this.fnPrepareParamsData = fnPrepareParamsData;
        this.resultContainerId = searchingResultContianerId;
        this.resultContainer = $("#" + searchingResultContianerId);
        this.isInitialized = false;
    }

    pagingSearchResult.prototype = {
        //Init search action
        // controller : controller name
        // action : action name in controller will be invoke
        initialize: function (controller, action) {
            var that = this;

            //Find all paged list number container
            $(that.listPagedListContainers).each(function () {
                var pageNumberLinks = $(this).find("a");

                //Binding click event for each page link number
                pageNumberLinks.each(function () {

                    $(this).bind("click", function () {

                        var pageNumber = $(this).attr("data-targetPageNumber");
                        var modelParams = that.fnPrepareParamsData();

                        var dataToSend;
                        if (modelParams == null) {
                            dataToSend = { pageIndex: pageNumber };
                        } else {
                            dataToSend = { model: modelParams, pageIndex: pageNumber };
                        }

                        Helpers.ajaxHelper.ajax({
                            type: "POST",
                            controller: controller,
                            action: action,
                            showMask: true,
                            dataType: 'html',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify(dataToSend),
                            success: function (resultContent) {
                                that.resultContainer.html(resultContent);
                                var containerSet = that.resultContainer.pagingSearchResult(that.fnPrepareParamsData, that.resultContainerId);
                                containerSet.initialize(controller, action);
                            }
                        });
                    });

                });

            });

            //Complete intialize for paging action
            that.isInitialized = true;
        }
    };

})(jQuery, window.Nsf = window.Nsf || {});