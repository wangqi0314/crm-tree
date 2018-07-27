//检查数字
function clearNoNum(event, obj, type) {

    //响应鼠标事件，允许左右方向键移动 
    event = window.event || event;
    if (event.keyCode == 37 | event.keyCode == 39) {
        return;
    }
    if (type == 3) {
        //先把非数字的都替换掉，除了数字和. 
        obj.value = obj.value.replace(/[^\d.]/g, "");
        //必须保证第一个为数字而不是. 
        obj.value = obj.value.replace(/^\./g, "");
        //保证只有出现一个.而没有多个. 
        obj.value = obj.value.replace(/\.{4,}/g, ".");
        obj.value = obj.value.replace(/\.\d.{3}$/gi, "");

        //保证.只出现一次，而不能出现两次以上 
        obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");

        //保证只带一位小数
    }
    else
    if (type == 2) {
        //先把非数字的都替换掉，除了数字和. 
        obj.value = obj.value.replace(/[^\d.]/g, "");
        //必须保证第一个为数字而不是. 
        obj.value = obj.value.replace(/^\./g, "");
        //保证只有出现一个.而没有多个. 
        obj.value = obj.value.replace(/\.{3,}/g, ".");
        obj.value = obj.value.replace(/\.\d.{2}$/gi, "");

        //保证.只出现一次，而不能出现两次以上 
        obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");

        //保证只带一位小数
    } else
        if (type == 1) {
        //先把非数字的都替换掉，除了数字和. 
        obj.value = obj.value.replace(/[^\d.]/g, "");
        //必须保证第一个为数字而不是. 
        obj.value = obj.value.replace(/^\./g, "");
        //保证只有出现一个.而没有多个. 
        obj.value = obj.value.replace(/\.{2,}/g, ".");
        obj.value = obj.value.replace(/\.\d.{1}$/gi, "");

        //保证.只出现一次，而不能出现两次以上 
        obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    }

}
function checkNum(obj) {
    //为了去除最后一个. 
    obj.value = obj.value.replace(/\.$/g, "");
    //先把非数字的都替换掉，除了数字和. 
    obj.value = obj.value.replace(/[^\d.]/g, "");
}

function keydown(event) {
    if (event.keyCode == 8)//屏蔽退格键
    {
        var type = event.srcElement.type; //获取触发事件的对象类型
        //var tagName=window.event.srcElement.tagName;
        var reflag = event.srcElement.readOnly; //获取触发事件的对象是否只读
        var disflag = event.srcElement.disabled; //获取触发事件的对象是否可用
        if (type == "text" || type == "textarea")//触发该事件的对象是文本框或者文本域
        {
            if (reflag || disflag)//只读或者不可用
            {
                //window.event.stopPropagation();
                event.returnValue = false; //阻止浏览器默认动作的执行
            }
        }
        else {
            event.returnValue = false; //阻止浏览器默认动作的执行
        }
    }


}
//检查数字和 -
function clearNumTel(event, obj) {

    //响应鼠标事件，允许左右方向键移动 
    event = window.event || event;
    if (event.keyCode == 37 | event.keyCode == 39) {
        return;
    }
     //先把非数字的都替换掉，除了数字和. 
    obj.value = obj.value.replace(/[^\d-]/g, "");

}
function checkNumTel(obj) {
    //先把非数字的都替换掉，除了数字和. 
    obj.value = obj.value.replace(/[^\d-]/g, "");
}