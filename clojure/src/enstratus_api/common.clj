(ns enstratus-api.common
  (:use clojure.tools.logging))

(defn get-timestamp [] (System/currentTimeMillis))

(def timestamp (get-timestamp))

(def access-key (get (System/getenv) "ES_ACCESS_KEY"))

(def secret-key (get (System/getenv) "ES_SECRET_KEY"))
