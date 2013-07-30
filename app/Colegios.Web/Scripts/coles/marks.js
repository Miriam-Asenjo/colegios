schoolMarks = function () {
};

schoolMarks.prototype.init = function (options) {

    var thisObj = this;

    var viewModel = {

        cityId: options.CityId,
        educationType: options.EducationType,
        allEducationTypes: options.AllEducationTypes,
        allCities: options.AllCities,
        allTowns: options.AllTowns,
        allDistricts: options.AllDistricts,
        city: ko.observable(options.CityId),
        town: ko.observable(options.TownId),
        district: ko.observable(),
    };

    viewModel.towns = ko.dependentObservable(function () {

        var selectedTowns = viewModel.allTowns[this.city()];
        if (!selectedTowns) {
            return [];
        }
        return selectedTowns;
    }, viewModel);
    
    viewModel.districts = ko.dependentObservable(function () {
        var selectedDistricts = viewModel.allDistricts[this.town()];
        if ((!selectedDistricts) || (selectedDistricts.length <= 1)) {
            var selectedValues = [];

            if ((selectedDistricts != null) && (selectedDistricts.length == 1)) {
                //selectedValues.push(selectedDistricts[0].Id);
                //options.Elements.hiddenlocation.val(selectedValues);
                $("label[for='Locations']").hide();
                $("#Locations").hide();
            }

            //$("#TownId").parent("div.row").css("width", "600px")
            //options.Elements.town.css("width", "280px");
            //if (this.town()) {
            //    options.Elements.eldistrict.fadeOut(400);
            //}
            return [];
        } else {
            //$("#TownId").parent("div.row").css("width", "500px")
            //options.Elements.town.css("width", "210px");
            //options.Elements.hiddenlocation.val([]);
            //options.Elements.eldistrict.fadeIn(800);
            $("label[for='Locations']").show();
            $("#Locations").show();
            return selectedDistricts;
        }

    }, viewModel);

    $(document).delegate('select[data-val=true]', 'mouseover', function (e) {
        var $errorMsg = $(e.target).parent().find("span.field-validation-error[data-valmsg-for=" + e.target.id + "]");
    });


    options.Elements.hiddenlocation.val((options.SelectedDistricts.length) ? options.SelectedDistricts : "");


    if (options.TabSelected) {
        $("#lnk" + options.TabSelected).parent().removeClass("lnkUnSelected").addClass("lnkSelected").children("div .lnkArrow").show()
        //$("#lnk" + options.TabSelected).next("a .lnkArrow").show(); 
    }


    var opinionModel = opinion.getViewModel();

    $.extend(viewModel, opinionModel);

    ko.applyBindings(viewModel, document.getElementById("main"));


   if (options.SelectedDistricts != null && options.SelectedDistricts.length > 0){

        for (var index = 0; index < options.SelectedDistricts.length; index ++) {
            $("#Locations option[value='" + options.SelectedDistricts[index] + "']").attr("selected", 1);
        }
   }

};

schoolmarks = new schoolMarks();