﻿using Bale.Net.Types;

namespace Bale.Net.Interfaces;

public interface IMessages
{
    ValueTask<Message> SendMessageAsync(long chatId, string message, ReplyMarkup? replyMarkup = null, long replayToMessageId = 0);
    
}