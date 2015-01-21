﻿(function($) { $.fn.filestyle = function(options) { var settings = { width: 250 }; if (options) { $.extend(settings, options); }; return this.each(function() { var self = this; var wrapper = $("<div id='" + $(self).attr('id') + "_div'>").css({ "width": settings.imagewidth + "px", "height": settings.imageheight + "px", "background": "url(" + settings.image + ") 0 0 no-repeat", "background-position": "right", "display": "inline", "position": "absolute", "overflow": "hidden", "margin-top": "-14px", "margin-left": "5px", "cursor": "pointer" }); if ($('#' + $(self).attr('id') + '_file').size() == 0) { var filename = $('<input class="file" id="' + $(self).attr('id') + '_file">').addClass($(self).attr("class")).css({ "display": "none", "width": settings.width + "px" }); $(self).before(filename); $(self).wrap(wrapper); $(self).css({ "position": "relative", "height": settings.imageheight + "px", "width": settings.width + "px", "display": "inline", "cursor": "pointer", "opacity": "0.0" }); if ($.browser.mozilla) { if (/Win/.test(navigator.platform)) { $(self).css("margin-left", "-142px"); } else { $(self).css("margin-left", "-168px"); }; } else { $(self).css("margin-left", settings.imagewidth - settings.width + "px"); }; $(self).bind("change", function() { }); } }); }; })(jQuery);