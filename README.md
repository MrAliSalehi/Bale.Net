[![NuGet stable version](https://badgen.net/nuget/v/Bale.Net)](https://www.nuget.org/packages/Bale.Net)

# Bale.Net

This is a Simple [Bale](https://bale.ai/) bot Api Wrapper for dotnet core.

# Install

you can get this package using [nuget](https://www.nuget.org/packages/Bale.Net/): 

`dotnet add package Bale.Net --version 1.0.0`

or

`<PackageReference Include="Bale.Net" Version="1.0.0" />`

or

`NuGet\Install-Package Bale.Net -Version 1.0.0`

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

you can also read my [tests](https://github.com/MrAliSalehi/Bale.Net/tree/master/Bale.Net.NUnit/InterfaceTests) to see the methods in action.
