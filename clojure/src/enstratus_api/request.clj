(ns enstratus-api.request
  (:use [clojure.tools.logging :only (info error)])
  (:require [clj-http.client :as client])
  (:use [clojure.data.json :as json])
  (:use [clojure.pprint :as pp])
  (:use [enstratus-api.common :as common])
  (:use [enstratus-api.signature :as sig]))

(def es-url "https://api.enstratus.com/api/enstratus/2012-02-29/geography/Cloud")


(def es-headers {"x-esauth-access" common/access-key
                 "x-esauth-timestamp" (str common/timestamp)
                 "x-esauth-signature" (sig/gen-signature {})
                 "x-es-details" "basic"
                 "user-agent" "enstratus.clj"})

(defn req
  []
  (let [resp (client/get es-url 
    	      {:headers es-headers
               :accept :json})]
   (do
      (info (:headers resp))
      (pp/pprint (json/read-json (:body resp))))))
