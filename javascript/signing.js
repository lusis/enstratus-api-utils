var https = require('https');
var crypto = require('crypto');
var pp = require('prettyjson');

var access_key = process.env.ES_ACCESS_KEY;
var secret_key = process.env.ES_SECRET_KEY;

var http_method = 'GET'
var api_version = '2012-02-29';
var sign_path = '/api/enstratus/'+api_version+'/geography/Cloud';
var ua = 'enstratus.js';

var hmac = crypto.createHmac('sha256', secret_key);
var timestamp = Date.now();

var parts = [access_key, http_method, sign_path, timestamp, ua];
var to_sign = parts.join(':');

hmac.update(to_sign);
var b64auth = hmac.digest('base64');

var es_headers = { 'x-esauth-access': access_key,
		'x-esauth-timestamp': timestamp, 
		'x-esauth-signature': b64auth,
		'x-es-details': 'basic', 
		'accept': 'application/json', 
		'user-agent': ua };


https.get({ host: 'api.enstratus.com', path: sign_path, headers: es_headers }, function(res) {
	console.log(res.headers);

	var resp = '';

	res.on('data', function(d) {
		resp += d;
	});

	res.on('end', function () {
		console.log(pp.render(JSON.parse(resp)));	
	});
}).on('error', function(e) {
	console.error(e);
});
