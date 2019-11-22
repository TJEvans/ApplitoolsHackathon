# ApplitoolsHackathon
Applitools Visual AI Rockstar Hackathon

These Projects were build on Visual Studio 2019 and require dotnet core 3.0 sdk installed and chrome updated.

Your APPLITOOLS_API_KEY Environment Variable MUST be set when executing the ApplitooslHackathonVisionAi Tests

Can be run in Visual Studio using the build in Test Explorer or using dotnet test from each project directory.

## Initial Tests the Traditional Way
Time it took to develop the traditional test project? 360 minutes

Lines of code written ~485

Number of Locators Used was Fourteen

Number of field labels or test displayed in the app I need to copy? Seven, Three for the error strings & Four for populating xPaths


## Initial Tests using Vision AI
Time it took to learn AppliTools? Scanned the Tutorial for 30 minutes for the basics then dove right in

Time it took to develop the VisionAi test project?  Took an hour to get Eyes added and work through API key and Viewport params.  Interacting with the Eyes UI took a half day to get comfortable going in blind.  Figuring out how to remove Baselines and update them gave me a lot of trouble.

Lines of code written? ~60 lines of code was added to utilize AppliTools Eyes, but was able to remove various Asserts from the Traditional version

Number of locators, and copied fields was not changed, but I can see how various Visual AI verificiations would allow me to reduce the amount of code


## Updates for Client Version v2
On review of the Intial run I determined one locator, password input, and one client string, no user & pass error, needed to be updated
See [Pull Request](https://github.com/TJEvans/ApplitoolsHackathon/pull/1) for Details

### Bugs identified in V2
The traditional version identified the following
  - Password login field label has changed
  - No Password used during login does NOT trigger an error
  - Recent transactions table amount sort does not work
  - Only One Ad displayed when they are enabled

The traditional version failed to indentify the following bugs, but they were identifed by Eyes
  - No User error formatted badly
  - Login form display name changed to Lougout
  - Missing Instagram Label on the Login Page
  - Canvas Chart had slight differences in the Bars, but the data should have been the same

### Suggested Improvements
  - If a new run of Eyes occurs and there is not an image where there was one in the baseline, we have no way to comment/remark
  - Should be able to do all the same remarking and editing of an image you can do when looking at results as when looking at the baseline directly