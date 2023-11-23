    $(document).ready(function () {


        $('.js-delete').on('click', function () {
            var btn = $(this);

            Swal.fire({


                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#d33",
                cancelButtonColor: "#3085d6",
                confirmButtonText: "Yes, delete it!"

            }).then((result) => {
                if (result.isConfirmed) {

                    $.ajax({
                        url: `/Games/Delete/${btn.data('id')}`,
                        type: 'DELETE',
                        success: function (result) {

                            Swal.fire({
                                title: "Deleted!",
                                text: "Your file has been deleted.",
                                icon: "success"
                            });


                            btn.parents('tr').fadeOut();


                        },
                        error: function (result) {
                            Swal.fire({
                                title: "Opps!",
                                text: "Some thin went Wrong!",
                                icon: "danger"
                            });
                        }
                    });


                }



            });




            //$.ajax({
            //    url:`/Games/Delete/${btn.data('id')}`,
            //    type: 'DELETE',
            //    success: function (result) {




            //    },
            //    error:function(result){

            //    }
            //});

            // alert(`/Games/Delete/${btn.data('id')}`)



        });
        });
