﻿public class ForbiddenError : ErrorException
{
    public ForbiddenError()
    {
    }

    public ForbiddenError(string? message) : base(message)
    {
    }
}