require 'excon'
require 'openssl'
require 'json'
require 'base64'
require 'pp'

# normally I'd do anything other than access_key and secret_key as an options hash
def sign_request(access_key, secret_key, ua='enstratus.rb', http_method='GET', path='/geography/Cloud', api_version='2012-02-29')

  timestamp = (Time.now.to_i * 1000)
  sign_path = "/api/enstratus/#{api_version}#{path}"

  parts = []
  parts << access_key << http_method << sign_path << timestamp << ua
  to_sign = parts.join(":")

  digest = OpenSSL::HMAC.digest(OpenSSL::Digest::Digest.new('sha256'), secret_key, to_sign)
  b64auth = Base64.encode64(digest).strip
    
end

access_key = ENV['ES_ACCESS_KEY']
secret_key = ENV['ES_SECRET_KEY']
ua = 'enstratus.rb'
api_version = '2012-02-29'
path = '/geography/Cloud'
http_method='GET'
timestamp = (Time.now.to_i * 1000)

signature = sign_request(access_key, secret_key, ua=ua, http_method=http_method, path=path, api_version=api_version)

connection = Excon.new('https://api.enstratus.com')

headers = {'User-agent' => ua,
           'x-esauth-access' => access_key,
           'x-esauth-timestamp' => timestamp,
           'x-esauth-signature' => signature,
           'x-es-details' => 'basic',
           'Accept' => 'application/json'}

results = connection.get(:path => "/api/enstratus/#{api_version}/geography/Cloud", :headers => headers)

pp results.headers
pp JSON.load(results.body)
