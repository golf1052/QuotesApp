(function() {
	var app = angular.module('quotesApp', []);

	app.controller('QuoteStreamController', ['$http', function($http) {
			// initialize data structures for room + quote data
			var quoteStream = this;
			quoteStream.room = {};
			quoteStream.quotes = []

			// set API key headers
			$http.defaults.headers.common = {
				'X-Parse-Application-Id' : 'i0Js9aOJTnuUygn0yQLZyy4L4EWbacRmMoqu2IXC',
				'X-Parse-REST-API-Key' : '61B0s035wFn2PhVNX9sL8pPKFro9iIzuSaohyCwL'
			};
			var pUrl = 'https://api.parse.com/1/'; 

			// get room data
			$http.get(pUrl + 'classes/Room/RN7ATsC59V').success(function(data){ // for party
			//$http.get(pUrl + 'classes/Room/SwU8fgU2iE').success(function(data){ // for testing w/ more quotes
				quoteStream.room = data;
				quoteStream.quotes = quoteStream.room['quotes'];
				//console.log(quoteStream.quotes);
				// go through the quote IDs and get their objects
				for (var i = 0; i < quoteStream.quotes.length; i++) {
					!function outer(ii){ // wrapper because for loop
						// get Quote objects from Parse
						$http.get(pUrl + 'classes/Quote/' + quoteStream.quotes[i]['objectId']).success(function(data) {
								quoteStream.quotes[ii] = data;
								for (var j = 0; j < quoteStream.quotes[ii]['blurbs'].length; j++) {
									!function outerj(jj){ // another wrapper
										// get Blurb objects from Parse
										$http.get(pUrl + 'classes/Blurb/' + quoteStream.quotes[ii]['blurbs'][j]['objectId']).success(function(data) {
											// popular Blurb objects to data
											quoteStream.quotes[ii]['blurbs'][jj] = data;
									})
								}(j)
								}
							}
							)
					}(i)
				}

			});




	} ]);
})();