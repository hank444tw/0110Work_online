$(function () {

    //點擊下滑按鈕，文字框展開效果，icon翻轉
    $(".fa-arrow-circle-down").click(function () {

        if ($(this).css("transform") == "matrix(-1, 0, 0, -1, 0, 0)") {
            $(this).css("transform", "scale(1)");
        }
        else {
            $(this).css("transform", "scale(-1)");
        }

        $(this)
            .parent()
            .next()
            .slideToggle();
    });

});

