(function() {
	var app = angular.module('quotesApp', ['ngRoute']);
	var prodRoom = 'RN7ATsC59V'; // Party Room
	var prodUser = 'wwQKl03DKN'; // Party User

	app.controller('QuoteStreamController', ['$http', function($http) {
			// initialize data structures for Room and Quote data
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
			$http.get(pUrl + 'classes/Room/' + prodRoom).success(function(data){ // for party
			//$http.get(pUrl + 'classes/Room/SwU8fgU2iE').success(function(data){ // for testing w/ more quotes
				quoteStream.room = data;
				quoteStream.quotes = quoteStream.room['quotes'];
				// go through the quote IDs and get their objects
				for (var i = 0; i < quoteStream.quotes.length; i++) {
					!function outer(ii){ // wrapper because for loop
						// get Quote objects from Parse
						$http.get(pUrl + 'classes/Quote/' + quoteStream.quotes[i]['objectId']).success(function(data) {
								quoteStream.quotes[quoteStream.quotes.length - 1 - ii] = data;
							});
					}(i)
				}

			});




	} ]);

	app.controller('QuoteSubmitController', ['$scope', '$route', '$window', '$http', function($scope, $route, $window, $http) {
		// initialize quote data with appropriate Room and User
		this.quote = {
			"room" : { "__type"    : "Pointer",
				"className" : "Room",
				"objectId"  : prodRoom,
			},
			"submitter" : { "__type"    : "Pointer",
				"className" : "_User",
				"objectId"  : prodUser,
			},
			"blurbs" : [
				// { 'blurb' : 'foo', 'attributed' : 'bar' }
			]
		};

		this.parseQuote = {};
		this.room = {};

		$http.defaults.headers.common = {
			'X-Parse-Application-Id' : 'i0Js9aOJTnuUygn0yQLZyy4L4EWbacRmMoqu2IXC',
			'X-Parse-REST-API-Key' : '61B0s035wFn2PhVNX9sL8pPKFro9iIzuSaohyCwL'
		};
		var pUrl = 'https://api.parse.com/1/'; 

		this.addQuote = function() {
			// submit quote to parse
			$http.post(pUrl + 'classes/Quote', this.quote).success(function(data){
				this.quote = data;
				var quoteVar = this.quote;

				$http.get(pUrl + 'classes/Room/' + prodRoom).success(function(data){
					this.room = data;
					this.pushData = {
						'__type' : "Pointer",
						'className' : 'Quote',
						'objectId' : quoteVar['objectId'],
					}
					this.room['quotes'].push(this.pushData);
										console.log(this.room);
					$http.put(pUrl + 'classes/Room/' + this.room['objectId'], this.room).success(function(data){
						this.reponse = data;
						$window.location.reload();
					});
				});

				// then reset Quote object to defaults
				this.quote = {
						"room" : { "__type"    : "Pointer",
							"className" : "Room",
							"objectId"  : prodRoom,
						},
						"submitter" : { "__type"    : "Pointer",
							"className" : "_User",
							"objectId"  : prodUser,
						},
						"blurbs" : [
							// { 'blurb' : 'foo', 'attributed' : 'bar' }
						]
					};
		});

	} } ]);
})();