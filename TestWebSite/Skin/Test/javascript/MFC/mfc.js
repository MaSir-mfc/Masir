(function () {
    if (!window.MFC) {
        window.MFC = {};
    }

    function isCompatible(other) {
        //使用能力检测来检测必要条件
        if (other === false || !Array.prototype.push || !Object.hasOwnProperty || !document.createElement || !document.getElementsByTagName) {
            return false;
        }
        return true;
    }
    window.MFC.isCompatible = isCompatible;

    function $() {
        var elements = new Array();
        //查找作为参数提供的所有元素
        for (var i = 0; i < arguments.length; i++) {
            var element = arguments[i];
            //如果该参数是一个字符串那假设他是一个id
            if (typeof element=='String') {
                element = document.getElementById(element);
            }
            //如果只提供了一个参数，则立即返回这个元素
            if (arguments.length==1) {
                return element;
            }
            //否则，将它添加到数组中
            elements.push(element);
        }
        //返回包含多个被请求元素的数组
        return elements;
    }
    window.MFC.$ = $;//var element=MFC.$('example');  //var element=document.getElementById('example');

    function addEvent(node, type, listener) {
        //使用前面的方法检测兼容性以保证平稳退化
        if (!isCompatible()) {
            return false;
        }
        if (!(node=$(node))) {
            return false;
        }
        if (node.addEventListener) {
            //W3C的方法
            node.addEventListener(type, listener, false);
            return true;
        } else if (node.attachEvent) {
            //MSIE的方法
            node['e' + type+listener] = listener;
            node[type + listener] = function () {
                node['e' + type + listener](window.event);
            }
            node.attachEvent('on' + type, node[type + listener]);
            return true;
        }
        //若两种方法都不具备则返回false
        return false;
    }
    window.MFC.addEvent = addEvent;

    function removeEvent(node, type, listener) {
        if (!(node=$(node))) {
            return false;
        }
        if (node.removeEventListener) {
            //W3C的方法
            node.removeEventListener(type, listener, false);
            return true;
        } else if (node.detachEvent) {
            //MSIE的方法
            node.detachEvent('on' + type, node[type + listener]);
            node[type + listener] = null;
            return true;
        }
        //若两种方法都不具备则返回false
        return false;
    }
    window.MFC.removeEvent = removeEvent;

    function getElementsByClassName(className, tag, parent) {

    }
    window.MFC.getElementsByClassName = getElementsByClassName;

    function toggleDisplay(node, value) {

    }
    window.MFC.toggleDisplay = toggleDisplay;

    function insertAfter(node, referenceNode) {

    }
    window.MFC.insertAfter = insertAfter;

    function removeChildren(parent) {

    }
    window.MFC.removeChildren = removeChildren;

    function prependChild(parent, newChild) {

    }
    window.MFC.prependChild = prependChild;
})();