# Python examples
All of the python examples use the `requests` library from Kenneth Rietz.

## Signing requests
Set the enviroment variables `ES_ACCESS_KEY` and `ES_SECRET_KEY` to match your enStratus API keys and run:

`python signing.py`

### Expected Output
```python
{'content-type': 'application/json;charset=UTF-8',
 'date': 'Fri, 31 Aug 2012 21:06:29 GMT',
 'server': 'Apache-Coyote/1.1',
 'transfer-encoding': 'chunked',
 'x-es-request': 'msp0x111111',
 'x-es-version': '2012-06-15'}
{u'clouds': [{u'cloudId': 1,
              u'cloudProviderConsoleURL': u'http://aws.amazon.com',
              u'cloudProviderLogoURL': u'/clouds/aws.gif',
              u'cloudProviderName': u'Amazon',
              u'computeAccessKeyLabel': u'AWS_ACCESS_KEY',
              u'computeAccountNumberLabel': u'AWS_ACCOUNT_NUMBER',
              u'computeDelegate': u'org.dasein.cloud.aws.AWSCloud',
              u'computeEndpoint': u'https://ec2.us-east-1.amazonaws.com,https://ec2.us-west-1.amazonaws.com,https://ec2.eu-west-1.amazonaws.com',
              u'computeSecretKeyLabel': u'AWS_SECRET_ACCESS_KEY',
              u'computeX509CertLabel': u'AWS_CERTIFICATE',
              u'computeX509KeyLabel': u'AWS_PRIVATE_KEY',
              u'daseinComputeDelegate': u'org.dasein.cloud.aws.AWSCloud',
              u'documentationLabel': None,
              u'endpoint': u'https://ec2.us-east-1.amazonaws.com,https://ec2.us-west-1.amazonaws.com,https://ec2.eu-west-1.amazonaws.com',
              u'logoUrl': u'/clouds/aws.gif',
              u'name': u'Amazon Web Services',
              u'privateCloud': False,
              u'provider': u'Amazon',
              u'status': u'ACTIVE',
              u'statusUrl': u'http://status.aws.amazon.com/',
              u'webUrl': u'http://aws.amazon.com'}]}
```
