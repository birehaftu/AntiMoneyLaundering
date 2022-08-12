///*<<<<<<< .mine*/

// modules/nice_popup.js
//
(function(a)
{
    function c(b)
    {
        target=a(b);
        target.show().css("z-index", "999999");
        a(document).trigger("reveal.overlay", [target,!1])
    }
    jQuery.nicePopup=function(b)
    {
        b.match(/^#/)?c(b):(target_string = '<div class="dialog nice_popup">' + b + "</div>",target = a(target_string),target.appendTo("body").show(),left = a(window).width() / 2 - target.width() / 2,target.css({zIndex:"999999",left:left}),a(document).trigger("reveal.overlay", [target,!1]))
    };
    jQuery.fn.nicePopup=function()
    {
        a(this).live("click.popup", function(b)
                     {
                         b.preventDefault();
                         target_string=a(this).attr("href");
                         (target_string.match(/^#/) || target_string.match(/^./))&&c(target_string)
                     })
    };
    a(document).bind("reveal.overlay", function(b, c, d)
                     {
                         function e()
                         {
                             a("#dropdown_overlay").fadeOut(100, function()
                                                            {
                                                                a(this).remove()
                                                            })
                         }
                         function f()
                         {
                             c.fadeOut(100, function()
                                       {
                                           d&&a(this).remove();
                                           a(document).unbind("keydown.overlay")
                                       })
                         }
                         d==void 0 && (d = !0);
                         if (c == void 0 || a("#dropdown_overlay").length)return!1;
                         a(document).bind("keydown.overlay", function(a)
                                          {
                                              a.keyCode == 27&&f();
                                              e();
                                              return!0
                                          });
                         a("body").append('<div id="dropdown_overlay"></div>');
                         a("#dropdown_overlay").click(function()
                                                      {
                                                          f();
                                                          e();
                                                          return!1
                                                      });
                         a(document).bind("hide.overlay", function()
                                          {
                                              e()
                                          })
                     })
})(jQuery);

//
// modules/project_selector.js
//
var selected_li = !1,keyCodes=[8,13,27,37,38,39,40,46,16,17,18,91,93];

jQuery.fn.projectSelect=function()
{
    function h()
    {
        i();
        $("#project-selector").length?d():$.get("/projects/selector", function(b)
                                                {
                                                    $("body").append(b);
                                                    f=$("#project-selector").height();
                                                    d()
                                                })
    }
    function d()
    {
        $("#project-selector").css({marginLeft:b.offset().left - $("#header .bar").offset().left - 24}).show().height(0).animate({height:f}, 200, function()
                                                                                                                                 {
                                                                                                                                     $(document).trigger("reveal.project_selector", [$(this)])
                                                                                                                                 });
        b.addClass("active").removeClass("loading")
    }
    function e()
    {
        $("#project-selector").animate({height:0}, 200,
                                       function()
                                       {
                                           b.removeClass("active");
                                           $(document).unbind("keydown.project-search");
                                           $(document).unbind("keydown.project-selector");
                                           $(document).unbind("keydown.project_selector");
                                           $(document).unbind("keyup.project_selector");
                                           $("#project-selector input").blur();
                                           $(this).hide()
                                       })
    }
    function i()
    {
        $("#dropdown_overlay").is(":visible")||($(document).bind("keydown.project-selector", function(b)
                                                                 {
                                                                     b.keyCode == 27&&(e(),g());
                                                                     return!0
                                                                 }),$("body").append('<div id="dropdown_overlay"></div>'),$("#dropdown_overlay").click(function()
                                                                                              {
                                                                                                  e();
                                                                                                  g();
                                                                                                  return!1
                                                                                              }))
    }
    function g()
    {
        $("#dropdown_overlay").remove()
    }
    var f = 0,b=$(this);
    b.click(function()
            {
                $(this).hasClass("active")||($(this).addClass("loading"),h());
                return!1
            });
    $(document).bind("reveal.project_selector", function(b, d)
                     {
                         function c()
                         {
                             return document.activeElement == $("input#q")[0] ?!0: !1
                         }
                         function f(a, b)
                         {
                             a.effect("pulsate", {times:2}, 100, function()
                                      {
                                          window.location.pathname=b
                                      })
                         }
                         function e()
                         {
                             $("#project-selector .all li").removeClass("hidden")
                         }
                         selected_li=$(".assigned ul li:visible:first");
                         $("a",
                           selected_li).addClass("hover").addClass("active-project-selector");
                         $(document).bind("keydown.project_selector", function(a)
                                          {
                                              if ($.inArray(a.which, keyCodes) > -1)return $.inArray(a.which, [8,46,37,39]) == -1 && a.preventDefault(),a.stopPropagation(),a.which == 13 ?(c() && (selected_li = $("#project-selector .all li:visible:first"),$("a:last", selected_li).addClass("hover")),f($("a:last", selected_li), $("a:last", selected_li).attr("href"))): a.which == 38 && !c() ?(selected_li.prevAll(":visible:first").length > 0 ?selected_li =
                                                                                                                                                                                                                                                                                                                                                                                                                                                          selected_li.prevAll(":visible:first"): $("input#q").focus(),selected_li.offset().top < 0 && $(".all ul").animate({scrollTop:selected_li.offset().top}, "slow")): a.which == 40 ?(c() ?(selected_li = $(".all ul li:visible:first"),$("input#q").blur()): selected_li.nextAll(":visible:first").length > 0 && (selected_li = selected_li.nextAll(":visible:first")),selected_li.offset().top > $("#project-selector .all").height() && $(".all ul").animate({scrollTop:selected_li.offset().top}, "slow")): a.which == 39 && !c() ?(selected_li.parents("ul:first").find("a").removeClass("hover"),
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     selected_li = d.nextAll(".all ul li:visible:first")): a.which == 37 && !c() && (selected_li.parents("ul:first").find("a").removeClass("hover"),selected_li = d.prevAll(".assigned ul li:visible:first")),selected_li.parents("ul:first").find("a").removeClass("hover"),$("a", selected_li).addClass("hover"),$.inArray(a.which, [8,46,37,39]) > -1 ?!0: !1;else c() || $("input#q").focus(),selected_li.parents("ul:first").find("a").removeClass("hover");
                                              return!0
                                          });
                         $(document).bind("keyup.project_selector", function(a)
                                          {
                                              if ($.inArray(a.which,
                                                            keyCodes) > -1)return $.inArray(a.which, [8,46,37,39]) == -1 && a.preventDefault(),a.stopPropagation(),c() && $("input#q").val() == "" && e(),$.inArray(a.which, [8,46,37,39]) > -1 ?!0: !1;else value = $("input#q").val(),$("#show_archived_projects").is(":not(:checked)") && ($("#show_archived_projects").attr("checked", "checked"),$("#project-selector .all li.status-archived").removeClass("hidden")),value == "" ?e(): ($("#project-selector .all li").addClass("hidden"),$("#project-selector .all").find("li[rel*=" + value.toLowerCase() +
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    "]").removeClass("hidden"));
                                              return!0
                                          })
                     });
    return this
};
//
// modules/tooltip.js
//
jQuery.fn.tooltip=function(c)
{
    function e(a, d, f, b)
    {
        b.appendTo("body").show();
        c?a.bind("mousemove.tooltip", function(a)
                 {
                     page_x=a.pageX - b.width() / 2;
                     page_y=a.pageY - (b.height() + 20);
                     b.css({top:page_y,left:page_x})
                 }):(page_x = a.offset().left + 22 - (b.outerWidth() / 2 - a.width() / 2),page_y = a.offset().top - (b.outerHeight() + 10),css_options = page_x + b.width() > $(window).width() ?{top:page_y,right:"10px"}: {top:page_y,left:page_x},b.css(css_options))
    }
    c==void 0 && (c = !1);
    if (!$(this).length)return this;
    $(this).each(function()
                 {
                     var a =
                     $(this),d=a.attr("title");
                     a.attr("title", "");
                     if (d && d.length > 0)
                     {
                         var c = $("<span class='tip'>" + d + "</span>"),b=null;
                         a.bind("mouseover.tooltip", function(d)
                                {
                                    b=setTimeout(function()
                                                 {
                                                     e(a, d, b, c)
                                                 }, 500)
                                });
                         a.bind("mouseout.tooltip", function()
                                {
                                    c.hide();
                                    clearTimeout(b);
                                    a.unbind("mousemove.tooltip")
                                })
                     }
                 })
};
//
// modules/truncate.js
//
(function(b)
{
    function p(b, a)
    {
        return a.measureText(b).width
    }
    function q(b, a)
    {
        a.text(b);
        return a.width()
    }
    var g = !1,n,j,k;
    b.fn.shorten=function()
    {
        return this.each(function()
                         {
                             var a = b(this),d=a.text(),o=d.length,i,f=b("<span/>").html(h.tail).text(),l={shortened:!1,
                                 textOverflow:!1};
                             i=a.css("float") != "none" ?h.width || a.width(): h.width || a.parent().width();
                             if (i < 0)return!0;
                             a.data("shorten-options", h);
                             this.style.display="block";
                             this.style.whiteSpace="nowrap";
                             if (n)
                             {
                                 var c = b(this),m=document.createElement("canvas");
                                 ctx=m.getContext("2d");
                                 c.html(m);
                                 ctx.font=c.css("font-style") + " " + c.css("font-variant") + " " + c.css("font-weight") + " " + Math.ceil(parseFloat(c.css("font-size"))) + "px " + c.css("font-family");
                                 j=ctx;
                                 k=p
                             }
                             else c = b('<table style="padding:0; margin:0; border:none; font:inherit;width:auto;zoom:1;position:absolute;"><tr style="padding:0; margin:0; border:none; font:inherit;"><td style="padding:0; margin:0; border:none; font:inherit;white-space:nowrap;"></td></tr></table>'),
                                  $td = b("td", c),b(this).html(c),j = $td,k = q;
                             c=k.call(this, d, j);
                             if (c < i)return a.text(d),this.style.visibility = "visible",a.data("shorten-info", l),!0;
                             h.tooltip&&this.setAttribute("title", d);
                             if (g._native && !e.width && (m = b("<span>" + h.tail + "</span>").text(),m.length == 1 && m.charCodeAt(0) == 8230))return a.text(d),this.style.overflow = "hidden",this.style[g._native] = "ellipsis",this.style.visibility = "visible",l.shortened = !0,l.textOverflow = "ellipsis",a.data("shorten-info", l),!0;
                             f=k.call(this, f, j);
                             i-=f;
                             f=i * 1.15;
                             if (c - f >
                                 0 && (f = d.substring(0, Math.ceil(o * (f / c))),k.call(this, f, j) > i))d = f,o = d.length;
                             do o--,d = d.substring(0, o);while(k.call(this, d, j) >= i);
                             a.html(b.trim(b("<span/>").text(d).html()) + h.tail);
                             this.style.visibility="visible";
                             l.shortened=!0;
                             a.data("shorten-info", l);
                             return!0
                         })
    };
    var e = document.documentElement.style;
    "textOverflow" in e ?g = "textOverflow": "OTextOverflow"in e && (g = "OTextOverflow");
    typeof Modernizr != "undefined" && Modernizr.canvastext ?n = Modernizr.canvastext: (e = document.createElement("canvas"),n = !(!e.getContext ||
                                                                                                                                   !(e.getContext("2d") && typeof e.getContext("2d").fillText === "function")));
    b.fn.shorten._is_canvasTextSupported=n;
    b.fn.shorten._native=g;
    b.fn.shorten.defaults={tail:"&hellip;",tooltip:!0}
})(jQuery);


$(document).ready(function()
                  {
                      $(".truncate").shorten();
                      $("input[readonly]").addClass("readonly");
                      $("input[disabled]").addClass("readonly");
                  
              
                      var c;
                      $("ul.tabs .tab").hover(function()
                                              {
                                                  c=$("ul:first", this);
                                                  right_margin=$(this).offset().left + c.width();
                                                  swap_direction=c.width() - $(this).width();
                                                  right_margin > $(document).width()&&c.css({marginLeft:"-" + swap_direction + "px"});
                                                  c.show()
                                              }, function()
                                              {
                                                  $("ul:first", this).hide()
                                              });
                      if ($("#tab-zone").length > 0 && window.location.hash != "")
                      {
                          var b = $("#tab-zone ul.tabs").find("li > a").map(function(a, b)
                                                                            {
                                                                                return $(b).attr("rel")
                                                                            }).get(),a=window.location.hash.substring(1);
                          b.indexOf(a) != -1&&($("#tab-zone ul.tabs li a[rel=" +
                                                 a + "]").addClass("active"),$("#tab-zone ul.tabs li a[rel!=" + a + "]").removeClass("active"),$("#tab-zone ul.areas li.tab").hide(),$("#tab-zone ul.areas li.tab." + a).show())
                      }
                  });
