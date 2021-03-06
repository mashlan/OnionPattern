﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using OnionPattern.Domain.DataTransferObjects.Platform;
using OnionPattern.Domain.DataTransferObjects.Platform.Input;
using OnionPattern.Domain.Repository;
using OnionPattern.Domain.Services.Requests.Platform.Async;
using Serilog;

namespace OnionPattern.Service.Requests.Platform.Async
{
    public class UpdatePlatformNameRequestAsync : BaseServiceRequestAsync<Domain.Entities.Platform>, IUpdatePlatformNameRequestAsync
    {
        /// <inheritdoc />
        /// <summary>
        ///     Request to update a Platform's name asynchronously.
        /// </summary>
        /// <exception cref="T:System.ArgumentNullException">Condition.</exception>
        public UpdatePlatformNameRequestAsync(IRepositoryAsync<Domain.Entities.Platform> repository, IRepositoryAsyncAggregate repositoryAggregate) 
            : base(repository, repositoryAggregate) { }

        #region Implementation of IUpdatePlatformNameRequestAsync

        /// <summary>
        /// Executes the request asynchronously.
        /// </summary>
        /// <param name="input">Inputs required to update the Platform.</param>
        /// <returns></returns>
        public async Task<PlatformResponseDto> ExecuteAsync(UpdatePlatformNameInputDto input)
        {
            var platformResponse = new PlatformResponseDto();
            try
            {
                CheckInputValidity(input);
                Log.Information("Updating Plattform with Id: [{Id}] with new Name: [{NewName}]...", input?.Id, input?.NewName);

                var platformToUpdate = await Repository.SingleOrDefaultAsync(p => p.Id == input.Id);
                if (platformToUpdate == null)
                {
                    var exception = new Exception($"Failed to find Platform with Id:[{input.Id}]");
                    Log.Error(exception, EXCEPTION_MESSAGE_TEMPLATE, exception.Message);
                    HandleErrors(platformResponse, exception, 404);
                }
                else
                {
                    var previousName = platformToUpdate.Name;
                    platformToUpdate.Name = input.NewName;
                    var updatedPlatform = await Repository.UpdateAsync(platformToUpdate);

                    platformResponse = Mapper.Map(updatedPlatform, platformResponse);
                    platformResponse.StatusCode = 200;

                    Log.Information("Updated Platform Name [{PreviousName}] to [{Name}].", previousName, platformResponse.Name);
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Failed to update Platform with Id: [{Id}].", input?.Id);
                HandleErrors(platformResponse, exception);
            }
            return platformResponse;
        }

        #endregion
        private void CheckInputValidity(UpdatePlatformNameInputDto input)
        {
            if (input == null) { throw new ArgumentNullException(nameof(input)); }
            if (input.Id <= 0) { throw new ArgumentException($"Input {nameof(input.Id)} must be 1 or greater."); }
            if (string.IsNullOrWhiteSpace(input.NewName)) { throw new ArgumentException($"Input {nameof(input.NewName)} cannot be empty."); }
        }
    }
}
