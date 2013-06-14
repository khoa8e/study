$(document).ready(function () {
    //find all button update
    var buttonUpdate = $(".portalSetting").find(".buttonUpdate");
    buttonUpdate.each(function (i, item) {
        //call ajax to update
        $(item).unbind('click').bind('click', function () {
            //get portalId, initnumber value
            var tableRow = this.parentNode.parentNode;
            var portalId = $(tableRow).find(".portalId")[0].innerText;
            var initNumber = $(tableRow).find("input")[0].value;
            var cancelRemark = $(tableRow).find("input")[1].value;
            var urlRequest = "/User/UpdatePortalSetting";
            //ajax call             
            $.ajax({
                type: "POST",
                url: urlRequest,
                data: { portalId: portalId, initNumber: initNumber, cancelRemark: cancelRemark },
                dataType: "json",
                success: function (result) {
                    if (result.Result == true) {
                        alert('update successful');
                        console.log("success");
                    }
                },
                error: function () {
                    console.log("error");
                }
            });
        })
    });
});