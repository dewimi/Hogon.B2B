/**
 * @description

 * 禾工通用Js组件
 */

// 通知操作成功
function notifySuccess(message) {
    parent.$(parent.document).trigger("notifySuccess", message);
}

// 通知操作失败
function notifyFailure(message) {
    parent.$(parent.document).trigger("notifyFailure", message);
}