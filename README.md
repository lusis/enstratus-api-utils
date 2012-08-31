# enStratus API Utils

These are just a few bits of example code and notes around how to work with the enStratus API.

# What's in here?
Just a toc of sorts

## Signing examples
The enStratus API is fairly complex. The thing that trips most people up, however, is the signing process.

A few key points to remember are:

- The `path` you sign excludes any filters you might want to pass
- User-agent in the request must match the one in the signature
- The timestamp used in the signature is millseconds not Unix time

## API versioning
The enStratus API is versioned by release date. Some functionality might not exist in the version you're trying. Always make sure you're referencing the correct version's documentation.

## IDs
IDs that you see in the enStratus console don't always match up with the ID you're expected to pass.
A good example of this is the `Snapshot` API. The `volumeId` in the console looks something like this:

`j-111-222`

however the value you pass in the snapshot call would be:

`111222`

## Content type is required
You can use either XML or JSON but you have to set the header appropriately.
