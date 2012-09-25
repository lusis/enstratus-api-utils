enStratus Java API call example
===============================

## Build

Build the self-contained "fat" jar:

    mvn package

This creates <code>enstratus-java-api-utils-X.Y.Z.jar</code>.

That jar file and environment variables are all you need. The <code>run.sh</code> script is
provided for convenience.

## Setup

Export your API credentials (see 'Manage My API Keys' in the web application for values):

    export ENSTRATUS_API_ACCESS_KEY=12345
    export ENSTRATUS_API_SECRET_KEY=0xba5eba11

Optional, default is <code>https://api.enstratus.com</code>

    export ENSTRATUS_API_ENDPOINT=https://api.enstratus.com

Optional, default is <code>2012-06-15</code>:

    export ENSTRATUS_API_VERSION=2012-06-15

## Run

Listing available clouds is the only action in the example:

    ./run.sh -a listclouds

