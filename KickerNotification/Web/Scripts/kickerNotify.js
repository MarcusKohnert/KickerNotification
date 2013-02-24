/// <reference path="jquery-1.6.4.js" />
/// <reference path="jquery.signalR-0.5.3.js" />

$(function () {
    var hub = $.connection.kickerNotifyHub;

    hub.setCurrentPlayerCount = function (count) {
        $('#currentPlayerCount').text(count);
    };

    hub.updateStatus = function (status) {
        $('#statusBar').text(status);
    };

    $.connection.hub.start();
});