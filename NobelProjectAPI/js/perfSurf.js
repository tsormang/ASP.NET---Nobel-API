// Bring Modal

$(function () {
    var showModal = function () {
        $("#theModal").modal("show");
    };
    $("#showModal").click(showModal);
});



// SignalR
(function () {
    var perfHub = $.connection.perfHub;
    $.connection.hub.logging = true;
    $.connection.hub.start();

    perfHub.client.newMessage = function (message) {
        model.addMessage(message);
    };

    var Model = function (message) {
        var self = this;
        self.message = ko.observable(""),
        self.messages = ko.observableArray()
    };

    Model.prototype = {
        sendMessage: function (message) {
            var self = this;
            perfHub.server.send(self.message());
            self.message("");
        },

        addmessage: function (message) {
            var self = this;
            self.messages.push(message);
        }
    };


    var model = new Model();
    $(function () {
        ko.applyBindings(model);
    });



}());