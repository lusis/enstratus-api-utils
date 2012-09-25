package com.enstratus.api.example;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.http.HttpResponse;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

public class Action {
    public static final String USER_AGENT = "enStratus Java example";
    public static final String DEFAULT_BASEURL = "https://api.enstratus.com";
    public static final String DEFAULT_VERSION = "2012-06-15";

    public static final String ENSTRATUS_API_ENDPOINT = "ENSTRATUS_API_ENDPOINT";
    public static final String ENSTRATUS_API_VERSION = "ENSTRATUS_API_VERSION";
    public static final String ENSTRATUS_API_ACCESS_KEY = "ENSTRATUS_API_ACCESS_KEY";
    public static final String ENSTRATUS_API_SECRET_KEY = "ENSTRATUS_API_SECRET_KEY";

    public static final ObjectMapper mapper = new ObjectMapper();

    protected final String baseUrl;
    protected final String apiVersion;
    protected final String accessKey;
    protected final String secretKey;

    /**
     * Default constructor for using environment variables/defaults. What's documented
     * in the README.
     */
    public Action() {
        final String endpoint = System.getenv(ENSTRATUS_API_ENDPOINT);
        if (endpoint != null && !endpoint.trim().isEmpty()) {
            this.baseUrl = endpoint;
        } else {
            this.baseUrl = DEFAULT_BASEURL;
        }
        final String version = System.getenv(ENSTRATUS_API_VERSION);
        if (version != null && !version.trim().isEmpty()) {
            this.apiVersion = version;
        } else {
            this.apiVersion = DEFAULT_VERSION;
        }
        this.accessKey = System.getenv(ENSTRATUS_API_ACCESS_KEY);
        this.secretKey = System.getenv(ENSTRATUS_API_SECRET_KEY);
    }

    /**
     * Supply keys and URL yourself instead of using environment variables.
     *
     * @param baseUrl e.g. "https://api.enstratus.com"
     * @param apiVersion e.g.  "2012-06-15"
     * @param accessKey access key
     * @param secretKey raw secret key
     */
    public Action(String baseUrl, String apiVersion, String accessKey, String secretKey) {
        this.baseUrl = baseUrl;
        this.apiVersion = apiVersion;
        this.accessKey = accessKey;
        this.secretKey = secretKey;
    }

    protected String textFromResponse(HttpResponse response) throws IOException {
        if (response == null) {
            return null;
        } else {
            return inputStreamToString(response.getEntity().getContent());
        }
    }

    protected String inputStreamToString(InputStream is) throws IOException {
        String line = "";
        final StringBuilder all = new StringBuilder();
        final BufferedReader rd = new BufferedReader(new InputStreamReader(is));
        while ((line = rd.readLine()) != null) {
            all.append(line);
        }
        return all.toString();
    }
}
