(ns enstratus-api.signature
  "Generates an x-esauth-signature signature"
  (:use [clojure.tools.logging :only (info error)])
  (:use [enstratus-api.crypto :as crypto])
  (:use [enstratus-api.common :as common])
  (:use [clojure.string :only [join]]))

(def ^:private base-path
  "/api/enstratus")

(defn gen-signature
  [opts]
  (let [opts (merge {:access-key (str common/access-key)
		     :secret-key (str common/secret-key)
		     :http-method "GET"
		     :resource-path "geography/Cloud"
		     :api-version "2012-02-29"
		     :user-agent "enstratus.clj"} opts)
        sign-path (apply str (interpose \/ [(str base-path)
			      (str (:api-version opts))
			      (str (:resource-path opts))]))
	to-sign (apply str (interpose \: [(str (:access-key opts))
			      (str (:http-method opts))
			      (str sign-path)
			      (str common/timestamp)
			      (str (:user-agent opts))]))]
	(str (crypto/hmac-sha-256 (str (:secret-key opts)) (str to-sign)))))
