# Node examples
All of the node examples use the `prettyjson` library from Rafael de Oleza.

_note that I'm obviously not very well versed in javascript or node. Pull requests welcome_

## Signing requests
Set the environment variables - `ES_ACCESS_KEY` and `ES_SECRET_KEY` to match your enStratus API keys and run:

`node signing.js`

### Expected Output
```
{"Server"=>"Apache-Coyote/1.1",
 "x-es-request"=>"msp0x11111",
 "x-es-version"=>"2012-06-15",
 "Content-Type"=>"application/json;charset=UTF-8",
 "Transfer-Encoding"=>"chunked",
 "Date"=>"Sun, 02 Sep 2012 04:04:06 GMT"}
clouds: 
  - 
    computeDelegate:           org.dasein.cloud.aws.AWSCloud
    webUrl:                    http://aws.amazon.com
    cloudId:                   1
    computeX509KeyLabel:       AWS_PRIVATE_KEY
    status:                    ACTIVE
    computeEndpoint:           https://ec2.us-east-1.amazonaws.com,https://ec2.us-west-1.amazonaws.com,https://ec2.eu-west-1.amazonaws.com
    privateCloud:              false
    logoUrl:                   /clouds/aws.gif
    provider:                  Amazon
    computeSecretKeyLabel:     AWS_SECRET_ACCESS_KEY
    computeX509CertLabel:      AWS_CERTIFICATE
    endpoint:                  https://ec2.us-east-1.amazonaws.com,https://ec2.us-west-1.amazonaws.com,https://ec2.eu-west-1.amazonaws.com
    computeAccountNumberLabel: AWS_ACCOUNT_NUMBER
    documentationLabel:        null
    name:                      Amazon Web Services
    cloudProviderName:         Amazon
    statusUrl:                 http://status.aws.amazon.com/
    computeAccessKeyLabel:     AWS_ACCESS_KEY
    cloudProviderConsoleURL:   http://aws.amazon.com
    daseinComputeDelegate:     org.dasein.cloud.aws.AWSCloud
    cloudProviderLogoURL:      /clouds/aws.gif
```

