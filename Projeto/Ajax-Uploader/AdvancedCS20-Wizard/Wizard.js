// JScript File

var WizardAjaxUrl="WizardAjax.ashx";

function GetWizardID()
{
	return window.location.href.split('#')[0].split('?')[1].replace(/(^|.*\&)WizardID\=([a-f0-9-]{36})(\&.*|$)/i,"$2");
}

function CreateXMLHttp()
{
	if(window.XMLHttpRequest)
		return new XMLHttpRequest();
	return new ActiveXObject("Microsoft.XMLHttp");
}

function AsyncLoadWizard(callback)
{
	var xh=CreateXMLHttp();
	var guid=GetWizardID();
	var url=WizardAjaxUrl+"?WizardID="+guid+"&Method=Load&_t="+new Date().getTime();
	xh.open("GET",url,true);
	xh.onreadystatechange=function()
	{
		if(xh.readyState<4)return;
		
		callback(xh);
	}
	xh.send("");
}
function ParseWizardResult(xh,errhandler)
{
	if(!errhandler)
		errhandler=alert;
		
	if(xh.status==500)
	{
		errhandler("server error:" + xh.responseText);
		return;
	}
	if(xh.status!=200)
	{
		errhandler("http error :"+xh.status);
		return;
	}
	if(!xh.responseText)
	{
		errhandler("server error , return nothing");
		return;
	}
	var res;
	try
	{
		eval("res="+xh.responseText)
	}
	catch(x)
	{
		errhandler("unable parse data:"+xh.responseText);
		return;
	}
	if(res.error)
	{
		switch(res.error)
		{
			case "InvalidWizardID":
				errhandler("invalid wizard data!");
				return;
			case "InvalidUserID":
				errhandler("you are not the owner of this wizard!");
				return;
			case "Exception":
				errhandler("Server Exception : "+res.errormessage);
				return;
			case "InvalidProfileName":
			case "InvalidProfileEmail":
				errhandler("invalid profile data!");
				return;
		}
		if(res.errormessage)
		{
			errhandler(res.errormessage);
			return;
		}
		errhandler("Unknown error : "+res.error);
		return;
	}
	return res;
}

















