using System;
using System.ComponentModel.DataAnnotations;
using GuyHarwood.DieThrow.Domain.Core;
using Moq;
using Xunit;

namespace GuyHarwood.DieThrow.Domain.Tests
{
    public class DescribeCommandValidator
    {
        public class MockCommand : Command
        {
            [Required]
            public string Required { get; set; }
            public bool Invalid { get; set; }
        }

        [Fact]
        public void ItShouldThrowExceptionWhenCommandNotInValidState()
        {
            var decoratedHandlerMock = new Mock<IHandler<MockCommand,object>>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var sut = new CommandValidator<MockCommand, object>(decoratedHandlerMock.Object, serviceProviderMock.Object);

            Assert.Throws<ValidationException>(() => sut.Handle(new MockCommand()));
        }

        [Fact]
        public void ItShouldDelegateToDecoratedHandlerWhenCommandValid()
        {
            var decoratedHandlerMock = new Mock<IHandler<MockCommand, object>>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var sut = new CommandValidator<MockCommand, object>(decoratedHandlerMock.Object, serviceProviderMock.Object);

            var command = new MockCommand()
            {
                Required = "required"
            };
            sut.Handle(command);

            decoratedHandlerMock.Verify(d => d.Handle(command), Times.Once);
        }
    }
}