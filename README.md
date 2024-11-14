[![NuGet stable version](https://badgen.net/nuget/v/Bale.Net)](https://www.nuget.org/packages/Bale.Net)
![Nuget](https://img.shields.io/nuget/dt/Bale.Net)

# Bale.Net

This is a Simple [Bale](https://bale.ai/) bot Api Wrapper for dotnet core.

# Install

you can get this package using [nuget](https://www.nuget.org/packages/Bale.Net/):

`dotnet add package Bale.Net`

or

`NuGet\Install-Package Bale.Net`

# How To Use

first you have to instantiate the client and pass dawn your bot token(you can get it from @botfather):

```csharp
var client = new BaleClient("your token");
```

after that you have access to 6 interfaces:

```csharp
IAttachments, IChats, IMessages, IPayments, IUpdates, IUsers
```

just like the [api](https://dev.bale.ai/api), and you can just use all the methods like this:

```csharp
//send message
var message = await _client.Messages.SendMessageAsync(MyId, Text);

//edit message
var message = await _client.Messages.EditMessageTextAsync(MyId, messageId, $"new txt");

//delete message
var message = await _client.Messages.DeleteMessageAsync(MyId, message.MessageId);
```

**Note** that in order to send any kind of Media(video, audio...etc) you need to work with `Media` Class, for example:

```csharp
//send photo with url
var media = Media.FromUrl(new Uri("https://someImage.com/"));
var message = await _client.Attachments.SendPhotoAsync(chatId, media);

//send with file_id (the media already uploaded on the servers)
var media = Media.FromId("2939234i92fskkdofs");
var message = await _client.Attachments.SendPhotoAsync(chatId, media);

//upload from the disk

var media = Media.FromDisk("/home/Photo/somepic.png");
var message = await _client.Attachments.SendPhotoAsync(chatId, media);
```

other methods perform the same way, they all accept a `Media`, and you fill it the same way.

you can also read my [tests](https://github.com/MrAliSalehi/Bale.Net/tree/master/Bale.Net.NUnit/InterfaceTests) to see
the methods in action.

### retry policy

this packages uses [Polly](https://github.com/App-vNext/Polly) to retry **any** failed API calls, you customize it's
behaviour like the following:

- set the delay between each attempt:

```csharp
_client.Delay = TimeSpan.FromSeconds(1); //can be anything
```

- set the maximum retry attempts (the default is 3):

  - to completely disable the retry policy you can set this property to zero. 
```csharp
_client.MaxRetryAttempts = 2;
```

- get notified when a retry is happening:
```csharp
_client.OnRetry = static arg =>
{
    Console.WriteLine(arg.AttemptNumber); //print the attempt counts
    return ValueTask.CompletedTask;
};
```
note that these settings should be done **before** any api calls, otherwise it wont have any effect.
[*sample test*](https://github.com/MrAliSalehi/Bale.Net/blob/6f6c5452bf76ddc00dfda1c36fb9ebf5168cc0d7/Bale.Net.NUnit/InterfaceTests/MessagesTest.cs#L119).

## TODO

- documentation for types
- abstractions for update handling (in progress)
- mock the API
- more unit tests
- [SendContact throws internal error](https://github.com/MrAliSalehi/Bale.Net/blob/6f6c5452bf76ddc00dfda1c36fb9ebf5168cc0d7/Bale.Net.NUnit/InterfaceTests/AttachmentsTest.cs#L157) when `reply_to_message_id` is used