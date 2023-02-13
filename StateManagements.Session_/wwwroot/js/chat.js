var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.querySelectorAll('.cart').forEach(item => {
    item.addEventListener('click', event => {
        //handle click
        console.log("salam");
        connection.invoke("RunCartBox");
    })
})


//cartelement.addEventListener('click', function () {
    
//    connection.invoke("RunCartBox");
//})
connection.on("ShowCartItemCount", function (itemcount) {
    var boxelement = document.getElementsByClassName("hs-cart-circle");
    boxelement.value = itemcount;
})