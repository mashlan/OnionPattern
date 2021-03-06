﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using OnionPattern.Domain.DataTransferObjects.Game;
using OnionPattern.Domain.DataTransferObjects.Game.Input;
using OnionPattern.Domain.Repository;
using OnionPattern.Domain.Services.Requests.Game.Async;
using Serilog;

namespace OnionPattern.Service.Requests.Game.Async
{
    public class CreateGameRequestAsync : BaseServiceRequestAsync<Domain.Entities.Game>, ICreateGameRequestAsync
    {
        public CreateGameRequestAsync(IRepositoryAsync<Domain.Entities.Game> repository, IRepositoryAsyncAggregate repositoryAggregate) 
            : base(repository, repositoryAggregate) { }

        #region Implementation of ICreateGameRequestAsync

        public async Task<GameResponseDto> ExecuteAsync(CreateGameInputDto input)
        {
            var gameResponse = new GameResponseDto();
            try
            {
                Log.Information("Creating Game Entry for [{NewName}]...", input?.Name);
                var gameEntity = Mapper.Map<CreateGameInputDto, Domain.Entities.Game>(input);
                gameResponse = Mapper.Map(await Repository.CreateAsync(gameEntity), gameResponse);
                gameResponse.StatusCode = 200;
                Log.Information("Created Game Entry for [{NewName}] with Id: [{Id}]", gameResponse.Name, gameResponse.Id);
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Failed to Create Game: [{NewName}].", input?.Name);
                HandleErrors(gameResponse, exception);
            }
            return gameResponse;
        }

        #endregion
    }
}
