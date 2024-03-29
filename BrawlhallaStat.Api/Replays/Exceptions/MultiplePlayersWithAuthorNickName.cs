﻿namespace BrawlhallaStat.Api.Replays.Exceptions;

public class MultiplePlayersWithAuthorNickName : ReplayUploadException
{
    public string NickName { get; }
    public override string Message => $"There is no player with the nickname {NickName} in the game";

    public MultiplePlayersWithAuthorNickName(string nickName)
    {
        NickName = nickName;
    }
}