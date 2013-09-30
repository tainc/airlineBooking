(function ($) {

    $.fn.pagingSearchResult = function (dataSend, pageSearchResult) {
        return new PagingSearchResult($(this), dataSend);
    };

    function PagingSearchResult(owner, dataSend, pageSearchResult) {
        this.parent = owner;
        this.pageIndexes = $(owner).find("#page-navi").find("a");
        this.searchKeyword = dataSend;
        this.pageContent = $("#" + pageSearchResult + "");
        this.isInitialized = false;
    }

    PagingSearchResult.prototype = {
        initialize: function(controller, action) {
            var that = this;
            var keyword = that.searchKeyword;

            $(that.pageIndexes).each(function() {

                $(this).bind("click", function() {

                    var isActivatedIndex = $(this).attr("data-is-activated");
                    if (isActivatedIndex == 1) {

                        var currentActive = $("#page-navi li a.current");
                        currentActive.removeClass("current");
                        currentActive.attr("data-is-activated", 1);

                        $(this).addClass("current");
                        $(this).attr("data-is-activated", 0);

                        var pageNo = $(this).text();

                        Helpers.ajaxHelper.ajax({
                            type: "GET",
                            controller: controller,
                            action: action,
                            showMask: true,
                            dataType: 'html',
                            contentType: 'application/html; charset=utf-8',
                            data: JSON.stringify(pageContent),
                            success: function(resultPage) {
                                that.pageContent.html(resultPage);
                            }
                        });
                    }
                });

            });

            that.isInitialized = true;
        }
    };

})(jQuery, window.Ewsn = window.Ewsn || {});