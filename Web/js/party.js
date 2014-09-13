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
  newBlurbDiv.setAttribute("class", "form-group");
  var newBlurbBox = document.createElement("input");
  newBlurbBox.setAttribute("type", "text");
  newBlurbBox.setAttribute("class", "form-control");
  newBlurbBox.setAttribute("placeholder", "Blurb");
  newBlurbBox.setAttribute("id", makeid());
  newBlurbBox.setAttribute("onfocusout", "addQuoteForm(this.id)");
  newBlurbBox.setAttribute("ng-model", "qsc.quote.blurbs[" + addedFormCount + "]['blurb']");
  newBlurbDiv.appendChild(newBlurbBox);

  // and insert it before the submit button
  quotesForm.insertBefore(newBlurbDiv, submitButton);

  // create new misattributed box
  var newMisattribDiv = document.createElement("div");
  newMisattribDiv.setAttribute("class", "form-group");
  var newMisattribBox = document.createElement("input");
  newMisattribBox.setAttribute("type", "text");
  newMisattribBox.setAttribute("class", "form-control");
  newMisattribBox.setAttribute("placeholder", "Misattributed To");
  newMisattribBox.setAttribute("ng-model", "qsc.quote.blurbs[" + addedFormCount + "]['attributed']");
  var misattribBoxId = makeid();
  newMisattribBox.setAttribute("id", misattribBoxId);
  newMisattribDiv.appendChild(newMisattribBox);

  // and insert it before the submit button
  quotesForm.insertBefore(newMisattribDiv, submitButton);

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


// Will be called whenever the window resizes
$(window).resize(function() {
  reorderTheSideBar();
});
$(document).ready(function() {
  reorderTheSideBar();
})
function reorderTheSideBar() {
  var tabletSize = 990;
  var documentWidth = $('body').innerWidth();
  if (documentWidth <= tabletSize) {
    // If the window is bigger than the tablet size
    // Move the entire sidebar above the quotes scrolling thing
    var $leftHandSide = $('.col-md-8');
    var $rightHandSide = $('.col-md-4');

    $sidebarElement = $('#sidebar');
    $rightHandSide.detach(); // Detach it

    // Add it back inserted before the leftHandSide
    $rightHandSide.insertBefore($leftHandSide);
    $rightHandSide.removeClass('col-md-4').addClass('col-md-8');
    $rightHandSide.css("height", $sidebarElement.innerHeight() + "px");
    $leftHandSide.css("margin-top", "20px");
    $rightHandSide.css('width', $leftHandSide.innerWidth());

  } else if (documentWidth > tabletSize) {
    // Otherwise check if they're in the wrong places..
    // and if the screen is too big.
    // If so, fucking change them.
    var $submitQuoteSection = $("#submitQuoteSection");
    $sidebarElement = $("#sidebar");
    $submitQuoteSection.detach();

    var $quotesListSection = $("#quotesListSection");
    $submitQuoteSection.insertAfter($quotesListSection);
    $submitQuoteSection.removeClass("col-md-8").addClass("col-md-4");
    $submitQuoteSection.css("height", "auto");
    $submitQuoteSection.css('width', "auto");
    // $submitQuoteSection.css("margin-top", "0");
    // $quotesListSection.css("margin-top", "0");
  }
}
