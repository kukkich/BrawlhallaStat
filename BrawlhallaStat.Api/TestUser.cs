﻿using BrawlhallaStat.Domain.Identity;

namespace BrawlhallaStat.Api;

public static class TestUser
{
    public static User Instance => new()
    {
        //TODO remove
        Id = "3a54046f-a17b-4757-87e9-3b3a7847d8e3",
        Login = "Nasral V Szhopu"
    };
}