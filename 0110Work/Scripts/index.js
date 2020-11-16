$(function () {

    //------------scoll滑動功能--------------
    $("#starticon").click(function () {
        let target = $("#bigimg").height();
        $("html,body").animate({ //瀏覽器緣故，故html和body兩個都選取
            scrollTop: target
        },800);
    });
    //------------scoll滑動功能 End--------------

    //------------進入列表按鈕，動畫效果-------------
    $(".golist-btn").mouseenter(function () {
        $(".golist-btn-span").css("width", "100%");
    }).mouseleave(function () {
        $(".golist-btn-span").css("width", "0%");
        });
    //------------進入列表按鈕，動畫效果 End-------------

    //------------輪播(flexslider套件)-----------
    $(document).ready(function () {
        $('.flexslider').flexslider({
            animation: "fade"
        });
    });
    //------------輪播(flexslider套件) End-----------

});