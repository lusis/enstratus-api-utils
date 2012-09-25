#!/bin/sh

# Convenience script that looks for the jar file to use in its enclosing directory.
#
# Alternate jar searches: "target/enstratus-java-api-utils*jar", "lib/enstratus-java-api-utils*jar"

relative_dir="`dirname $0`"
script_dir=`cd $relative_dir; pwd`

jar1=`cd $script_dir; ls enstratus-java-api-utils*jar 2>/dev/null`
jar2=`cd $script_dir; ls lib/enstratus-java-api-utils*jar 2>/dev/null`
jar3=`cd $script_dir; ls target/enstratus-java-api-utils*jar 2>/dev/null`

jar=""
if [ -f "$jar1" ]; then
    jar=$jar1
elif [ -f "$jar2" ]; then
    jar=$jar2
elif [ -f "$jar3" ]; then
    jar=$jar3
fi

if [ -z "$jar" ]; then
  echo "Cannot find the jar file."
  exit 1
fi

if [ -z "$DEBUGLAUNCHER" ]; then
  java -jar $jar $*
else
  java $DEBUGLAUNCHER -jar $jar $*
fi
