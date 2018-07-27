//op select method
function isInteger(value){
	var reg=/^[0-9]*[1-9][0-9]*$/;
	if(reg.test(value))
		return true;
	return false;
};
//添加option
function addOption(sel, tex, val,srcObj) {
	try{
		var obj = document.getElementById(sel);
		var opt = document.createElement("option");
		opt.value = val;
		opt.text = tex;
		var opitem=new Option(tex,val);
		if(srcObj){
			opitem['srcValue']=srcObj;
		}
		obj.options.add(opitem);
	}catch(e){
		alert("add error "+sel+":"+e.message);
	}
};
//根据value移除option
function removeOptionByValue(sel, val) {
	var obj = document.getElementById(sel);
	try{
	var count = obj.options.length;
	for (var i = 0; i < count; i++) {
		if (obj.options[i].value == val) {
			obj.removeChild(obj.options[i]);
			i = 0;
		}
	}
	}catch(ex){
		obj.innerHTML="";
	}
};
//根据value判断是否在select中
function inselect(sel,val){
	try {
		var obj = document.getElementById(sel);
		var count = obj.options.length;
		var i=0;
		for (i = 0; i < count; i++) {
			if (obj.options[i].value == val) {
				return true;
			}
		}
	}catch (e) {
		alert("inselect error!:" + e.message);
	}
	return false;
};
//使用数组填充列表
//fullSelect('productPtypeList', 'CategoryName', 'ID', list, false)
function fullSelect(sel, tcName, vcName, list, flag) {
	try {
		try{
			cleanSelect(sel);
		}catch(ee){
		    $("#"+sel).innerHTML='';
		}
		for (var i = 0; i < list.length; i++) {
			var tex = eval("list["+i+"]." + tcName);
			var val = eval("list["+i+"]." + vcName);
			addOption(sel, tex, val,list[i]);
		}
		if(sel=="productNumList")addOption(sel, "自定义", "other",null);
		if(flag && dtype != 30)addOption(sel, "自定义", "other",null);
		
		if($("#"+sel).options.length == 1){
			$("#"+sel).readOnly='true';
			$("#"+sel).disabled='disabled';
		}else{
			$("#"+sel).removeAttribute('readOnly');
			$("#"+sel).removeAttribute('disabled');
		}
	}
	catch (e) {
		alert(sel+"fullSelect error!:" + e.message);
	}
};
//使用数组填充列表
function fullSelect2(sel, tcName, vcName, list) {
	try {
		try{
			cleanSelect(sel);
		}catch(ee){
		    $(sel).innerHTML='';
		}
		for (var i = 0; i < list.length; i++) {
			var tex = eval("list["+i+"]." + tcName);
			var val = eval("list["+i+"]." + vcName);
			addOption(sel, tex, val,list[i]);
		}
	}
	catch (e) {
		alert(sel+"fullSelect error!:" + e.message);
	}
};
function fullSelect3(sel, tcName, vcName, list) {
    try {
		try{
			cleanSelect(sel);
		}catch(ee){
		    $(sel).innerHTML='';
		}
		var defaultStr = "请选择";
		
		addOption(sel, defaultStr, "0");
		for (var i = 0; i < list.length; i++) {
			var tex = eval("list["+i+"]." + tcName);
			var val = eval("list["+i+"]." + vcName);
			addOption(sel, tex, val,list[i]);
		}
	}
	catch (e) {
		alert(sel+"fullSelect error!:" + e.message);
	}
};

//选择指定值
function selectOption(sel, val) {
	try {
		var obj = document.getElementById(sel);
		var count = obj.options.length;
		
		var i=0;
		for ( i= 0; i < count; i++) {
			if (obj.options[i].value == val) {
				//alert(val +" "+i+" "+obj.options[i].value);
				obj.selectedIndex = i;
				break;
			}
		}
		if(i!=0 && i==count){
			obj.selectedIndex = 0;
		}
	}
	catch (e) {
		alert("selectOption error!:" + e.message);
	}
};
//选择指定值
function selectOption1(sel, val) {
	//alert(sel +" "+val);
	try {
		var obj = document.getElementById(sel);
		var count = obj.options.length;
		var i=0;
		for ( i= 0; i < count; i++) {
			if (obj.options[i].value == val) {
				//alert(val +" "+i+" "+obj.options[i].value);
				obj.selectedIndex = i;
				break;
			}
		}
		if(i!=0 && i==count){
			obj.selectedIndex = count - 1;
		}
	}
	catch (e) {
		alert("selectOption error!:" + e.message);
	}
};

//选择指定文本值項
function selectOptionByText(sel, val) {
	//alert(sel +" "+val);
	try {
		var obj = document.getElementById(sel);
		var count = obj.options.length;
		var i=0;
		if(val != "自定义"){
			for ( i= 0; i < count; i++) {
				if (obj.options[i].text == val) {
					//alert(val +" "+i+" "+obj.options[i].value);
					obj.selectedIndex = i;
					break;
				}
			}
		}
		if(i!=0 && i==count){
			obj.selectedIndex = 0;
		}
	}
	catch (e) {
		alert("selectOption error!:" + e.message);
	}
};
//清空选项
function cleanSelect(sel) {
	var obj = document.getElementById(sel);
	try{
		var count = obj.options.length;
		for (var i = 0; i < count; i++) {
			obj.removeChild(obj.options[0]);
		}
	}catch(ex){obj.innerHTML='';}
};
function cleanSelect2(sel) {
    var obj = document.getElementById(sel);
    try {
        var count = obj.options.length;
        for (var i = 1; i < count; i++) {
            obj.removeChild(obj.options[0]);
        }
    } catch (ex) { obj.innerHTML = ''; }
};

//str = "{id:1,name:"zhangshan"}";
function strToJson(str){
     var json = eval('(' + str + ')'); 
     return json;
};

function strToJsonArray(str,splitChar){
    var list=[];
    var tmpList = [];
    if(str != "") {
        if(splitChar=="") 
            splitChar="@";
        list = str.split(splitChar);
        if(list.length>0) {
            for(var i=0,j=0;i<list.length;i++) {
                if(list[i]!="")
                {
                    tmpList[j] = strToJson(list[i]);
                    j++;
                }
	        }
        }
    }
    return tmpList;
};