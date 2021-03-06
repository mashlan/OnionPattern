﻿using OnionPattern.Domain.DataTransferObjects.Game;

namespace OnionPattern.Domain.Services.Requests.Game
{
    public interface IGetGamesByGenreRequest
    {
        GameListResponseDto Execute(string genre);
    }
}