all:
	@mkdir -p bin/
	@go get -d -v ./...
	@go install enstratius

clean:
	@rm -rf bin/ pkg/ src/github.com/

.PHONY: all clean
