//改使用css的media屬性
//-------------依視窗大小，調換Navbar和banner-----------
//$(window).resize(function () {
//    changeNav();
//});

//function changeNav() {

//    //依視窗大小，調換Navbar
//    let w = $(window).width();
//    if (w < 992) {
//        $("#Navbar-lg").css("display", "none");
//        $("#Navbar-xs").css("display", "");
//    }
//    else {
//        $("#Navbar-lg").css("display", "");
//        $("#Navbar-xs").css("display", "none");
//    };

//    依視窗大小，調換banner
//    if (w < 768) {
//        $("#banner-lg").css("display", "none");
//        $("#banner-xs").css("display", "");
//    }
//    else {
//        $("#banner-lg").css("display", "");
//        $("#banner-xs").css("display", "none");
//    };
//};

//changeNav();
//-------------依視窗大小，調換Navbar和banner End-----------

//-------------頁面滾動超過banner，就隱藏Navbar-------------
let target; 
$(window).scroll(function () {
    target = $("#bigimg").height() - 1; //-1比較剛好(視窗大小可能改變，故每次都再抓一次banner高)
    if ($("html,body").scrollTop() >= target) {
        $("#myNavbar").fadeOut();
    }
    else {
        $("#myNavbar").fadeIn();
    }
});
//-------------頁面滾動超過banner，就隱藏Navbar End-------------

//------------下拉式選單------------
$(".dropdownlist").hover(function () {
    let ul = $(this).children("ul");
    ul.stop();
    ul.fadeToggle();
});
//------------下拉式選單 End------------