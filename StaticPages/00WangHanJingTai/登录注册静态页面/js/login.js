function check(){
		var user = $("#login-username");
		var pwd = $("#login-pwd");
		var username = user.val();
		var password = pwd.val();
		var checkmg = $("#check-mg");
		
			if( username == null || username == ""){
				checkmg.html("请输入用户名");
                user.focus();
                return false;
			}
			if( password == null || password == ""){
				checkmg.html("请输入密码");
                pwd.focus();
                return false;
			}
//			var form = document.getElementById("login-form");
//			form.submit();
			
			$.ajax({
				type: "POST",
				url: "",
				data: "",
				success: function(result){
				//根据后台返回的result判断是否成功!
				return true;
				window.location.href="index.html";
			},error:function(){//处理页面出错以后执行的函数。
        	$('#check-mg').innerHTML="密码错误，请重新输入";
        	return false;
            }    
		}); 
}

window.onload=function(){
}
