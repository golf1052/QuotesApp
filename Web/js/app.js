(function() {
	var foo = "";

	var app = angular.module('quotesApp', ['ngRoute', 'ngResource']);

	Parse.initialize("i0Js9aOJTnuUygn0yQLZyy4L4EWbacRmMoqu2IXC", "96l2LO6K6wwwh6lzWkKIMA5nQeb4h0aiRis2BhwH");

	var Room = Parse.Object.extend("Room");
	var query = new Parse.Query(Room);
	// get the Whale-Cum Black Party Room
	query.get("RN7ATsC59V", {
		success: function(room) {
			// the object was retrieved successfully
			console.log(room)
			var Quote = Parse.Object.extend("Quote");
			query = new Parse.Query(Quote);

			//var quoteIds = query.get("quotes");
			//var quoteIds = room['quotes'];
			
			var quoteIds = room.get("quotes");
			console.log(quoteIds);
			var quotes = [];
			console.log(quoteIds[0].get('id'));
			// query for quotes from room
			for (var i = 0; i < quoteIds.length; i++) {
				query.get(quoteIds[i].get('id'), {
					success: function(quote) {
						quotes[i] = quote;
						console.log(quotes)
					},
					error: function(object, error){
						console.log('ERROR')

					}
				});
			}

		},
		error: function(object, error) {
			// the object was not retrieved successfully
			// error is a Parse.Error with an error code and message
		}
	});




	app.controller('QuoteStreamController', function() {


	});
})();