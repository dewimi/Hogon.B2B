//图片上传预览
function preview(file)  {
 	var prevDiv = document.getElementById('thumbnail');  
 	if (file.files && file.files[0]){
 		
 		var reader = new FileReader();  
 		reader.onload = function(evt){  
 		prevDiv.innerHTML = '<img src="' + evt.target.result + '" />';  
	} 
	reader.readAsDataURL(file.files[0]);
	}else{  
 	prevDiv.innerHTML = '<div class="img" style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=\'' + file.value + '\'"></div>';  
 }  
 } 
 
 $.validator.setDefaults({
	submitHandler: function() {
		alert("注册成功");
	}
});
 $(function() {
 	//用户名
		jQuery.validator.addMethod("user", function(value, element) {
		var user =  /[\u4e00-\u9fa5_a-zA-Z0-9_]{4,16}/;
		return (user.test(value)) || this.optional(element);
	}, "支持中文,字母,数字,下划线，4-16个字符");
		jQuery.validator.addMethod("pwd", function(value, element) {
		var mail = /^(?!(?:\d+|[a-zA-Z]+)$)[\da-zA-Z]{6,20}$/;
		return (mail.test(value)) || this.optional(element);
	}, "建议至少使用两种字符组合，支持字母,数字,下划线，6-20个字符");

jQuery.validator.addMethod("username", function(value, element) {
		var username = /[\u4e00-\u9fa5]/;
		return (username.test(value)) || this.optional(element);
	}, "请输入联系人姓名");
	
jQuery.validator.addMethod("phone", function(value, element) {
		/*var phone = /0?(13|14|15|18)[0-9]{9}/;*/
		var phone=/^0?(13|15|17|18|14)[0-9]{9}$/;
		return (phone.test(value)) || this.optional(element);
	}, "请输入有效的手机号码");
	
jQuery.validator.addMethod("email", function(value, element) {
		var email = /\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}/;
		return (email.test(value)) || this.optional(element);
	}, "请输入有效的电子邮件地址");
	
	$("#register-form").validate({
		rules: {
			user: {
				required: true,
				rangelength: [4, 6],
				user:true
			},
			pwd: {
				required: true,
				rangelength: [6, 20],
				pwd: true
			},
			repwd: {
				required: true,
				rangelength: [6, 20],
				equalTo: "#pwd"
			},
			username: {
				required: true,
				rangelength: [2, 6],
				username:true
			},
			phone: {
				required: true,
				maxlength:11,
				phone:true
			},
			email: {
				required: true,
				email: true
			},
			companyname:{
				required:true
			},
			address:{
				required:true
			},
			agree: "required"
		},
		messages: {
			user: {
				required: "请输入用户名",
				rangelength: "支持中文,字母,数字,下划线，4-16个字符"
			},
			pwd: {
				required: "请输入密码",
				rangelength: "建议至少使用两种字符组合，支持字母,数字,下划线，6-20个字符"
			},
			repwd: {
				required: "请确认密码",
				rangelength: "密码为6-20个字符",
				equalTo: "两次密码输入不一致"
			},
			username: {
				required: "请输入联系人姓名",
				rangelength: "仅支持中文，2-6个字符"
			},
			phone: {
				required:"请输入手机号",
				maxlength:"请输入正确的手机号",
				phone:"请输入正确的手机号"
			},
			email: {
				required: "请输入电子邮件地址",
				email:"请输入有效的电子邮件地址"
			},
			companyname:{
				required:"请输入公司名称(工商局注册全称)"
			},
			address:{
				required:"请输入公司详细地址"
			},
			agree: "请同意用户注册协议"
		}
	});
});
