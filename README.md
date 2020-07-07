## Requirements
- .NET Core 3.1 SDK
- Postman for testing request

## Developing and running on local
- Go to `src` folder, run `dotnet restore` , `dotnet build`, `dotnet run --project  .\ProfanityCheck.WebAPI\ProfanityCheck.WebAPI.csproj` to start the web project
- Using ngRok to tunnel requests to your localhost ( if you are testing againts Salesforce client)
- Use the postman_collection.json , remember to re-attach text file in POST request

## Deploying ( to Salesforce )
- All triggers and classes should be deployed , starting with classes then triggers

## Configuration ( Salesforce )
- GlobalSettings.apcx 
- Remote Site Settings to authorize Backend API
- Email attachment settings might need some tweak

## Deploying backend API ( to Heroku )
- WIP

## What has been done

- [x] Apex Triggers and Apex Queueable Job 
- [X] Backend API with list of banned words from Github and local file (Banned_Words.txt)
- [x] Tests for Backend API 
- [ ] Deploy-able Backend API
- [ ] Tests for Salesforce Client App
- [ ] propose or even implement the logic to process files with the size bigger than 12MB (you know Salesforce limits, don’t you?).
=> Sounds like Apex Governor Limits , I am thinking should it be about splitting up the file into multiple chunks (being sent with contentDocumentId), then chaining the queuable jobs ? File should be merged on the other end and to be processed
- [x] propose or even implement the logic to notify the user when s/he has attached files with disallowed content,
=> This can be done using either email (implemented) or Notification Bell

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


