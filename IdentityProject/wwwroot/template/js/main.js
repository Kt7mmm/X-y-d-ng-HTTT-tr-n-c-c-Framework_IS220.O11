$.ajaxSetup({
    headers: {
        'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
    }
});

function removeRow(id, url){

    if (confirm('Xóa mà không thể khôi phục. Bạn có chắc ?')){
        $.ajax({
            type: 'delete',
            datatype: 'JSON',
            data: {id},
            url: url,
            success: function(result){
                if (result.error == false){
                    alert(result.message);
                    location.reload();
                }
                else {
                    alert(result.message);
                }
            }
        })
    }
}

function removeRow2(id, id2, url){

    if (confirm('Xóa mà không thể khôi phục. Bạn có chắc ?')){
        $.ajax({
            type: 'delete',
            datatype: 'JSON',
            data: {id, id2},
            url: url,
            success: function(result){
                if (result.error == false){
                    alert(result.message);
                    location.reload();
                }
                else {
                    alert(result.message);
                }
            }
        })
    }
}

function removeRow3(id, id2, id3, url){

    if (confirm('Xóa mà không thể khôi phục. Bạn có chắc ?')){
        $.ajax({
            type: 'delete',
            datatype: 'JSON',
            data: {id, id2, id3},
            url: url,
            success: function(result){
                if (result.error == false){
                    alert(result.message);
                    location.reload();
                }
                else {
                    alert(result.message);
                }
            }
        })
    }
}

/* Upload file poster*/
$('#upload_poster').change(function () {
    var form = new FormData();
    form.append('file', $(this)[0].files[0]);

    $.ajax({
        processData: false,
        contentType: false,
        type: 'POST',
        data: form,
        url: '/Movie/StorePoster',
        success: function (result) {
            if (result.success) {
                // Hiển thị ảnh trên trang mà không làm tải lại
                $('#show_poster').html('<a href="' + result.imagePath + '" target="_blank">' +
                    '<img src="' + result.imagePath + '" width="100px"></a>');

                // Gán đường dẫn ảnh cho trường ẩn
                $('#link_poster').val(result.imagePath);
            } else {
                alert("Upload File Poster Lỗi: " + result.errorMessage);
            }
        },
        error: function (error) {
            alert("Upload File Poster Lỗi: " + error.statusText);
        }
    });
});


/* Upload file trailer*/
$('#upload_trailer').change(function(){

    var form = new FormData();
    form.append('file', $(this)[0].files[0]);

    $.ajax({
        processData: false,
        contentType: false,
        type: 'POST',
        data: form,
        url: '/Movie/StoreTrailer',
        success: function (result) {
            if (result.success) {
                $('#show_trailer').html('<a href="' + results.trailerPath + '" target="_blank">Đường dẫn trailer</a>');

                $('#link_trailer').val(results.trailerPath);
            }
            else {
                alert("Upload File Trailer Lỗi");
            }
        }
    });
});