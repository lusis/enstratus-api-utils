# Clojure examples
All of the clojure examples use `leiningen`. Also this was written against Clojure 1.3.

## Signing requests
Set the enviroment variables `ES_ACCESS_KEY` and `ES_SECRET_KEY` to match your enStratus API keys and run:

`lein run`

### Expected output
```
Sep 3, 2012 11:36:00 PM sun.reflect.NativeMethodAccessorImpl invoke0
INFO: {server Apache-Coyote/1.1, x-es-request msp0x1111111, x-es-version 2012-06-15, content-type application/json;charset=UTF-8, transfer-encoding chunked, date Tue, 04 Sep 2012 03:34:52 GMT, connection close}
{:clouds
 [{:privateCloud false,
   :status "ACTIVE",
   :endpoint
   "https://ec2.us-east-1.amazonaws.com,https://ec2.us-west-1.amazonaws.com,https://ec2.eu-west-1.amazonaws.com",
   :computeDelegate "org.dasein.cloud.aws.AWSCloud",
   :webUrl "http://aws.amazon.com",
   :logoUrl "/clouds/aws.gif",
   :name "Amazon Web Services",
   :computeSecretKeyLabel "AWS_SECRET_ACCESS_KEY",
   :cloudId 1,
   :documentationLabel nil,
   :computeEndpoint
   "https://ec2.us-east-1.amazonaws.com,https://ec2.us-west-1.amazonaws.com,https://ec2.eu-west-1.amazonaws.com",
   :computeAccountNumberLabel "AWS_ACCOUNT_NUMBER",
   :statusUrl "http://status.aws.amazon.com/",
   :computeX509CertLabel "AWS_CERTIFICATE",
   :cloudProviderLogoURL "/clouds/aws.gif",
   :computeX509KeyLabel "AWS_PRIVATE_KEY",
   :daseinComputeDelegate "org.dasein.cloud.aws.AWSCloud",
   :provider "Amazon",
   :cloudProviderName "Amazon",
   :computeAccessKeyLabel "AWS_ACCESS_KEY",
   :cloudProviderConsoleURL "http://aws.amazon.com"}]}
```
