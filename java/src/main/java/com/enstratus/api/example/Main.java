package com.enstratus.api.example;

import joptsimple.OptionParser;
import joptsimple.OptionSet;

public class Main {
    public static final String USAGE = "Usage: -a listclouds";

    public static void main(String[] args) throws Exception {
        final OptionParser parser = new OptionParser( "a:" );
        final OptionSet options = parser.parse(args);
        if (!options.hasArgument("a")) {
            System.err.println("Action required: -a");
            System.err.println(USAGE);
            System.exit(1);
        }
        final String action = (String)options.valueOf("a");

        if ("listclouds".equalsIgnoreCase(action)) {
            new ListClouds().list();
        } else {
            System.err.println("Unknown action: " + action);
            System.err.println(USAGE);
            System.exit(1);
        }
    }
}
