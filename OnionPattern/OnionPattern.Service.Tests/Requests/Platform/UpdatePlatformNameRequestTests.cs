﻿using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnionPattern.Domain.DataTransferObjects.Platform.Input;
using OnionPattern.Domain.Services.Requests.Platform;
using OnionPattern.Service.Requests.Platform;

namespace OnionPattern.Service.Tests.Requests.Platform
{
    public class UpdatePlatformNameRequestTests
    {
        [TestClass]
        public class ConstructorTests : TestBase<Domain.Entities.Platform>
        {
            [TestInitialize]
            public void TestInitialize()
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
                var request = new UpdatePlatformNameRequest(FakeRepository, FakeRepositoryAggregate);

                request.Should().NotBeNull();
                request.Should().BeAssignableTo<BaseServiceRequest<Domain.Entities.Platform>>();
                request.Should().BeAssignableTo<IUpdatePlatformNameRequest>();
                request.Should().BeOfType<UpdatePlatformNameRequest>();
            }
        }

        [TestClass]
        public class MethodsTests : TestBase<Domain.Entities.Platform>
        {
            private IUpdatePlatformNameRequest request;

            [TestInitialize]
            public void TestInitialize()
            {
                InitializeFakes();
                request = new UpdatePlatformNameRequest(FakeRepository, FakeRepositoryAggregate);
            }

            [TestCleanup]
            public void TestCleanup()
            {
                ClearFakes();
            }

            [TestMethod]
            public void InputIsNull()
            {
                var response = request.Execute(null);

                response.Should().NotBeNull();
                response.ErrorSummary.Should().NotBeNullOrWhiteSpace();
                response.ErrorSummary.Should().BeEquivalentTo($"Value cannot be null.{Environment.NewLine}Parameter name: input");
                response.StatusCode.HasValue.Should().BeTrue();
                response.StatusCode.ShouldBeEquivalentTo(500);
            }

            [DataTestMethod]
            [DataRow(-1)]
            [DataRow(0)]
            public void InvalidInputIdNotValid(int Id)
            {
                var invalidInput = new UpdatePlatformNameInputDto { Id = Id, NewName = "Something" };
                var response = request.Execute(invalidInput);

                response.Should().NotBeNull();
                response.ErrorSummary.Should().NotBeNullOrWhiteSpace();
                response.ErrorSummary.Should().BeEquivalentTo($"Input {nameof(Id)} must be 1 or greater.");
                response.StatusCode.HasValue.Should().BeTrue();
                response.StatusCode.ShouldBeEquivalentTo(500);
            }

            [DataTestMethod]
            [DataRow(default(string))]
            [DataRow("")]
            [DataRow("      ")]
            public void InvalidInputNameIsEmpty(string name)
            {
                var invalidInput = new UpdatePlatformNameInputDto { Id = 666, NewName = name };
               var response = request.Execute(invalidInput);

                response.Should().NotBeNull();
                response.ErrorSummary.Should().NotBeNullOrWhiteSpace();
                response.ErrorSummary.Should().BeEquivalentTo("Input NewName cannot be empty.");
                response.StatusCode.HasValue.Should().BeTrue();
                response.StatusCode.ShouldBeEquivalentTo(500);
            }
        }
    }
}
