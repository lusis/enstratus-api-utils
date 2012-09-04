(ns enstratus-api.crypto
  (:use [clojure.tools.logging :only (info error)])
  (:import
    (javax.crypto Mac)
    (javax.crypto.spec SecretKeySpec)
    (org.apache.commons.codec.binary Base64)))

(defn hmac-sha-256
  "Calculate HMAC sig"
  [^String key ^String data]
  (let [hmac-sha256 "HmacSHA256"
        signing-key (SecretKeySpec. (.getBytes key) hmac-sha256)
	mac (doto (Mac/getInstance hmac-sha256) (.init signing-key))]
    (do
      (String. (org.apache.commons.codec.binary.Base64/encodeBase64
		(.doFinal mac (.getBytes data)))
		"UTF-8"))))
