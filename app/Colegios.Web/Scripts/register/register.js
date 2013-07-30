Register = function () {
};

Register.prototype.init = function (options) {

    $("#Institution_Town").hide();
    $("#Institution_Town").prev("label").hide();
    var viewModel = {
        allCities: options.AllCities,
        allTowns: options.AllTowns,
        allDistricts: options.AllDistricts,
        city: ko.observable(options.CityId),
        town: ko.observable(),

        showCommonInfo: function () {

        },
        continueAccount: function () {
            var validator = options.form.validate();
            validator.settings.ignore = ":hidden, #commonDetails :input, #specificDetails input, #specificDetails textarea";
            $.data(options.form[0], 'validator', validator);
            if (options.form.valid())
                options.basicInfo.show();

        },

        continueBasicInfo: function () {
            var validator = options.form.validate();
            validator.settings.ignore = ":hidden, #specificDetails input, #specificDetails textarea";
            $.data(options.form[0], 'validator', validator);
            if (options.form.valid())
                options.detailInfo.show();
        },

        continueDetailInfo: function () {
            var validator = options.form.validate();
            validator.settings.ignore = ":hidden";
            $.data(options.form[0], 'validator', validator);
            console.log("Form valid: " + options.form.valid());
            if (options.form.valid())
                options.form.submit();
        }

    };

    viewModel.towns = ko.dependentObservable(function () {

        if (this.city() > 0) {
            $("#Institution_Town").show();
            $("#Institution_Town").prev("label").show();
        } else {
            $("#Institution_Town").hide();
            $("#Institution_Town").prev("label").hide();
            $("#districts").hide();
        }

        var selectedTowns = viewModel.allTowns[this.city()];
        if (!selectedTowns) {
            return [];
        }
        return selectedTowns;
    }, viewModel);

    viewModel.districts = ko.dependentObservable(function () {

        if (this.town() == 2) {
            $("#districts").show();
            var selectedDistricts = viewModel.allDistricts[this.town()];
            return selectedDistricts;
        }
        $("#districts").hide();
        return [];
    }, viewModel);

    ko.applyBindings(viewModel, document.getElementById("main"));

}


register = new Register();