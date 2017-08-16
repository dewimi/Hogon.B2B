//$(function(){
//		$(".filed input").focus(function(){
//		var hint =$(this).data('hint');
//			$("#check_"+$(this).prop('id')).html(hint);			
//		})
//		$(".filed input").blur(function(){
//			$("#check_"+$(this).prop('id')).html('');		
//	});
//		var hint =$(this).data('hint');
//	$("#check_"+$(this).ID).html(hint);
//})
$.validator.setDefaults({
	submitHandler: function() {
		alert("注册成功");
	}
});

$(function() {
		jQuery.validator.addMethod("username", function(value, element) {
		var username =  /[\u4e00-\u9fa5_a-zA-Z0-9_]{4,16}/;
		return (username.test(value)) || this.optional(element);
	}, "支持中文,字母,数字,下划线，4-16个字符");
	
		jQuery.validator.addMethod("pwd", function(value, element) {
		var mail = /^(?!(?:\d+|[a-zA-Z]+)$)[\da-zA-Z]{6,20}$/;
		return (mail.test(value)) || this.optional(element);
	}, "至少使用两种字符组合，支持字母,数字,下划线，6-20个字符");

jQuery.validator.addMethod("phone", function(value, element) {
		var phone = /0?(13|14|15|18)[0-9]{9}/;
		return (phone.test(value)) || this.optional(element);
	}, "请输入有效的手机号码");
	
jQuery.validator.addMethod("email", function(value, element) {
		var email = /\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}/;
		return (email.test(value)) || this.optional(element);
	}, "请输入有效的电子邮件地址");
	
	$("#register-form").validate({
		errorPlacement: function(error, element) {
			//			$(element)
			//				.closest( ".input-group" )//.nextSbiling
			//					.nextSbiling('p')
			//						.append( error );
			$("#check_" + $(element).prop('id')).html("").append(error);
		},
		errorElement: "p",
		rules: {
			reg_username: {
				required: true,
				rangelength: [4, 12],
				username:true
			},
			reg_pwd: {
				required: true,
				rangelength: [6, 20],
				pwd: true
			},
			reg_repwd: {
				required: true,
				rangelength: [6, 20],
				equalTo: "#reg_pwd"
			},
			reg_phone: {
				required: true,
				maxlength:11,
				phone:true
			},
			reg_email: {
				required: true,
				email: true
			}
		},
		messages: {
			reg_username: {
				required: "请输入用户名,支持中文,字母,数字,下划线，4-16个字符",
				rangelength: "支持中文,字母,数字,下划线，4-16个字符"
			},
			reg_pwd: {
				required: "请输入密码,支持字母,数字,下划线，6-20个字符",
				rangelength: "建议至少使用两种字符组合，支持字母,数字,下划线，6-20个字符"
			},
			reg_repwd: {
				required: "请确认密码",
				rangelength: "密码为6-20个字符",
				equalTo: "两次密码输入不一致"
			},
			reg_phone: {
				required:"请输入手机号",
				maxlength:"请输入正确的手机号",
				phone:"请输入正确的手机号"
			},
			reg_email: {
				required: "请输入电子邮件地址",
				email:"请输入有效的电子邮件地址"
			}
		}
	});
	showBg();
});
function showBg() {
	var bh = $("body").height();
	var bw = $("body").width();
	$(".coverbg").css({
		height: bh,
		width: bw,
		display: "block"
	});
	$(".dialog").show();
}
//返回  
function goback() {
	window.location.href="index.html"
}
function closebg(){
	$(".coverbg,.dialog").hide();
}
