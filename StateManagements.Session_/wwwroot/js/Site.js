$(() => {
    $('.cart').on('click', (e) => {
        let curentItem = e.currentTarget;
        var id = curentItem.getAttribute('id');


        $.ajax({
            url: `/cart/add/1`,
            method: 'post',
            success: (data) => { console.log(data) },
            error: (err) => { console.log(err) }
        });
    });


    $('.delete').on('click', (e) => {
        let curentItem = e.currentTarget;
        curentItem.pae  
    });

    $('.increase').on('click', (e) => {
        let curentItem = e.currentTarget;
    });

    $('.decrease').on('click', (e) => {
        let curentItem = e.currentTarget;
    });
})