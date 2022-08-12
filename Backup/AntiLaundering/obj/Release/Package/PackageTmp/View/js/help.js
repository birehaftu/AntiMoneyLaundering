var realtime=false;
var nosuggest=5;

var newsupport=false;
if(newsupport) console.log("testing on new script");

function changeButton(type,active) {

	if(!active) {
	
		switch(type) {
		
			case "question":
				$("#help_question_button span").html("<img src=\""+httpsurl+"images/icons/help3.png\"/> Ask a question");	
				$("#help_question_button").attr('class','button blue');
			break;
			
			case "problem":
				$("#help_problem_button span").html("<img src=\""+httpsurl+"images/icons/exclamation.png\"/> Report a problem");	
				$("#help_problem_button").attr('class','button red');

			break;
			
			case "idea":
				$("#help_idea_button span").html("<img src=\""+httpsurl+"images/icons/lightbulb.png\"/> Suggest an idea");	
				$("#help_idea_button").attr('class','button yellow');
			break;
			
		}
		
		$("#help_"+type+"_button").fadeTo(150,1);
		$("#help_"+type+"_button").attr("href","#");
			
	} else {
	
		if(ishelp) {
			$("#help_"+type+"_button span").html("<img src=\""+httpsurl+"images/icons/page_go.png\"/> Back to Help");
			$("#help_"+type+"_button").attr('class','button green');
		} else {
			$("#help_"+type+"_button").fadeTo(150,0.5);
			$("#help_"+type+"_button").removeAttr("href");
		}
		
	}
	
}

function setView(type) {

	if(type=="help") {
	
		window.querytype=null;
		changeButton("question",false);
		changeButton("problem",false);
		changeButton("idea",false);
		setupForm("reset");
	
		$("#help_support_form").fadeOut(200).queue(function(){
			$("#help_panel").animate({height:window.helpheight},400);
			$(this).css("position","absolute");
			$("#help_overview").slideDown(400);
			$("#help_panel").height("auto");
			$(this).dequeue();
		});
	
	} else {
	
		window.seed = new Date().getTime();
		
		$("#help_panel").height("auto");
		
		if(!$("#help_overview").is(":visible")) {
			$("#help_form_text,#help_form_title").fadeOut(200);
		} else {
			if(!window.helpheight) {
				window.helpheight=$("#help_panel").height();
			}
			if(!window.minheight && $("#help_links").height()>=200) {
				window.minheight=$("#help_links").height();
			} else if(!window.minheight) {
				window.minheight=200;
			}
		}
		
		if(type=="problem") {
			changeButton("question",false);
			changeButton("problem",true);
			changeButton("idea",false);
			$("#help_form_text").queue(function(){
				$("#help_answered span").text("Yes. Problem solved.");
				$("#help_form_title").text("Report a problem.");
				$("#help_form_text").text("We'll see if this problem has already been solved.");
				$("#help_form_text,#help_form_title").fadeIn(200);
				$(this).dequeue();
			});
		}
		if(type=="question") {
			changeButton("question",true);
			changeButton("problem",false);
			changeButton("idea",false);
			$("#help_form_text").queue(function(){
				$("#help_answered span").text("Yes. Question answered.");
				$("#help_form_title").text("Ask a question.");
				$("#help_form_text").text("We'll see if this question has already been answered.");
				$("#help_form_text,#help_form_title").fadeIn(200);
				$(this).dequeue();
			});
		}
		if(type=="idea") {
			changeButton("question",false);
			changeButton("problem",false);
			changeButton("idea",true);
			$("#help_form_text").queue(function(){
				$("#help_answered span").text(" ");
				$("#help_form_title").text("Suggest an idea.");
				$("#help_form_text").text(" ");
				$("#help_form_text,#help_form_title").fadeIn(200);
				$(this).dequeue();
			});
		}
		

		window.querytype=type;
		
		if($("#help_overview").is(":visible")) {
			$("#help_panel").queue(function(){
				$("#help_overview").slideUp(400);
				$("#help_support_form").fadeIn(400).queue(function(){$(this).css("position","relative");$(this).dequeue()});
				$(this).dequeue();
			});
		} else {
			setupForm("reset");
		}
		
		if(type=="idea") {
			$("#help_form_text").queue(function(){
				setupForm("full");
				$(this).dequeue();
			});
		}
	}
	
}


function setupForm(mode,message,faqs,helps) {

	//modes: 'reset','loading','suggest','full','validerror','sysmsg', 'edit'

	//validerror shows only the message box and Submit button (if present), allow edit
	//sysmsg shows: if not full, message and redbutton only, if full message only and no editing.
	//suggest disables editing (if non-realtime) shows edit button and shows suggestions and/or message depending on passed values, with red+green button

	$("#help_support_form").queue(function() {
	
		var fullmode; 
		if ($("#help_ticket textarea#body").is(":visible") || mode=="full") fullmode=true;
		
		if(mode!="suggest") {
			$("#help_more_guides,#help_more_faqs").hide();
		}
		
		// Show/hide blue button & enable/disable editing
		if(!realtime && !(mode=="sysmsg" && fullmode) && !(mode=="loading" && fullmode)) {
		
			$("#help_form_buttons").slideDown(150);
			
			if(mode=="loading") {
				$("#help_prelim_submit").removeAttr("href"); // disable blue button
				$("#help_prelim_submit span").text("please wait...");
				$("#help_ticket textarea").attr("disabled","disabled"); // disable form
			} else if(mode=="suggest") {
				$("#help_prelim_submit").attr("href","#");
				$("#help_prelim_submit span").text("Edit");
			} else {
				$("#help_prelim_submit span").text("Submit"); 
				$("#help_prelim_submit").attr("href","#");
				$("#help_ticket textarea").removeAttr("disabled"); 
			}
			
		} else {
			$("#help_form_buttons").slideUp(150);
			if(mode=="loading") {
				$("#help_ticket textarea").attr("disabled","disabled");
			}
		}
			
		
		// Show/hide red & green buttons
		if(!fullmode && mode!="validerror" && mode!="reset" && mode!="edit" && mode!="loading") { 
		
			$("#help_panel").queue(function() {
			
				$("#help_ticket_buttons").slideDown(150);
				
				if(mode!="sysmsg") {
					$("#help_answered").show();
					$("#help_submit span").text("Nope. Contact support.");
				} else {
					$("#help_answered").hide();
					$("#help_submit span").text("Contact support");
				}
				
				$(this).dequeue();
				
			});
			
		} else {
			$("#help_ticket_buttons").slideUp(150);
		}
		
		var time=300;
		var slineheight=20;
		
		// Show/hide & resize suggest boxes	
		if(mode=="suggest") {
		
			$("#help_suggest").text("Do any of the following match your "+window.querytype+"?").slideDown(150);
			
			if(faqs) {
			
				if(!$("#help_faqs").is(":visible")) {
					$("#help_faqs").height('auto');
					$("#help_faqs").slideDown(time);
				} else {
					$("#help_faqs").show().animate({height:26+(faqs*slineheight)},time).queue(function(){
						$("#help_faqs").height('auto');
						$(this).dequeue()
					});
				}
			}
			
			if(helps) {
				if(!$("#help_guides").is(":visible")) {
					$("#help_guides").height('auto');
					$("#help_guides").slideDown(time);
				} else {
					$("#help_guides").show().animate({height:26+(helps*slineheight)},time).queue(function(){
						$("#help_guides").height('auto');
						$(this).dequeue()
					});
				}
			} 
			

		} else {
			$("#help_suggest").slideUp(time);
			if($("#help_guides").is(":visible")) {
				$("#help_guides").slideUp(time);
			}
			if($("#help_faqs").is(":visible")) {
				$("#help_faqs").slideUp(time);
			}
			$("#help_guides").html("");
			$("#help_faqs").html("");
		}
		
		//Show/hide message/loading box
		if(mode=="loading" || message) {
			if(mode=="loading") {
				$("#help_info").html("<span id='help_loader'>"+message+"</span>");
				$("#help_info").slideDown(300);
			} else {
				$("#help_info").html(message);
				$("#help_info").slideDown(300);
			}
		} else {
			$("#help_info").slideUp(300);
		} 
		
		
		// enable full mode
		if(mode=="full") {
			if(window.querytype=="idea") {
				$("#help_form_text").text("Submit an idea for a great feature or an area we could improve here and it will be posted publicly for other users to view, discuss and vote on. We will review the most popular and the best may be implemented in Clear Books!");
			} else {
				$("#help_form_text").text("Give your "+window.querytype+" a title and enter some details and click \"Submit\" to send it to support.");
			}
			$("#help_ticket label").show(300);
			$("textarea#body").slideDown(300);
			$("textarea#query").animate({height:28});
			if ($("textarea#query").val().length>80) { // maybe can split on the nearest space if poss
				$("textarea#body").val($("textarea#query").val());
				$("textarea#query").val($("textarea#query").val().substring(0,77)+"...");
			}
			$("textarea#query").val($("textarea#query").val().replace(/[\n\r]/g," ")); 
		}
		
		// reset everything
		if(mode=="reset") {
			$("#help_faqs,#help_suggestions,#help_info").html("");
			$("textarea#query,textarea#body").val("");
			if(fullmode) {
				$("#help_ticket label").hide(300);
				$("textarea#body").hide(300);
				$("textarea#query").animate({height:66});	
			}
		}
		
		$(this).dequeue();
	});
	
}



function addSuggestion(type,data) {

	window.maxlength=64; 
	
	switch(type) {
	
		case "guides":
		//data contains:name,url,type
			if(!$("#help_guides").html()) {
				$("#help_guides").html("<h3>Help guides and videos</h3><ul id='help_support_suggestions'></ul>");
			}
			var newtitle=data["title"];
			if(newtitle.length>window.maxlength) {
				newtitle=newtitle.substring(0,window.maxlength)+"...";
			}
			$("#help_guides ul").append("<li style='display:none' class='help_suggestion_"+data["type"]+"'><a target='_blank' title=\""+data["title"]+"\" href='"+data["url"]+"'>"+newtitle+"</a></li>");
			$("#help_guides ul li:last").fadeIn(150);
		break;
		
		case "faqs":
		//data contains: question,answer,type
			if(!$("#help_faqs").html()) $("#help_faqs").html("<h3>Frequently asked questions</h3><ul id=\"help_faq_suggestions\"></ul>");		
			var newtitle=data["question"];
			if(newtitle.length>window.maxlength) {
				newtitle=newtitle.substring(0,window.maxlength)+"...";
			}
			var answer;
			if(data["answer"]) {
				answer=data["answer"].replace(/\r\n|\r/g,"\n");
				answer=answer.replace(/\n{2,}/g,"</p><p class='help_faq_answer'>");
				answer=answer.replace(/\n/g,"<br/>");
			} else {
				answer=""; // seems to be a possibilty of null data reaching this point
					//since it doesnt have html_entities run on it and json_encode can return null on stuff it doesnt like
			}
			$("#help_faqs ul").append("<li style='display:none' class='help_suggestion_"+data["type"]+"'><a href='#' class='help_faq_title' title=\""+data["question"]+"\">"+newtitle+"</a><p class='help_faq_answer'>"+answer+"</p></li>");
			$("#help_faqs ul li:last").fadeIn(150);
		break;
	}
}

function setupSuggestions(results) {
		
	var editmsg=""; var editmsg2="";
//	if(!realtime) {editmsg=" click Edit and "; editmsg2=" clicking Edit and ";}
		
	if (results["guides"].status=="empty") {
		setupForm("validerror","No text entered. Please "+editmsg+"enter some text!");
	} else if (results["guides"].status=="long") {
		setupForm("validerror","Sorry, your "+window.querytype+" is too long. Please "+editmsg+"shorten it!");
	} else if (results["guides"].status=="none" && results["faqs"].status=="none") {
		setupForm("sysmsg","Sorry, we couldn't find any relevant help. You may want to try "+editmsg2+"rewording your "+window.querytype+" or click below to contact support."); // when this happens it seems like you can still edit
	} else if (results["guides"].status=="fail" && results["faqs"].status=="fail") {
		setupForm("sysmsg","Sorry, an error occurred while searching for suggestions. Please click below to contact support."); 
	} else {
		if (results["guides"].status=="ok"||results["guides"].status=="more") {
			if (results["faqs"].status=="ok"||results["faqs"].status=="more") {
				setupForm("suggest",null,results["faqs"].no,results["guides"].no); // both help & faqs
			} else {
				setupForm("suggest",null,null,results["guides"].no); // help but not faqs
			}
		} else if (results["faqs"].status=="ok"||results["faqs"].status=="more") {
			setupForm("suggest",null,results["faqs"].no,null); // faqs but not help
		} else {
			setupForm("sysmsg","Sorry, an unknown error occurred while searching for suggestions. Please click below to contact support."); 
		}
		if (results["faqs"].status=="more") {
			$("#help_more_faqs").fadeIn(200);
		}
		if (results["guides"].status=="more") {
			$("#help_more_guides").fadeIn(200);
		}
	}
	
}


function searchfinished(type,searchresult,no) {

	window.results[type].status=searchresult;
	window.results[type].no=no;

	if(window.results["guides"].status&&window.results["faqs"].status) setupSuggestions(window.results);
}


function doSearch(realtime) {
	window.results={faqs:{status:null,no:null},guides:{status:null,no:null}};
	
	setupForm("loading","Searching for help...");
	
	if(!realtime) {
		window.seed = new Date().getTime();
	}

	$("#help_panel").queue(function() {
		
		window.startpoints={faqs:0,guides:0};
		
		getResult("guides");
		getResult("faqs");
		
		$(this).dequeue();
		
	});
}
	
	

function getResult(type) {
	
	var datanames,searchresult,noresults;
	
	$("#help_more_"+type).hide();

    var supporturl;
    if(newsupport) { supporturl = ""+httpsurl+"common/support/suggest-"+type+"/" }
    else { supporturl = ""+httpsurl+"accounting/generic/support/"; }
	
	
	jQuery.ajax({type:"POST",url:supporturl,data:{ticket:$("#help_ticket textarea").val(),matches:nosuggest,seed:window.seed,mode:"suggest"+type},dataType:"json",success:function(result){
	
		var noresults=0;
		
		if(result) {
			if(!$("#help_ticket textarea").val()||result.too_long||result.error||result.matches.length<=0) { 

				if(!$("#help_ticket textarea").val()) {
					searchresult="empty"; 
				} else if(result.error) {
					searchresult="fail";
				} else if(result.matches.length<=0) {
					searchresult="none";
				} else if (result.too_long) {
					searchresult="long";
				}
					
			} else {
			
				var x;
				searchresult="ok";
				noresults=result.matches.length;
				
				$("#help_"+type).queue(function(){
					for(x=0;x<result.matches.length;x++) {
						addSuggestion(type,result.matches[x]);
					}
					if((window.startpoints[type]+result.matches.length)<result.avail_matches) {
						searchresult="more";
					}
					$(this).dequeue();
				});
				
				
			} 
			if(result.matches.length<result.avail_matches) searchresult="more";
		} else {
			 searchresult="fail";
		}
		
		searchfinished(type,searchresult,noresults);

	},error:function(result,textStatus,errorThrown){ 
	
		searchresult= "fail";
		searchfinished(type,searchresult)
		
	}});
	

}
	

function getMore(id)
{
	$("#"+id).fadeOut(200);
	var type=id.replace(/^help_more_/,"");
	var noguide,nofaq,ajaxmode,datanames;
	
	switch(type) {
	
		case "guides":
			noguide=window.startpoints["guides"];
			nofaq=null;
		break;
		
		case "faqs":
			$(".help_suggestion_faq a.help_faq_title").each(function(index){
				if($(this).text()>window.maxlength) {
					$(this).text()=$(this).text().substring(0,window.maxlength)+"...";
				}
			});
			$(".help_suggestion_faq a.help_faq_title").css('background-image',"url("+httpsurl+"theme/images/icons/plus.png)");
			$(".help_suggestion_faq p").slideUp(150);
			noguide=null;
			nofaq=window.startpoints["faqs"];
		break;
		
	}
	
	window.startpoints[type]+=nosuggest;

    var supporturl;
    if(newsupport) { supporturl = ""+httpsurl+"common/support/suggest-"+type+"/" }
    else { supporturl = ""+httpsurl+"accounting/generic/support/"; }


    jQuery.ajax({type:"POST",url:supporturl,dataType:"json",data:{ticket:$("#help_ticket textarea").val(),matches:nosuggest,seed:window.seed,startat:window.startpoints[type],mode:"suggest"+type},success:function(result){
		if(result) {
			if(result.too_long||result.matches.length<=0) {
				setupForm("suggest","Sorry, an error occurred fetching more suggestions",nofaq,noguide);
			} else if(result.error) {
				setupForm("suggest","Sorry, an error occurred fetching more suggestions",nofaq,noguide); 
			} else {
				if(type=="guides") {
					setupForm("suggest",null,null,window.startpoints[type]+result.matches.length);
				} else if (type=="faqs") {setupForm("suggest",null,window.startpoints[type]+result.matches.length,null); }
				$("#help_panel").queue(function(){
					for(x=0;x<result.matches.length;x++) {
						addSuggestion(type,result.matches[x]);
					//	alert(result.matches[x].answer);
					}
					if((window.startpoints[type]+result.matches.length)<result.avail_matches) {
						$("#"+id).fadeIn(200);
					}
					$(this).dequeue();
				});
			}
		} else {
			setupForm("suggest","Sorry, an error occurred fetching more suggestions",nofaq,noguide); 
		}
	},error:function(result,textStatus,errorThrown){
		setupForm("suggest","Sorry, an error occurred fetching more suggestions",nofaq,noguide); 
	}});		

}

function submitTicket() {

	setupForm("loading","Submitting ticket...");
	
	if(window.querytype=="idea") {

		jQuery.ajax({type:"POST",dataType:"text",url:httpsurl+"/network/ideas/post-idea/",data:{title:$("textarea#query").val(),text:$("textarea#body").val(),mode:"ajax"},success:function(result){
		
			if(!isNaN(result)) {
					setupForm("sysmsg","Your idea has been entered into our system! <a href='../../../network/ideas/view/?idea="+result+"' target='_blank'>Click here</a> to view it. (This opens in a new window or tab and will not affect the current page)");
			} else if (result=="v4") {
					setupForm("validerror","Sorry, your title is too long. Please reduce it to 255 characters or less.");
			} else if (result=="v5") {
					setupForm("validerror","Sorry, your text is too long. Please reduce it to 100573 characters or less.");
			} else if (result=="v6"||result=="v2"||result=="v3") {
					setupForm("validerror","Please enter both a title and some details before you submit your idea!");
			} else {
				setupForm("sysmsg","Sorry, an error (R) occurred submitting your idea - it may or may not have been submitted. ");
			}
			
		} ,error:function(result,textStatus,errorThrown){
		
			setupForm("sysmsg","Sorry, an error (A) occurred submitting your idea - it may or may not have been submitted. ");
			
		}});

	} else {

        var supporturl;
        if(newsupport) { supporturl = ""+httpsurl+"common/support/send-ticket/" }
        else { supporturl = ""+httpsurl+"accounting/generic/support/"; }


        jQuery.ajax({type:"POST",dataType:"json",url:supporturl,data:{title:escape($("textarea#query").val()),ticket:escape($("textarea#body").val()),querytype:window.querytype,mode:"send",page:window.location.href},success:function(result){
		
			if(result.ok) {
				setupForm("sysmsg","Your "+window.querytype+" has been entered into our system. We\'ll be in touch!");
			} else if (!result.ok && result.error && result.allowretry) {
				setupForm("validerror",result.error);
			} else if (!result.ok && result.error) {
				setupForm("sysmsg",result.error);
			} else {
				setupForm("sysmsg","Sorry, an error (P) occurred submitting your "+window.querytype+" to support - it may or may not have been submitted. Please email us at <a href='mailto:help@clearbooks.co.uk'>help@clearbooks.co.uk</a>.");
			}
			
		} ,error:function(result,textStatus,errorThrown){
		
			setupForm("sysmsg","Sorry, an error (A) occurred submitting your "+window.querytype+" to support - it may or may not have been submitted. Please email us at <a href='mailto:help@clearbooks.co.uk'>help@clearbooks.co.uk</a>.");
			
		}});
		
	}

}


// realtime search functions


function rtsqueue(delay) { 

	window.nochecks++;
	var thischeck=window.nochecks;
	setTimeout(function(){
		if(thischeck==window.nochecks) {
			dorts();
		}
	},delay);
	
}

function dorts() {

	var stripval=$('#query').val().replace(/[\.,\n\r\?!<>\/]+/g,'');// strip away the ?!., \n etc because itll ignore those anyway

	if(window.lasttext!=stripval) { 
		if(realtime) {
			dosearch(true);
		}
	}
	
	window.lasttext=stripval;
}


$(document).ready(function(){

	$(".help_suggestion_faq a.help_faq_title").live("click",function(e){
	
		e.preventDefault();
		
		if(!$(this).parent().children("p").is(":visible")) {
		
			$(this).text($(this).attr("title"));
			$(this).css("background-image","url("+httpsurl+"theme/images/icons/minus.png)");
			$(this).parent().children("p").slideDown(150);
			
		} else {
		
			if($(this).text()>window.maxlength) $(this).text()=$(this).text().substring(0,window.maxlength)+"...";
			$(this).css('background-image',"url("+httpsurl+"theme/images/icons/plus.png)");
			$(this).parent().children("p").slideUp(150);
			
		}
		
	});
	
	$("li.help").toggle(function(e){
	
		e.preventDefault();
		$("#help_panel").slideDown(600);
		if(!ishelp) setView("question"); 

	}, function(e){
	
		e.preventDefault();
		setView("help");
		$("#help_panel").slideUp(600);
		
	}); 
		
	$("#help_prelim_submit").click(function(e){
	
		e.preventDefault();
		if($(this).attr("href")=="#") { // if not it's disabled
			if($("#query").is(':disabled')) { // if query box isn't editable
				setupForm("edit","",0,0);
			} else if(!$("#help_ticket textarea#body").is(":visible")) { // not full mode
				doSearch(false);
			} else {
				submitTicket();
			}
		}
		
	});
	
	$("#help_more_guides,#help_more_faqs").click(function(e){
	
		e.preventDefault();
		getMore($(this).attr('id'));
		
	});
			
	$("#help_problem_button,#help_question_button,#help_idea_button").click(function(e){ // If you click really fast it can get out of sync
	
		e.preventDefault();
		
		switch ($(this).attr('id')) {
		
			case "help_question_button":
				if(window.querytype=="question" && ishelp) {
					setView("help");
				} else if (window.querytype!="question") {
					setView("question");
				}
			break;
			
			case "help_problem_button":
				if(window.querytype=="problem" && ishelp) {
					setView("help");
				} else if (window.querytype!="problem") {
					setView("problem");
				}
			break;
			
			case "help_idea_button":
				if(window.querytype=="idea" && ishelp) {
					setView("help");
				} else if (window.querytype!="idea") {
					setView("idea");
				}
			break;
		
		}

	});
	
	$("#help_answered").click(function(e){
	
		e.preventDefault();
		$("ul.right li.help").click();
		setView("help");
		
	});
	
	$("#help_submit").click(function(e){
	
		e.preventDefault();
		setupForm("full");
		
	});
	
	$('#query').keydown(function(e) {
	
		if($("textarea#body").is(":visible") && e.keyCode == 13) {
			e.preventDefault(); // prevent newlines
		} else {
				switch(e.keyCode) {
					case 32: case 13: case 190: case 188: case 49: case 191: // automatically do a search if one of these keys pressed
				//    space   return         .         ,        !         ? <<<<<< Doesnt distinguish between shifted and unshifted keys though
					rtsqueue(500);
					break; // do on a paste as well?
				}
		}
		
	});
	
	$('#query').bind('input', function() {
	
		if($("textarea#body").is(":visible")) {
			$(this).val($(this).val().replace(/[\n\r]/g," ")); // replace existing newlines e.g. if pasted in
			if($(this).val().length>255) {
				$(this).val($(this).val().substring(0,255)); 
			}
		} else {
			rtsqueue(1000); // do a search 1sec after last input change
		}
		
	}); 
	

});




