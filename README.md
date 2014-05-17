dropBox-crawler
===============

To use:

1. Modify dropbox_crawl.exe.config. Change the key values for ClientId, ClientSecret, and RedirectUri to match the app values in DropBox.
2. Run dropbox_crawl.exe via dropbox_crawl <list of emails>

Assumptions:
1. App has access to entire user's dropbox.

Libraries (open-source)
- Moq: for unit-testing; not used
- Newtonsoft.Json: for parsing json API responses
- log4net: for logging; currently configured for file logging in crawler.log
- RestSharp: for making REST/HTTP requests
- SimpleInjector: for dependency injection

Architecture
I am using volatility-based decomposition guidelines with managers controlling code-flow and calling engines as needed. By isolating logic to engines, it is easier to do unit testing.

TODO LIST
===============

- General
1. add unit and integration test suite
2. refactor dependency injection implementation to take advantage of a SimpleInjector feature called packaging. The Crawler.Business library should expose a method to self-register interfaces. This allows consumers to call the method instead of manually assembling the library components.
3. refactor logging to allow dependency to be injected into classes instead of hardcoded.

- DropBoxEngine
1. handle edge case of multiple files having the same size for both largest and smallest files

- DropBoxManager
1. explore making AnalyzeFolder async (potential optimization) and ensure thread-safe incrementing

- DropBoxProxy
1. handle failure to acquire bearer token due to expired or bad code
2. support /metadata custom parameter values and error codes; e.g. code 406.
3. store, send, and compare hash parameter to optimize /metadata calls. Also handle error 304.
4. ensure UTF-8 encoding with NFC
5. ensure proper date parsing: "%a, %d %b %Y %H:%M:%S %z"
6. add locale support to content responses; set in app.config
7. handle standard DropBox API error codes

- RestProxy
1. add media type support to REST call; e.g. json, xml
2. add API versioning support
3. add Put request support
4. add Patch request support

- Program
1. integrate third-party command line option parser library with string list support
2. add option and email validation
