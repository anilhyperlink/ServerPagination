/********************** start pagination listing **********************/
var searchQuery = '';

/*display user record*/
function UserList(pageNumber) {
    ShowLoader();
    $.ajax({
        type: 'get',
        url: '/Home/LoadUserList',
        data: { pageNumber: pageNumber, searchQuery: searchQuery },
        success: function (data) {
            HideLoader();
            $('#UserListPartial').html(data);
        },
        error: function (er) {
            console.log(er);
        },
    });
}

$(document).ready(function () {
    UserList(1); // Load initial user list on page load

    $('#demo-input-search2').on('input', function () {
        searchQuery = $(this).val();
        UserList(1); // Load user list with updated search query and reset to first page
    });
});
/********************** end pagination listing **********************/

/********************** start add modal  **********************/
function AddUser() {

    var formData = $('#AddUserForm').serialize();
    ShowLoader();
    $.ajax({
        type: 'get',
        url: '/Home/AddUser',
        data: formData,
        success: function (data) {
            debugger;
            HideLoader();
            $('#addclose').click();
            $("#AddUserForm")[0].reset();
            UserList(1);
        },
        error: function (er) {
            console.log(er);
        },
    });
}
/********************** end add modal **********************/

/********************** start clear modal data **********************/
function AddClearModalData() {
    $("#AddUserForm")[0].reset();
}

function EditClearModalData() {
    $("#AddUserForm")[0].reset();
}
/********************** end clear modal data **********************/

/********************** start delete data **********************/
function DeleteUser(UserId) {
    ShowLoader();
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this Data!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: 'delete',
                    url: '/Home/DeleteUser',
                    data: { UserId: UserId },
                    success: function (data) {
                        HideLoader();
                        swal("Poof! Your Data has been deleted!", {
                            icon: "success",
                        });
                        UserList(1);
                    },
                    error: function (er) {
                        console.log(er);
                    },
                });

            } else {
                HideLoader();
                swal("Your imaginary file is safe!");
            }
        });
}
/********************** end delete data **********************/

/********************** start active manage data **********************/
function ActiveManage(UserId) {
    ShowLoader();
    $.ajax({
        type: 'get',
        url: '/Home/ActiveManage',
        data: { UserId: UserId },
        success: function (data) {
            HideLoader();
            UserList(1);
        },
        error: function (er) {
            console.log(er);
        },
    });
}
/********************** end delete data **********************/

function ViewUser(UserId) {
    $.ajax({
        type: 'get',
        url: '/Home/ViewUser',
        data: { UserId: UserId },
        success: function (data) {
            debugger;
            $('#FirstName').val(data.firstName);
            $("#LastName").val(data.lastName);
            $('#EmailAddress').val(data.emailAddress);
            if (data.gender === 'Male') {
                $('#Male').attr('checked', '');
            }
            else if (data.gender === 'Female') {
                $('#Female').attr('checked', '');
            }
            $('#MobileNo').val(data.mobileNo);
        },
        error: function (er) {
            console.log(er);
        },
    });
}

function GetUser(UserId) {
    $.ajax({
        type: 'get',
        url: '/Home/GetUser',
        data: { UserId: UserId },
        success: function (data) {
            debugger;
            $('#UserId').val(data.userId);
            $('#FirstName1').val(data.firstName);
            $("#LastName1").val(data.lastName);
            $('#EmailAddress1').val(data.emailAddress);
            if (data.gender === 'Male') {
                $('#Male1').attr('checked', '');
            }
            else if (data.gender === 'Female') {
                $('#Female1').attr('checked', '');
            }
            $('#MobileNo1').val(data.mobileNo);
        },
        error: function (er) {
            console.log(er);
        },
    });
}
function EditUser() {

    var formData = $('#EditUserForm').serialize();
    ShowLoader();
    $.ajax({
        type: 'put',
        url: '/Home/EditUser',
        data: formData,
        success: function (data) {
            debugger;
            HideLoader();
            $('#editclose').click();
            $("#AddUserForm")[0].reset();
            UserList(1);
        },
        error: function (er) {
            console.log(er);
        },
    });
}