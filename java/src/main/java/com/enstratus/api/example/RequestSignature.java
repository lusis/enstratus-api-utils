package com.enstratus.api.example;

import org.apache.commons.codec.binary.Base64;

import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;

public class RequestSignature {
    public static String sign(byte[] key, String stringToSign) throws Exception {
        final Mac mac = Mac.getInstance("HmacSHA256");
        mac.init(new SecretKeySpec(key, "HmacSHA256"));
        return new String(Base64.encodeBase64(mac.doFinal(stringToSign.getBytes("utf-8"))));
    }
}
