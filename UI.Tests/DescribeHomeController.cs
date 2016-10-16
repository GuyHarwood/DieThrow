using System.Web.Mvc;
using GuyHarwood.DieThrow.Domain;
using GuyHarwood.DieThrow.Domain.Core;
using Moq;
using UI.Controllers;
using UI.Models;
using Xunit;

namespace UI.Tests
{
    public class DescribeHomeController
    {
        private readonly HomeController _sut;
        private readonly Mock<IHandler<ThrowDice, HighestStreak>> _throwDiceHandlerMock;

        public DescribeHomeController()
        {
            _throwDiceHandlerMock = new Mock<IHandler<ThrowDice, HighestStreak>>();
            _sut = new HomeController(_throwDiceHandlerMock.Object);
        }

        [Fact]
        public void ItRendersCorrectView()
        {
            var result = _sut.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        public void ItShouldFormatLargestStreakTextCorrectly()
        {
            var highestStreak = new HighestStreak()
            {
                Value = 4
            };
            _throwDiceHandlerMock.Setup(x => x.Handle(It.IsAny<ThrowDice>()))
                .Returns(highestStreak);

            var result = _sut.Index(new HomeIndexModel());
            Assert.NotNull(result);

            var model = (HomeIndexModel) result.ViewData.Model;
            var expected = string.Format("The largest winning streak (sixes in a row) was {0}", highestStreak);
            Assert.Equal(expected, model.LargestStreak);
        }
    }
}