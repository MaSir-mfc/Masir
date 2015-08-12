jQuery.fn.updateimg = function (opts) {
    opts = jQuery.extend({
        container: jQuery(this).prop("id"),//要被flash替换的容器的id   
        editorType: 1,//（1为轻量编辑，2为轻量拼图，3为完整版，5为头像编辑器，默认值1）
        width: 500,
        height: 500,
        id: "lite",//编辑器id
        url: "http://b.txooo.com/Js/lib/uploadFile/updateImg.ashx",
        uploadType: 2,//上传方式1流式2post
        loadImg: "",//加载图片
        loadImgTrue: false,//是否加载图片
        imgName: "FileName",
        showImg: "show_img",
        showDiv: "upshow",//显示美图的div容器
        size: 2,//限制大小
        cropPresets: "300x300",//剪裁尺寸
        maxFinalWidth: 800,//最大宽
        maxFinalHeight: 600,//最大高
        customMenu: ["edit", "effect", "beautifySkin"],//自定义菜单
        titleVisible: 1,//标题栏显示隐藏
        albumType: 1//上传图片类型（0：自定义图片，1:：品牌，2：咨询，3：产品）
    }, opts || {});

    xiuxiu.setLaunchVars("maxFinalWidth", opts.maxFinalWidth);//最大宽
    xiuxiu.setLaunchVars("maxFinalHeight", opts.maxFinalHeight);//最大高
    xiuxiu.setLaunchVars("cropPresets", opts.cropPresets);//剪裁尺寸
    xiuxiu.setLaunchVars("customMenu", opts.customMenu);
    xiuxiu.setLaunchVars("titleVisible", opts.titleVisible);
    xiuxiu.setLaunchVars("nav", "edit");//默认打开项
    xiuxiu.setLaunchVars("subBrowseBtnVisible", 1);//重新加载图片
    //加载前后、、、////
    xiuxiu.embedSWF(opts.container, opts.editorType, opts.width, opts.height, opts.id);//加载编辑框
    xiuxiu.setUploadURL(opts.url, opts.id);//上传地址
    xiuxiu.setUploadArgs({ albumType: opts.albumType }, opts.id);//上传参数
    xiuxiu.setUploadType(opts.uploadType, opts.id);//上传方式1流式2post
    xiuxiu.setUploadDataFieldName(opts.imgName, opts.id);//上传name
    xiuxiu.onInit = function () {//加载图片
        if (opts.loadImg) {
            xiuxiu.loadPhoto(opts.loadImg, opts.loadImgTrue, opts.id);
        }
    }
    xiuxiu.onBeforeUpload = function (data, id) {//图片上传前的判断
        xiuxiu.setUploadArgs({ albumType: opts.albumType, imgName: data.name }, opts.id);//上传参数
        var size = data.size;
        if (size > opts.size * 1024 * 1024) {
            alert("图片不能超过" + opts.size + "M");
            return false;
        }
        return true;
    }
    xiuxiu.onUploadResponse = function (data) {//上传完毕
        $('#' + opts.showDiv).html('<div id="' + opts.container + '"></div>');
        UploadFinish(data, opts.showImg);
        $('.P_box').hide();
        $('.P_box_').hide();
        $('#windowFocus').focus().blur();
    }
    xiuxiu.onClose = function (id) {
        $('#' + opts.showDiv).html('<div id="' + opts.container + '"></div>');
        $('.P_box').hide();
        $('.P_box_').hide();
        $('#windowFocus').focus().blur();
    }
    xiuxiu.onDebug = function (data) {
        alert("错误响应" + data);
        //MT0001	上传接口不存在(404)
        //MT0002	您的服务器没有crossdomain.xml，
        //或者crossdomain.xml内未含有<allow-access-from domain="*.meitu.com"/>
        //查看crossdomain.xml如何设置
        //MT0003	上传接口未设置
    }
}