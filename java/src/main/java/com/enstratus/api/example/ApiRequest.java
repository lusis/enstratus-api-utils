package com.enstratus.api.example;

import org.apache.http.Header;
import org.apache.http.HttpException;
import org.apache.http.HttpRequest;
import org.apache.http.HttpRequestInterceptor;
import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpDelete;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpHead;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.methods.HttpPut;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.protocol.HttpContext;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.MalformedURLException;
import java.net.URL;

/**
 * Represents one request. Creates a new HttpClient for every call: this is inefficient
 * for large numbers of simultaneous calls.
 */
public class ApiRequest {

    private final String path;
    private final String accessKey;
    private final String secretKey;
    private final String userAgent;
    private final boolean json;
    private final String details;
    private final HttpMethod method;
    private final HttpUriRequest request;

    /**
     * @param method GET, POST, PUT, DELETE, HEAD
     * @param apiCall call e.g. "geography/Cloud"
     * @param version version e.g. "2012-02-29"
     * @param baseUrl "https://api.enstratus.com"
     * @param accessKey access key
     * @param secretKey raw secret key
     * @param userAgent desired user agent, may be null for default
     * @param json true if json is desired, false for xml
     * @param details 'none', 'basic', or 'extended'
     * @param requestBody POST or PUT may include request body, or null
     */
    public ApiRequest(HttpMethod method, String apiCall, String version, String baseUrl, String accessKey, String secretKey, String userAgent, boolean json, String details, String requestBody) {
        this.path = "/api/enstratus/" + strCheck(version, "version") + '/' + strCheck(apiCall, "apiCall");
        this.accessKey = strCheck(accessKey, "accessKey");
        this.secretKey = strCheck(secretKey, "secretKey");
        this.userAgent = userAgent;
        this.json = json;
        this.details = detailsCheck(details);
        this.method = methodCheck(method);
        final String url = strCheck(baseUrl, "baseUrl") + this.path;
        urlCheck(url);
        try {
            this.request = createRequest(method, url, requestBody);
        } catch (UnsupportedEncodingException e) {
            throw new IllegalArgumentException(e.getMessage(), e);
        }
    }

    /**
     * Returns call result (including error statuses) or throws an exception for serious issues
     * @return result
     */
    public HttpResponse call() throws Exception {
        final DefaultHttpClient httpclient = new DefaultHttpClient();
        final String timestamp = Long.toString(System.currentTimeMillis());
        final String toSign = accessKey + ':' + method.toString() + ':' + path + ':' + timestamp + ':' + userAgent;
        final String signature = RequestSignature.sign(secretKey.getBytes(), toSign);
        addHeaders(httpclient, signature, timestamp);
        return httpclient.execute(request);
    }

    public String getUrl() {
        return this.request.getURI().toASCIIString();
    }


    // -----------------------------------------------------------------------------------------
    // IMPL
    // -----------------------------------------------------------------------------------------

    private void addHeaders(DefaultHttpClient httpclient, final String signature, final String timestamp) {
        httpclient.addRequestInterceptor(new HttpRequestInterceptor() {
            public void process(final HttpRequest request, final HttpContext context)
                    throws HttpException, IOException {
                if (userAgent != null) {
                    final Header[] uaHeaders = request.getHeaders("User-Agent");
                    for (Header uaHeader : uaHeaders) {
                        request.removeHeader(uaHeader);
                    }
                    request.addHeader("User-Agent", userAgent);
                }
                if (json) {
                    request.addHeader("Accept", "application/json");
                } else {
                    request.addHeader("Accept", "application/xml");
                }
                request.addHeader("x-es-details", details);
                request.addHeader("x-es-with-perms", "false");
                request.addHeader("x-esauth-access", accessKey);
                request.addHeader("x-esauth-signature", signature);
                request.addHeader("x-esauth-timestamp", timestamp);
            }
        });
    }

    private static HttpUriRequest createRequest(HttpMethod httpMethod, String fullUrl, String body) throws UnsupportedEncodingException {
        switch (httpMethod) {
            case GET:
                return new HttpGet(fullUrl);
            case POST:
                final HttpPost post = new HttpPost(fullUrl);
                if (body != null) {
                    post.setEntity(new StringEntity(body));
                }
                return post;
            case PUT:
                final HttpPut put = new HttpPut(fullUrl);
                if (body != null) {
                    put.setEntity(new StringEntity(body));
                }
                return put;
            case DELETE:
                return new HttpDelete(fullUrl);
            case HEAD:
                return new HttpHead(fullUrl);
            default:
                throw new IllegalStateException("Unknown HTTP method: " + httpMethod);
        }
    }

    private static String strCheck(String val, String name) {
        if (val == null || val.trim().isEmpty()) {
            throw new IllegalArgumentException(name + " is empty or missing");
        }
        return val;
    }

    private static HttpMethod methodCheck(HttpMethod val) {
        if (val == null) {
            throw new IllegalArgumentException("no http method");
        }
        return val;
    }

    private static String detailsCheck(String val) {
        if ("none".equalsIgnoreCase(val)) {
            return "none";
        } else if ("basic".equalsIgnoreCase(val)) {
            return "basic";
        } else if ("extended".equalsIgnoreCase(val)) {
            return "extended";
        } else {
            throw new IllegalArgumentException("Unknown 'details' value: " + val);
        }
    }

    private static void urlCheck(String val) {
        try {
            final URL test = new URL(val);
        } catch (MalformedURLException e) {
            throw new IllegalArgumentException("Invalid URL '" + val + "': " + e.getMessage());
        }
    }
}
