
//<script type="text/javascript" src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
var wxShare = {
    title: null,//标题
    desc: null,//描述
    link: null,//链接
    imgUrl: null,//图标
    accountType: null,//账户类型

    //分享完成后执行的代码
    shareFinish: function () { },
    //分享失败后执行的代码
    shareError: function () { },
    //分享异步代码
    init: function () {
        $.ajax({
            type: "get",
            async: false,
            url: "http://passport.txooo.com/Member/TxMemberHandler.ashx/WXShareParams",
            data: 'selfurl=' + encodeURIComponent(window.location.href) + '&accountType=' + wxShare.accountType,
            dataType: "jsonp",
            jsonp: "callbackparam",//服务端用于接收callback调用的function名的参数
            jsonpCallback: "success_jsonpCallback",//callback的function名称
            success: function (json) {
                wxShare.wxParams(json[0]);
            },
            error: function () {
                alert('微信分享加载失败了');
            }
        });
    },
    //微信内部函数
    wxParams: function (jsonObj) {
        wx.config({
            debug: false,  // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: jsonObj.appId, // 必填，公众号的唯一标识
            timestamp: jsonObj.timestamp, // 必填，生成签名的时间戳
            nonceStr: jsonObj.nonceStr, // 必填，生成签名的随机串
            signature: jsonObj.signature,// 必填，签名，见附录1
            jsApiList: [
                'onMenuShareTimeline',
                'onMenuShareAppMessage',
                'onMenuShareQQ',
                'onMenuShareWeibo',
                'onMenuShareQZone'
            ]
        });
        wx.ready(function () {
            // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。
            wx.onMenuShareTimeline({
                title: wxShare.title, // 分享标题
                link: wxShare.link, // 分享链接
                imgUrl: wxShare.imgUrl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                    wxShare.shareFinish();
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                    wxShare.shareError();
                }
            });
            wx.onMenuShareAppMessage({
                title: wxShare.title, // 分享标题
                desc: wxShare.desc, // 分享描述
                link: wxShare.link, // 分享链接
                imgUrl: wxShare.imgUrl, // 分享图标
                type: '', // 分享类型,music、video或link，不填默认为link
                dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                success: function () {
                    // 用户确认分享后执行的回调函数
                    wxShare.shareFinish();
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                    wxShare.shareError();
                }
            });
            wx.onMenuShareQQ({
                title: wxShare.title, // 分享标题
                desc: wxShare.desc, // 分享描述
                link: wxShare.link, // 分享链接
                imgUrl: wxShare.imgUrl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                    wxShare.shareFinish();
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                    wxShare.shareError();
                }
            });
            wx.onMenuShareWeibo({
                title: wxShare.title, // 分享标题
                desc: wxShare.desc, // 分享描述
                link: wxShare.link, // 分享链接
                imgUrl: wxShare.imgUrl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                    wxShare.shareFinish();
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                    wxShare.shareError();
                }
            });
            wx.onMenuShareQZone({
                title: wxShare.title, // 分享标题
                desc: wxShare.desc, // 分享描述
                link: wxShare.link, // 分享链接
                imgUrl: wxShare.imgUrl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                    wxShare.shareFinish();
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                    wxShare.shareError();
                }
            });
        });
        wx.error(function (res) {
            // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。

        });
    }
};
