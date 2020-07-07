## Requirements
- .NET Core 3.1 SDK
- Docker to run Dockerfile locally
- Heroku CLI if needed to deploy from local machine
- Postman for testing request

## Developing and running locally
- Go to `src` folder, run `dotnet restore` , `dotnet build`, `dotnet run --project  .\ProfanityCheck.WebAPI\ProfanityCheck.WebAPI.csproj` to start the web project
- Using ngRok to tunnel requests to your localhost ( if you are testing againts Salesforce client)
- Use the postman_collection.json , remember to re-attach text file in POST request

## Deploying backend API ( to Heroku )
- Currently using GitHub Actions for deployment , use `.github/workflows/main.yml` as reference
- HEROKU_API_KEY is found from https://dashboard.heroku.com/account , and should be set as secret
- Any changes from master branch should invoke the Action

## Deploying ( to Salesforce )
- All triggers and classes should be deployed , starting with classes then triggers

## Configuration ( Salesforce )
- GlobalSettings.apcx needs to set 
- Remote Site Settings to authorize Backend API
- Email attachment settings might need some tweak

## What has been done
- [x] Apex Triggers and Apex Queueable Job 
- [X] Backend API with list of banned words from Github and local file (Banned_Words.txt)
- [x] Tests for Backend API 
- [X] Deploy-able Backend API , with Github Actions and Dockerfile
- [ ] Tests for Salesforce Client App
- [x] propose or even implement the logic to notify the user when s/he has attached files with disallowed content,
=> This can be done using either email (implemented) or Notification Bell
- [ ] propose or even implement the logic to process files with the size bigger than 12MB (you know Salesforce limits, don’t you?).
=> Sounds like Apex Governor Limits/ Heap size limits , I am thinking should it be about splitting up the file into multiple chunks (being sent with contentDocumentId), then chaining the queuable jobs ? File should be merged on the other end and to be processed
## How to test things out
- Create a new Case/Standard Object
- Attach a text file that has/no banned words
- Upload a new version with banned words
- File should be pernamently deleted, even from Recycle Bin
- Owner should receive an email with violated file

## What were challenging
- Figuring out what is the right Entity Trigger to use ( ContentDocument,ContentVersion, ContentDocumentLink) 
- ContentVersion.VersionData needs to be fetched at after insert
- Trigger.new does not show everything
https://salesforce.stackexchange.com/questions/268436/trigger-on-content-document-to-retrieve-the-body
- Uploading files using Apex was a pain, maybe there was a better approach


## What can be improved further
- Notification Bell to user instead of email 
- Bulk handling of files , not one file per notification email


