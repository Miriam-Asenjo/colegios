
colesSearch = function () {
};

colesSearch.prototype.init = function (options, googleMap) {

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

        showHideSection: function (section, schoolId) {
            var $category = $("span#categoryField" + section + schoolId);
            if ($category.css("display") == 'none') {
                $category.show();
            }
            else {
                $category.hide();
            }

            var $categoryField = $("ul[name=categoryFieldDetails" + section + schoolId + "]");
            $.each($categoryField, function (i, value) {
                if ($(value).css("display") == 'none') {
                    $(value).show();
                }
                else {
                    $(value).hide();
                }
            });
        },

        showBlock: function (schoolId, latitude, longitude) {

            $("#map_canvas" + schoolId).show();
            $("#showBlock" + schoolId).show().animate({
                opacity: 1,
                right: 120
            }, "slow").css("height", "240px").css("width", "450px");

            if ($("#map_canvas" + schoolId).children().length == 0) {
                thisObj.googleApi.createMap('map_canvas' + schoolId, latitude, longitude, null, options.TabSelected);
            }

            $("#marks" + schoolId).hide()

        },

        hideBlock: function (schoolId) {
            $("#showBlock" + schoolId).hide();
        },

        showMarks: function (schoolId) {
            if (!$("#showBlock" + schoolId).is(":visible")) {
                $("#showBlock" + schoolId).show().animate({
                    opacity: 1,
                    right: 120
                }, "slow").css("height", "300px").css("width", "450px");
            }

            $("#map_canvas" + schoolId).hide();
            $("#marks" + schoolId).show();
        },

        minimizeSearch: function () {
            $("#searchForm").find("div").each(function (index, value) {
                if (index > 0)
                    $(value).hide();
            });

            $("img[name='expandSearchForm']").show(true);
            $("span.expandSearchForm").show(true);
            $("img#minimizeSearchForm").hide();
        },

        maximizeSearch: function () {
            $("#searchForm").find("div").each(function (index, value) {
                $(value).show();
            });

            $("img[name='expandSearchForm']").hide();
            $("span.expandSearchForm").hide();
            $("img#minimizeSearchForm").show(true);

        },

        //        openDialog: function() {
        //            $("#dialogOpinion").dialog({autoOpen: false, 
        //                                        modal: true,
        //                                        closeOnEscape: true,
        //                                        width: '400',
        //                                        height: '300',
        //                                        position: ['center','center'],
        //                                        buttons: { "Enviar": function() { $(this).dialog("close"); } }});
        //                                        
        //            $("#dialogOpinion").dialog('open');
        //        }

        showMapSchools: function (latitude, longitude) {
            $("#map_canvas").show();
            $("#showBlockMapSchools").show().animate({
                opacity: 1,
                right: 120// containerWidth - 200,
            }, "slow").css("height", "380px").css("width", "620px");

            thisObj.googleApi.createMap('map_canvas', latitude, longitude, options.LocationSchools, options.TabSelected);
        },

        hideMapSchools: function () {
            $("#showBlockMapSchools").hide();
        }
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
                selectedValues.push(selectedDistricts[0].Id);
                options.Elements.hiddenlocation.val(selectedValues);
            }

            $("#TownId").parent("div.row").css("width", "600px")
            options.Elements.town.css("width", "280px");
            if (this.town()) {
                options.Elements.eldistrict.fadeOut(400);
            }
            return [];
        } else {
            $("#TownId").parent("div.row").css("width", "500px")
            options.Elements.town.css("width", "210px");
            options.Elements.hiddenlocation.val([]);
            options.Elements.eldistrict.fadeIn(800);
            return selectedDistricts;
        }

    }, viewModel);



    options.Elements.ellocality.live("change", function () {
        var selectedValues = [];

        $("input[type=checkbox][id=locality]:checked").each(function () {
            selectedValues.push($(this).val());
        });
        //assign to hidden element
        options.Elements.hiddenlocation.val(selectedValues);
    });

    $(document).delegate('select[data-val=true]', 'mouseover', function (e) {
        var $errorMsg = $(e.target).parent().find("span.field-validation-error[data-valmsg-for=" + e.target.id + "]");
    });


    options.Elements.hiddenlocation.val((options.SelectedDistricts.length) ? options.SelectedDistricts : "");

    if (googleMap) {
        this.googleApi = googleMap;
        this.googleApi.init(options.MapOptions);
    }

    if (options.TabSelected) {
        $("#lnk" + options.TabSelected).parent().removeClass("lnkUnSelected").addClass("lnkSelected").children("div .lnkArrow").show()
        //$("#lnk" + options.TabSelected).next("a .lnkArrow").show(); 
    }


    var opinionModel = opinion.getViewModel();

    $.extend(viewModel, opinionModel);

    ko.applyBindings(viewModel, document.getElementById("main"));

    $.each(options.SelectedDistricts, function (i, value) {
        $("input[type=checkbox][id=locality][value=" + value + "]").attr('checked', true);
    });


    if (options.MinimizeSearch) {
        viewModel.minimizeSearch();
    }

    if (options.HasPics != undefined && options.HasPics == true) {
	    if ($.browser.msie == undefined){
			$("#slider>img:first").load(function () {
				$('#slider').nivoSlider({
					effect: 'boxRain', adjust: true,
					slices: 20, // For slice animations
					boxCols: 8, // For box animations
					boxRows: 1, // For box animations
					animSpeed: 3000, // Slide transition speed   
					pauseTime: 3000, // How long each slide will show  ,   
					controlNavThumbs: true
				});
			});
		}else {
			$('#slider').nivoSlider({
					effect: 'boxRain', adjust: true,
					slices: 20, // For slice animations
					boxCols: 8, // For box animations
					boxRows: 1, // For box animations
					animSpeed: 3000, // Slide transition speed   
					pauseTime: 3000, // How long each slide will show  ,   
					controlNavThumbs: true
			});
		
		}
    }

}

//colesSearch.prototype.madridDistricts = function () {

//    var r = Raphael(document.getElementById("map"), 500, 700);

//    arr = new Array();

//    for (var country in pathsMadridDistricts) {
//        console.log(country);
//        var obj = r.path(pathsMadridDistricts[country].path);

//        obj.attr(pathsMadridDistricts[country].attributes);
//        arr[obj.id] = country;

//        if (pathsMadridDistricts[country].isDistrict === 'true') {
//            obj
//  		.hover(function () {
//  		    var id = arr[this.id];
//  		    this.animate({
//  		        fill: pathsMadridDistrictspaths[id].selectedColour
//  		    }, 300);
//  		}, function () {
//  		    var id = arr[this.id];
//  		    this.animate({
//  		        fill: pathsMadridDistricts[id].attributes.fill
//  		    }, 300);
//  		});

//            obj.click(function () {

//                document.location.hash = arr[this.id];

//                var point = this.getBBox(0);

//                $('#map').next('.point').remove();

//                $('#map').after($('<div />').addClass('point'));

//                $('.point')
//  			.html(pathsMadridDistricts[arr[this.id]].name)
//  			.prepend($('<a />').attr('href', '#').addClass('close').text('Close'))
//  			.prepend($('<img />').attr('src', 'flags/' + arr[this.id] + '.png'))
//  			.css({
//  			    left: point.x + (point.width / 2) - 80,
//  			    top: point.y + (point.height / 2) - 20
//  			})
//  			.fadeIn();

//            });

//            $('.point').find('.close').live('click', function () {
//                var t = $(this),
//  				parent = t.parent('.point');

//                parent.fadeOut(function () {
//                    parent.remove();
//                });
//                return false;
//            });
//        }
//    }

//    if (($.browser.mozilla) || ($.browser.msie)) {
//        r.setViewBox(20, 380, 850, 500, true);
//    } else if ($.browser.webkit) {
//        r.setViewBox(20, 35, 850, 1200, true);
//    }

//}

colesearch = new colesSearch();