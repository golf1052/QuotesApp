// IT AINT A PARTY IN HERE CAUSE EVERYTHING IS BROKEN AND FUCKED

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
  var misattribBoxId = makeid();
  newMisattribBox.setAttribute("id", misattribBoxId);
  newMisattribDiv.appendChild(newMisattribBox);

  // and insert it before the submit button
  quotesForm.insertBefore(newMisattribDiv, submitButton);
}

function checkAdd() {

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
