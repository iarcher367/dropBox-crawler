dropBox-crawler
===============

Directions:  
1.  Open DropBoxCrawler.sln and build Crawler.Console.  
2.  In the program directory, modify dropbox_crawl.exe.config. Change the key values for ClientId, ClientSecret, and RedirectUri to match the app values in DropBox.  
3.  Use the command line to run the following command:  
`dropbox_crawl <space separated list of emails>`  
e.g. `dropbox_crawl foo@gmail.com bar@gmail.com`

Note: The crawler assumes that it has access to the account's entire dropbox.  

The crawler implementation follows volatility-based decomposition guidelines with managers controlling code-flow and calling engines that encapsulate the desired business logic.  

Open-source libraries:  
1.  log4net: for logging; currently outputs to crawler.log at level INFO. Only set to DEBUG if you want to see **everything**.  
2.  Moq: for unit-testing  
3.  Newtonsoft.Json: for parsing json API responses  
4.  RestSharp: for making REST/HTTP requests  
5.  SimpleInjector: for dependency injection  

**TODO LIST**

- General
  1.  add unit and integration test suite
  2.  refactor dependency injection implementation to take advantage of the SimpleInjector packaging feature. The Crawler.Business library should expose a method to self-register interfaces. This allows consumers to call the method instead of manually assembling the library components.
  3.  refactor logging implementation to inject log dependency instead of hardcoding it

- AnalyticsEngine
  1.  handle edge case of multiple files having the same size for both largest and smallest files

- DropBoxManager
  1.  making AnalyzeFolder() async may speed up processing for large accounts; ensure thread-safe incrementing

- DropBoxProxy
  1.  handle failure to acquire bearer token due to expired or bad code
  2.  support /metadata custom parameter values and error codes; e.g. code 406.
  3.  store, send, and compare hash parameter to optimize /metadata calls; also handle error 304.
  4.  ensure UTF-8 encoding with NFC
  5.  ensure proper date parsing: "%a, %d %b %Y %H:%M:%S %z"
  6.  add locale support to content responses; set in app.config
  7.  handle standard DropBox API error codes

- Program
  1.  integrate third-party command line option parser library with string list support
  2.  add option and email validation

- RestProxy
  1.  add API versioning support
  2.  add Put request support
  3.  add Patch request support
  4.  add media type support to REST call; e.g. json, xml
