Blog = function () {
}

Blog.prototype.init = function (options) {

    if (options.TabSelected) {
        $("#lnk" + options.TabSelected).parent().removeClass("lnkUnSelected").addClass("lnkSelected").children("div .lnkArrow").show();
    }

    if (options.TopicSelected) {
        var topics = $("ul.topics>li");
        $.each(topics, function (index, value) {
            //console.log("Id " + value.Id);
            if (value.id == options.TopicSelected) {
                $(value).find("img#unselected").first().hide();
                $(value).find("img#selected").first().show();
            } else {
                $(value).find("img#unselected").first().show();
                $(value).find("img#selected").first().hide();
            }
        });
    }

    var opinionModel = opinion.getViewModel();

    ko.applyBindings(opinionModel, document.getElementById("main"));

}


blog = new Blog();
