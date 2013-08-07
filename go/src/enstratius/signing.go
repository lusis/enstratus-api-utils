package main

import (
	"crypto/hmac"
	"crypto/sha256"
	"encoding/base64"
	"encoding/json"
	"fmt"
	"github.com/davecgh/go-spew/spew"
	"io/ioutil"
	"net/http"
	"os"
	"strconv"
	"strings"
	"syscall"
	"time"
)

const ES_UA = "gostratus"
const ES_HOST = "https://api.enstratus.com"
const ES_BASE_PATH = "/api/enstratus"
const ES_API_VER = "2012-06-15"

type Cloud struct {
	Name                      string
	ComputeDelegate           string
	WebUrl                    string
	CloudId                   int
	Status                    string
	ComputeEndpoint           string
	PrivateCloud              bool
	LogoUrl                   string
	ComputeSecretKeyLabel     string
	Provider                  string
	Endpoint                  string
	ComputeAccountNumberLabel string
	DocumentationLabel        string
	CloudProviderName         string
	DaseinComputeDelegate     string
	CloudProviderConsoleUrl   string
	CloudProviderLogoUrl      string
}

type CloudList struct {
	Clouds []Cloud
}

type EnstratusError struct {
	// { "error" : { "message" : "Invalid access keys" } }
	Error struct {
		Message string
	}
}

func GetTimeString() string {
	s := time.Now()
	now := s.Unix()*1000 + int64(s.Nanosecond()/1e6)
	return strconv.FormatInt(now, 10)
}

func HmacB64(message string, secret string) string {
	h := hmac.New(sha256.New, []byte(secret))
	h.Write([]byte(message))
	return base64.StdEncoding.EncodeToString(h.Sum(nil))
}

func SignRequest(accessKey string, secretKey string,
	userAgent string, httpMethod string,
	httpPath string) string {
	signpath := strings.Join([]string{ES_BASE_PATH, ES_API_VER, httpPath}, "/")
	timestamp := GetTimeString()
	s := strings.Join([]string{accessKey, httpMethod, signpath, timestamp, userAgent}, ":")
	hash := HmacB64(s, secretKey)
	return hash
}

func main() {
	es_access_key, ok := syscall.Getenv("ES_ACCESS_KEY")
	if ok != true {
		fmt.Printf("You must set the environment variable: ES_ACCESS_KEY\n")
		os.Exit(1)
	}
	es_secret_key, ok := syscall.Getenv("ES_SECRET_KEY")
	if ok != true {
		fmt.Printf("You must set the environment variable: ES_SECRET_KEY\n")
		os.Exit(1)
	}
	url := "https://api.enstratus.com/api/enstratus/2012-06-15/geography/Cloud"
	request, _ := http.NewRequest("GET", url, nil)
	request.Header.Add("accept", "application/json")
	request.Header.Add("x-esauth-access", es_access_key)
	request.Header.Add("x-esauth-timestamp", string(GetTimeString()))
	request.Header.Add("x-es-details", "basic")
	request.Header.Add("user-agent", ES_UA)
	sig := SignRequest(es_access_key, es_secret_key,
		ES_UA, "GET",
		"geography/Cloud")
	request.Header.Add("x-esauth-signature", sig)
	client := &http.Client{}
	response, err := client.Do(request)
	if err != nil {
		fmt.Printf("%s", err)
		os.Exit(1)
	} else {
		defer response.Body.Close()
		contents, err := ioutil.ReadAll(response.Body)
		if err != nil {
			fmt.Printf("%s", err)
			os.Exit(1)
		}
		switch {
		case response.StatusCode >= 400 && response.StatusCode <= 599:
			e := &EnstratusError{}
			err := json.Unmarshal([]byte(contents), &e)
			if err != nil {
				fmt.Printf("JSON Decoding error: %s\n", err)
				os.Exit(1)
			}
			fmt.Printf("%s\n", e.Error.Message)
			os.Exit(1)
		}
		clouds := &CloudList{}
		jsonerr := json.Unmarshal([]byte(contents), &clouds)
		if jsonerr != nil {
			fmt.Printf("JSON Decoding error: %s\n", jsonerr)
			os.Exit(1)
		}
		spew.Dump(clouds)
	}
}
