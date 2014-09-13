// IT AINT A PARTY IN HERE CAUSE EVERYTHING IS BROKEN AND FUCKED
var addedFormCount = 1;
function addQuoteForm(x) {
  var originalBlurbBox = document.getElementById(x);

  if (originalBlurbBox.value == "") {
    return;
  }

  var originalBlurbForm = originalBlurbBox.parentNode;
  var originalMisattribForm = originalBlurbForm.nextElementSibling;

  // get quotes form
  var quotesForm = document.getElementById("addQuotesForm");

  // get submit button
  var submitButton = document.getElementById("submitButtonDiv");

  if (submitButton.previousElementSibling != originalMisattribForm) {
    return;
  }

  // create new blurb box
  var newBlurbDiv = document.createElement("div");
  var newBlurbBox = document.createElement("input");

  newBlurbDiv.setAttribute("class", "form-group");

  newBlurbBox.setAttribute("type", "text");
  newBlurbBox.setAttribute("class", "form-control");
  newBlurbBox.setAttribute("placeholder", "Blurb");
  newBlurbBox.setAttribute("id", makeid());
  newBlurbBox.setAttribute("onfocusout", "addQuoteForm(this.id)");
  newBlurbBox.setAttribute("ng-model", "qsc.quote.blurbs[" + addedFormCount + "].blurb");
  
  newBlurbDiv.appendChild(newBlurbBox);

  // create new misattributed box
  var misattribBoxId = makeid();
  
  var newMisattribDiv = document.createElement("div");
  var newMisattribBox = document.createElement("input");
  
  newMisattribDiv.setAttribute("class", "form-group");
  newMisattribBox.setAttribute("type", "text");
  newMisattribBox.setAttribute("class", "form-control");
  newMisattribBox.setAttribute("placeholder", "Misattributed To");
  newMisattribBox.setAttribute("ng-model", "qsc.quote.blurbs[" + addedFormCount + "].attributed");
  newMisattribBox.setAttribute("id", misattribBoxId);
  
  newMisattribDiv.appendChild(newMisattribBox);

  // and insert new blurb div before the submit button
  quotesForm.insertBefore(newBlurbDiv, submitButton);

  // and insert new misattrib div before the submit button
  quotesForm.insertBefore(newMisattribDiv, submitButton);

  // I hate Sanders for using vanilla js instead of jQuery
  resizeTheInputFields();

  //Increment the counter for the boxes added
  addedFormCount += 1;
}


// wow such found on stackoverflow
// http://stackoverflow.com/questions/1349404/generate-a-string-of-5-random-characters-in-javascript
function makeid() {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for( var i=0; i < 5; i++ )
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}

//Will be called on page load
$(document).ready(function() {
  reorderTheSideBar();
});

// Will be called whenever the window resizes
$(window).resize(function() {
  reorderTheSideBar();
});

function reorderTheSideBar() {
  var tabletSize = 990;
  var documentWidth = $('body').innerWidth();
  var $sidebarElement = $("#sidebar");

  var $quotesListSection = $('#quotesListSection');
  var $submitQuoteSection = $('#submitQuoteSection');
  if (documentWidth <= tabletSize) {
    // Aesthetic, keeping the two sections seperate when vertical
    $quotesListSection.css("margin-top", "20px");
    
    $submitQuoteSection.detach(); // Detach it

    // Add it back inserted before the quotesListSection
    $submitQuoteSection.insertBefore($quotesListSection);

    // Make it take up the entire row
    $submitQuoteSection.removeClass('col-md-4').addClass('col-md-8');
    $submitQuoteSection.css("height", $sidebarElement.innerHeight() + "px");
    $submitQuoteSection.css('width', $quotesListSection.innerWidth());  
  } else if (documentWidth > tabletSize) {
    $submitQuoteSection.detach(); // Detach it
    
    $submitQuoteSection.insertAfter($quotesListSection);
    $submitQuoteSection.removeClass("col-md-8").addClass("col-md-4");
    
    // Clear these values and let bootstrap do it's job
    $submitQuoteSection.css('width', "");
    $submitQuoteSection.css('height', "");
  }
}

function resizeTheInputFields() {
  var $sidebarElement = $('#sidebar');
  var $submitQuoteSection = $('#submitQuoteSection');

  $submitQuoteSection.css("height", $sidebarElement.innerHeight());
}