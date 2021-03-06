﻿(function() {
    function cookie(key, value, end, path, domain, secure) {
        if (arguments.length === 1) {
            return decodeURIComponent(document.cookie.replace(new RegExp("(?:(?:^|.*;)\\s*" + encodeURIComponent(key).replace(/[\-\.\+\*]/g, "\\$&") + "\\s*\\=\\s*([^;]*).*$)|^.*$"), "$1")) || null;
        }

        if (!key || /^(?:expires|max\-age|path|domain|secure)$/i.test(key)) {
            return false;
        }
        var expires = "";
        if (end) {
            switch (end.constructor) {
            case Number:
                expires = end === Infinity ? "; expires=Fri, 31 Dec 9999 23:59:59 GMT" : "; max-age=" + end;
                break;
            case String:
                expires = "; expires=" + end;
                break;
            case Date:
                expires = "; expires=" + end.toUTCString();
                break;
            }
        }
        document.cookie = encodeURIComponent(key) + "=" + encodeURIComponent(value) + expires + (domain ? "; domain=" + domain : "") + (path ? "; path=" + path : "") + (secure ? "; secure" : "");
        return true;
    }

    $(document).ready(function() {
        themeChooser.publishTheme(window.kendoTheme);
    });

    var doc = document,
        extend = $.extend,
        proxy = $.proxy,
        kendo = window.kendo,
        animation = {
            show: {
                effects: "fadeIn",
                duration: 300
            },
            hide: {
                effects: "fadeOut",
                duration: 300
            }
        },
        skinRegex = /kendo\.[\w\-]+(\.min)?\.(less|css)/i;

    var details = kendo.ui.Widget.extend({
        init: function(element, options) {
            kendo.ui.Widget.fn.init.call(this, element, options);

            this._summary = this.element.find(".tc-activator")
                .on("click", proxy(this.toggle, this));

            this._container = kendo.wrap(this._summary.next(), true);

            this._container.css("display", "none");
        },
        options: {
            name: "Details"
        },
        toggle: function() {
            var options = this.options;
            var show = this._container.is(":visible");
            var animation = kendo.fx(this._container).expand("vertical");

            animation.stop()[show ? "reverse" : "play"]();

            this._summary.toggleClass("tc-active", !show);
        }
    });

    kendo.ui.plugin(details);

    var themeChooser = kendo.ui.ListView.extend({
        init: function(element, options) {
            kendo.ui.ListView.fn.init.call(this, element, options);

            this.bind("change", this._updateTheme);
        },
        options: {
            name: "ThemeChooser",
            template: "",
            selectable: true,
            value: ""
        },
        dataItem: function(element) {
            var uid = $(element).closest("[data-uid]").attr("data-uid");
            return this.dataSource.getByUid(uid);
        },
        _updateTheme: function(e) {
            // make the item available to event listeners
            e.item = this.dataItem(this.select());

            // change theme
            var themeName = e.item.value;
            var commonFile = themeChooser.getCommonUrl();

            if (/material/i.test(themeName) && !/material/i.test(commonFile)) {
                commonFile = "common-material";
            } else if (/bootstrap/i.test(themeName) && !/bootstrap/i.test(commonFile)) {
                commonFile = "common-bootstrap";
            } else if (/fiori/i.test(themeName) && !/fiori/i.test(commonFile)) {
                commonFile = "common-fiori";
            } else if (/office365/i.test(themeName) && !/office365/i.test(commonFile)) {
                commonFile = "common-office365";
            } else if (/nova/i.test(themeName) && !/nova/i.test(commonFile)) {
                commonFile = "common-nova";
            } else if (!/material|bootstrap|fiori|nova/i.test(themeName)) {
                commonFile = "common";
            }

            themeChooser.changeThemePair(themeName, commonFile, true)
                .then(proxy(this.trigger, this, "transition"));
        },
        value: function(value) {
            if (!arguments.length) {
                var dataItem = this.dataItem(this.select());
                return dataItem.name;
            }

            var data = this.dataSource.data();

            for (var i = 0; i < data.length; i++) {
                if (data[i].value == value) {
                    this.select(this.element.find("[data-uid='" + data[i].uid + "']"));
                    break;
                }
            }
        }
    });

    var themeChooserViewModel = kendo.observable({
        themes: [
            { value: "default", name: "Default", colors: ["#ef6f1c", "#e24b17", "#5a4b43"] },
            { value: "blueopal", name: "Blue Opal", colors: ["#076186", "#7ed3f6", "#94c0d2"] },
            { value: "bootstrap", name: "Bootstrap", colors: ["#3276b1", "#67afe9", "#fff"] },
            { value: "silver", name: "Silver", colors: ["#298bc8", "#515967", "#eaeaec"] },
            { value: "uniform", name: "Uniform", colors: ["#666", "#ccc", "#fff"] },
            { value: "metro", name: "Metro", colors: ["#8ebc00", "#787878", "#fff"] },
            { value: "black", name: "Black", colors: ["#0167cc", "#4698e9", "#272727"] },
            { value: "metroblack", name: "Metro Black", colors: ["#00aba9", "#0e0e0e", "#565656"] },
            { value: "highcontrast", name: "High Contrast", colors: ["#b11e9c", "#880275", "#1b141a"] },
            { value: "moonlight", name: "Moonlight", colors: ["#ee9f05", "#40444f", "#212a33"] },
            { value: "flat", name: "Flat", colors: ["#363940", "#2eb3a6", "#fff"] },
            { value: "material", name: "Material", colors: ["#3f51b5", "#283593", "#fff"] },
            { value: "materialblack", name: "Material Black", colors: ["#3f51b5", "#1c1c1c", "#4d4d4d"] },
            { value: "fiori", name: "Fiori", colors: ["#007cc0", "#e6f2f9", "#f0f0f0"] },
            { value: "office365", name: "Office 365", colors: ["#0072c6", "#cde6f7", "#fff"] },
            { value: "nova", name: "Nova", colors: ["#32364c", "#ff4350", "#dfe0e1"] }
        ],
        sizes: [
            { name: "Standard", value: "common" },
            { name: "Bootstrap", value: "common-bootstrap", relativity: "larger" },
            { name: "Material", value: "common-material", relativity: "bold" },
            { name: "Nova", value: "common-nova", relativity: "bold" },
            { name: "Fiori", value: "common-fiori", relativity: "larger" },
            { name: "Office365", value: "common-office365", relativity: "bold" }
        ],

        selectedTheme: window.kendoTheme,
        selectedSize: window.kendoCommonFile
    });

    kendo.ui.plugin(themeChooser);
    window.ThemeChooserViewModel = themeChooserViewModel;

    $(document).ready(function() {
        kendo.bind($("[data-rel=themechooser]"), themeChooserViewModel);
    });

    extend(themeChooser, {
        preloadStylesheet: function(file, callback) {
            var deferred = $.Deferred();
            var element = $("<link rel='stylesheet' media='print' href='" + file + "' />");
            element.appendTo("head");
            deferred.then(callback);

            setTimeout(function() {
                deferred.resolve();
                element.remove();
            }, 100);

            return deferred.promise();
        },

        getCurrentCommonLink: function() {
            return $("head link").filter(function() {
                return (/kendo\.common/gi).test(this.href);
            });
        },

        getCurrentThemeLink: function() {
            return $("head link").filter(function() {
                return (/kendo\./gi).test(this.href) && !(/common|rtl|dataviz|mobile/gi).test(this.href);
            });
        },

        getCommonUrl: function(common) {
            var currentCommonUrl = themeChooser.getCurrentCommonLink().attr("href");

            return currentCommonUrl.replace(skinRegex, "kendo." + common + "$1.$2");
        },

        getThemeUrl: function(themeName) {
            var currentThemeUrl = themeChooser.getCurrentThemeLink().attr("href");

            return currentThemeUrl.replace(skinRegex, "kendo." + themeName + "$1.$2");
        },

        replaceCommon: function(commonName) {
            var newCommonUrl = themeChooser.getCommonUrl(commonName),
                themeLink = themeChooser.getCurrentCommonLink();

            themeChooser.updateLink(themeLink, newCommonUrl);
            cookie("commonFile", commonName, Infinity, "/");
        },

        updateLink: function(link, url) {
            var exampleElement = $("#example");
            var less = window.less;
            var isLess = /\.less$/.test(link.attr("href"));
            var browser = kendo.support.browser;

            link.eq(0).attr("href", url);

            if (isLess) {
                $("head style[id^='less']").remove();

                less.sheets = $("head link[href$='.less']").map(function() {
                    return this;
                });

                less.refresh(true);
            }

            if (exampleElement.length) {
                exampleElement[0].style.cssText = exampleElement[0].style.cssText;
            }
        },

        replaceTheme: function(themeName) {
            var newThemeUrl = themeChooser.getThemeUrl(themeName),
                oldThemeName = $(doc).data("kendoSkin"),
                themeLink = themeChooser.getCurrentThemeLink();

            themeChooser.updateLink(themeLink, newThemeUrl);
            $(doc.documentElement).removeClass("k-" + oldThemeName).addClass("k-" + themeName);

            themeChooser.publishTheme(themeName);
            cookie("theme", themeName, Infinity, "/");
        },

        publishTheme: function(themeName) {
            var themable = ["Chart", "TreeMap", "Diagram", "StockChart", "Sparkline", "RadialGauge", "LinearGauge"];

            if (kendo.dataviz && themeName) {
                for (var i = 0; i < themable.length; i++) {
                    var widget = kendo.dataviz.ui[themable[i]];

                    if (widget) {
                        widget.fn.options.theme = themeName;
                    }
                }
            }

            if (themeName) {
                $(doc).data("kendoSkin", themeName);
            }

            $("#example").trigger("kendo:skinChange");
        },

        currentlyUsing: function(href) {
            if (/common/.test(href)) {
                return themeChooser.getCurrentCommonLink().attr("href") == href;
            } else {
                return themeChooser.getCurrentThemeLink().attr("href") == href;
            }
        },

        animateCssChange: function(options) {
            options = $.extend({ complete: $.noop, replace: $.noop }, options);

            var prefetch = options.prefetch;

            if (!$.isArray(prefetch)) {
                prefetch = [prefetch];
            }

            prefetch = $.grep(prefetch, function(x) {
                return !themeChooser.currentlyUsing(x);
            });

            if (!prefetch.length) {
                return;
            }

            $.when.apply($, $.map(prefetch, themeChooser.preloadStylesheet)).then(function() {
                var example = $("#example");

                example.kendoStop().kendoAnimate(extend({}, animation.hide, {
                    complete: function(element) {
                        if (element[0] == example[0]) {
                            example.css("visibility", "hidden");

                            options.replace();

                            setTimeout(function() {
                                example
                                    .css("visibility", "visible")
                                    .kendoStop()
                                    .kendoAnimate(animation.show);

                                if (prefetch.join(":").indexOf("common") > -1) {
                                    kendo.resize(example, true);
                                }

                                options.complete();
                            }, 100);
                        }
                    }
                }));
            });
        },

        changeCommon: function(commonName, animate) {
            themeChooser.animateCssChange({
                prefetch: themeChooser.getCommonUrl(commonName),
                replace: function() {
                    themeChooser.replaceCommon(commonName);
                }
            });
        },

        changeTheme: function(themeName, animate, complete) {
            // Set transparent background to the chart area.
            extend(kendo.dataviz.ui.themes[themeName].chart, { chartArea: { background: "transparent" } });

            if (animate) {
                themeChooser.animateCssChange({
                    prefetch: themeChooser.getThemeUrl(themeName),
                    replace: function() {
                        themeChooser.replaceTheme(themeName);
                    },
                    complete: complete
                });
            } else {
                themeChooser.replaceTheme(themeName);
            }
        },

        changeThemePair: function(themeName, commonName, animate) {
            var deferred = $.Deferred();

            themeChooser.animateCssChange({
                prefetch: [
                    themeChooser.getCommonUrl(commonName),
                    themeChooser.getThemeUrl(themeName)
                ],
                replace: function() {
                    window.kendoTheme = themeName;
                    window.kendoCommonFile = commonName;
                    themeChooser.replaceCommon(commonName);
                    themeChooser.replaceTheme(themeName);
                },
                complete: function() {
                    deferred.resolve();
                }
            });

            return deferred.promise();
        }
    });

    window.kendoThemeChooser = themeChooser;
})();