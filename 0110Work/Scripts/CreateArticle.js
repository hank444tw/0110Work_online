//必須使用javascript語法封裝，若使用jquery封裝，編輯器height屬性會失效
(function () {

    //-----------ckeditor編輯器------------
    CKEDITOR.replace("TextArea1", {
        filebrowserImageUploadUrl: "/Admin/UploadEditorImage", //上傳至伺服器連結
        height: "1000px" //高度
    });
    //-----------ckeditor編輯器 End------------

    //------------驗證封面圖片副檔名並預覽------------
    $("#image").change(function () {
        let file = this;
        let accept = new Array(".jpg", ".png");
        let fileExt = file.value;
        fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
        if (accept.indexOf(fileExt) == -1) {
            file.value = null;
            alert("上傳圖片格式錯誤");
            return false;
        }

        //---------------預覽-----------------
        let reader = new FileReader();
        reader.onload = function (e) {
            $('#coverImage').attr('src', e.target.result).css("display", "");
        }
        reader.readAsDataURL(file.files[0]);
        return true;
        //---------------預覽 End-----------------
    });
    //------------驗證封面圖片副檔名並預覽 End------------

    //------------驗證表單--------------
    $("#submit").click(function () {
        //ckeditor.instances.textarea1.updateelement(); //送出表單，把內容更新回編輯器
        let content = CKEDITOR.instances.TextArea1.document.getBody().getText(); //取得純文本
        $("#truecontent").val(content);

        if ($.trim(content) == "") { //沒有內容，直接return true
            return true;
        }
        
        else if (content.indexOf("\r") != -1 || content.indexOf("\n") != -1 || content.indexOf("amp;") != -1
            || content.indexOf("/images/temporaryimg/") != -1 || content.indexOf("\\") != -1
            || content.indexOf("<img alt=") != -1) {
            alert("還敢亂打\n想害死我啊!!!");
            return false;
        };
        $("#aniCircle").show(); //顯示Loading轉圈圈
        return true;
    });
    //------------驗證表單 End--------------

})();

//////////////////////備用//////////////////////
//(function () {
//    let a = 1;
//    //-----------ckeditor編輯器------------
//    CKEDITOR.replace("TextArea1", {
//        filebrowserImageUploadUrl: "/Admin/UploadEditorImage", //上傳至伺服器連結
//        height: "1000px" //高度
//    });
//    //-----------ckeditor編輯器 End------------

//    //------------驗證封面圖片副檔名並預覽------------
//    $("#image").change(function () {
//        let file = this;
//        let accept = new Array(".jpg", ".png");
//        let fileExt = file.value;
//        fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
//        if (accept.indexOf(fileExt) == -1) {
//            file.value = null;
//            alert("上傳圖片格式錯誤");
//            return false;
//        }

//        //---------------預覽-----------------
//        let reader = new FileReader();
//        reader.onload = function (e) {
//            $('#coverImage').attr('src', e.target.result).css("display", "");
//        }
//        reader.readAsDataURL(file.files[0]);
//        return true;
//        //---------------預覽 End-----------------
//    });
//    //------------驗證封面圖片副檔名並預覽 End------------

//    //------------驗證表單--------------
//    $("#submit").click(function () {
//        //ckeditor.instances.textarea1.updateelement(); //送出表單，把內容更新回編輯器
//        let content = CKEDITOR.instances.TextArea1.document.getBody().getText(); //取得純文本
//        $("#truecontent").val(content);

//        if ($.trim(content) == "") { //沒有內容，直接return true
//            return true;
//        }

//        else if (content.indexOf("\r") != -1 || content.indexOf("\n") != -1 || content.indexOf("amp;") != -1
//            || content.indexOf("/images/temporaryimg/") != -1 || content.indexOf("\\") != -1
//            || content.indexOf("<img alt=") != -1) {
//            alert("還敢亂打\n想害死我啊!!!");
//            return false;
//        };
//        $("#aniCircle").show(); //顯示Loading轉圈圈
//        return true;
//    });
//    //------------驗證表單 End--------------

//})();