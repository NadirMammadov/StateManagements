var connection = new signalR.HubConnectionBuilder().withUrl("/fooddepHub").build();
connection.start().then(function () {
    connection.invoke("FirstPageData");
});

connection.on("GetFoods", function (foods) {
    var div = document.getElementById("col");
    div.innerHTML = " ";
    foods.forEach(function (food) {
        div.innerHTML += `<div class="card shadow-sm">
                <img class="card-img-top" src="${food.src}" alt="Card image cap">
                <div class="card-body">
                    <h5 class="card-title">${food.name}</h5>
                    <p class="card-text">
                        İçerisinde bilmediğimiz malzemeler olan pizzalar
                    </p>
                </div>
                <ul class="list-group list-group-flush">
                    <li class="list-group-item">Fiyat  ${food.price}</li>
                    <li class="list-group-item">Kategori ${food.categoryName}</li>
                </ul>

            </div>`
    });
    console.log("isledi");
});
