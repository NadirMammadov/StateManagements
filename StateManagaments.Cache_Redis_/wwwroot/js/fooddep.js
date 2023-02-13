ar connection = new signalR.HubConnectionBuilder().withUrl("/fooddepHub").build();
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
function InvokeFoods() {
   
}
connection.on("ReceivedFoods", function (foods) {

});
