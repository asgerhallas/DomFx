using DomFx.Layouters;
using Shouldly;
using Xunit;

namespace DomFx.Tests
{
    // No pun intended... testing the Unit class! :)
    public class UnitTests
    {
        [Fact]
        public void CanSumWithoutFractionLoss()
        {
            (2.cm() + 1.cm()).ShouldBe(3.cm());
        }
    }
}