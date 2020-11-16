$(function () {

    //文章圖片的style清空
    $(".article-content img").attr("style", "");

    //--------依視窗大小，調整padding左右(較符合手機觀看)----------
    $(window).resize(function () {
        change();
    });

    function change() {
        let w = $(window).width();

        if (w < 576) {
            $(".article").css("padding-right", "1rem");
            $(".article").css("padding-left", "1rem");
            $("#btnUp").css("display", "none"); //隱藏置頂按鈕
        }
        else {
            $(".article").css("padding-right", "");
            $(".article").css("padding-left", "");
        };
    };

    change();
    //--------依視窗大小，調整padding左右(較符合手機觀看) End----------

    //-----------------滑動視窗，置頂按鈕會隨著移動------------------------
    let target1 = $("#bigimg").height() - 1;
    $(window).scroll(function () {
        if ($(window).width() > 576) {
            if ($("html,body").scrollTop() >= target1) {
                $("#btnUp").css("display", "");
                goTop(800);
            }
            else {
                $("#btnUp").css("display", "none");
            }
        }
    });

    goTop(1); //快到看不見

    function goTop(speed) {
        let mywinhei = $(window).height(); //當前頁面高度
        let myscorlltop = $(window).scrollTop(); //卷軸上方高度

        mywinhei = $(window).height(); //當前頁面高度
        myscorlltop = $(window).scrollTop(); //卷軸上方高度

        $("#btnUp").stop().animate({ //要加stop，不然會疊加
            top: mywinhei + myscorlltop - $("#btnUp").outerHeight() - 30,
            left: (($(window).width() - $(".article").outerWidth()) / 2) + $(".article").outerWidth()
        }, speed);
    };
    //-----------------滑動視窗，置頂按鈕會隨著移動 End------------------------

    //------------------點擊置頂按鈕，滑動至頂端----------------
    $("#btnUp").click(function () {
        $("html,body").animate({
            scrollTop:0
        },800)
    });
    //------------------點擊置頂按鈕，滑動至頂端 End----------------

});