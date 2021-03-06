﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnionPattern.Domain.Entities;
using OnionPattern.Domain.Services.Requests.Game.Async;
using OnionPattern.Service.Requests.Game.Async;

namespace OnionPattern.Service.Tests.Requests.Games.Async
{
    public class GetGamesByGenreRequestAsyncTests
    {
        [TestClass]
        public class ConstructorTests : TestBaseAsync<Game>
        {
            [TestInitialize]
            public void TestInitalize()
            {
                InitializeFakes();
            }

            [TestCleanup]
            public void TestCleanup()
            {
                ClearFakes();
            }

            [TestMethod]
            public void Inheritence()
            {
                var request = new GetGamesByGenreRequestAsync(FakeRepositoryAsync, FakeRepositoryAsyncAggregate);

                request.Should().NotBeNull();
                request.Should().BeAssignableTo<BaseServiceRequestAsync<Game>>();
                request.Should().BeAssignableTo<IGetGamesByGenreRequestAsync>();
                request.Should().BeOfType<GetGamesByGenreRequestAsync>();
            }
        }
    }
}
