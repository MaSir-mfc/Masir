(function (window) {

    var timer;
    var callbackList = [];

    var _trigger = function (data) {

        for (var i = 0; i < callbackList.length; i++) {
            callbackList[i].call(window, data);
        }
    };

    window.XM = {

        init: function () {

            if (window.postMessage) {
                if (window.addEventListener) {
                    window.addEventListener("message", function (e) {
                        _trigger(e.data);
                    }, false);
                } else if (window.attachEvent) {
                    window.attachEvent("onmessage", function (e) {
                        _trigger(e.data);
                    });
                }
            } else {
                var hash = window.name = '';

                timer = setInterval(function () {
                    if (window.name !== hash) {
                        hash = window.name;
                        var tmp = hash;
                        hash = window.name = '';
                        _trigger(tmp);
                    }
                },
                 50);
            }
        },

        sendMessage: function (target, data) {
            if (!target) return;

            if (window.postMessage) {
                target.postMessage(data, '*');
            } else {
                target.name = data;
            }
        },

        onMessage: function (callback) {
            if (typeof callback !== 'function') return;
            callbackList.push(callback);
        }
    };

})(window);