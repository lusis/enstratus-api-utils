package com.enstratus.api.example;

import org.apache.http.HttpResponse;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

public class ListClouds extends Action {

    public void list() throws Exception {
        final ApiRequest req =
                new ApiRequest(HttpMethod.GET, "geography/Cloud", this.apiVersion, this.baseUrl,
                               this.accessKey, this.secretKey, USER_AGENT, true, "basic", null);

        System.out.println("Calling: " + req.getUrl());
        final HttpResponse response = req.call();
        System.out.println("Result:  " + response.getStatusLine());
        if (response.getStatusLine().getStatusCode() != 200) {
            System.err.println("Problem:" + textFromResponse(response));
        } else {
            printClouds(response);
        }
    }

    // Jackson has other ways of intaking JSON, this is the most manual way:
    void printClouds(HttpResponse response) throws IOException {
        final Map<String,Object> cloudList = mapper.readValue(textFromResponse(response), Map.class);
        if (!cloudList.containsKey("clouds")) {
            System.err.println("Expected 'clouds' envelope in the response JSON");
            return;
        }
        final Map<Integer,String> results = new HashMap<Integer, String>();
        final Iterable clouds = (Iterable) cloudList.get("clouds");
        for (Object o: clouds) {
            final Map<String,Object> cloud = (Map<String,Object>)o;
            final Integer cloudId = (Integer) cloud.get("cloudId");
            final String cloudProviderName = (String) cloud.get("cloudProviderName");
            results.put(cloudId, cloudProviderName);
        }

        final int num = results.size();
        if (num == 0) {
            System.out.println("No clouds.");
        } else if (num == 1) {
            System.out.println("One cloud:");
        } else {
            System.out.println(results.size() + " clouds:");
        }
        for (Integer id: results.keySet()) {
            final StringBuilder sb = new StringBuilder();
            if (id < 10) {
                sb.append("  ");
            } else if (id < 100) {
                sb.append(" ");
            }
            sb.append(id);
            sb.append(": ");
            sb.append(results.get(id));
            System.out.println(sb.toString());
        }
    }

    // for IDE quick-access/debug
    public static void main(String[] args) throws Exception {
        new ListClouds().list();
    }
}
