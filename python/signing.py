import base64, hmac, hashlib
import time
import requests as r
import pprint as pp
import json
"""
The example call we're making in this is getting a list of known clouds:

https://api.enstratus.com/api/enstratus/2012-02-29/geography/Cloud

The reason for this call is that it requires no special permissions
and requires no filters.

These also make for great defaults.
"""

def sign_request(access_key='',secret_key='',ua='enstratus.py',
                http_method='GET',path='geography/Cloud',
                api_version='2012-02-29'):

    timestamp = int(round(time.time() * 1000))
    sign_path = '/api/enstratus/'+api_version+'/'+path

    parts = []
    parts.append(access_key)
    parts.append(http_method)
    parts.append(sign_path)
    parts.append(timestamp)
    parts.append(ua)

    to_sign = ':'.join([str(x) for x in parts])
    digest = hmac.new(secret_key,msg=to_sign,digestmod=hashlib.sha256).digest()
    b64auth = base64.b64encode(digest).decode()

    # I normally like to return a dict here
    # of the params I need to make the request.
    # But this tuple works fine for now
    return (timestamp, b64auth)

if __name__ == '__main__':
    access_key = ''
    secret_key = ''
    url = 'https://api.enstratus.com/api/enstratus/2012-02-29/geography/Cloud'
    ua = 'enstratus.py'
    (timestamp, signature) = sign_request(access_key=access_key, secret_key=secret_key)

    headers = {'x-esauth-access':access_key,
                'x-esauth-timestamp':str(timestamp),
                'x-esauth-signature':signature,
                'x-es-details':'basic',
                'accept':'application/json',
                'user-agent':ua}

    results = r.get(url,headers=headers)

    pp.pprint(results.headers)
    pp.pprint(json.loads(results.content))
