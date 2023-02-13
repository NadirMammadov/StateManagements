var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.start();
document.querySelectorAll('.cart').forEach(item => {
    item.addEventListener('click', event => {
        //handle click
        var ss = 0;
        connection.invoke("RunNese",ss);
    })
})



//cartelement.addEventListener('click', function () {
    
//    connection.invoke("RunCartBox");
//})
connection.on("ShowCartItemCount", function (itemcount,total) {
    var boxelement = document.getElementById("item-count");
    var totalelement = document.getElementById("sb-cartbox-amount");
    var itemcountelement = document.getElementById("item-count");
    var sbcartboxlist = document.getElementById("sb-cartbox-list");
    
    totalelement.textContent = total;
    
    itemcountelement.textContent = itemcount;
    boxelement.textContent = itemcount;
})