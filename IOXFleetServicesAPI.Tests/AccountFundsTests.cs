using IOXFleetServicesAPI.Helpers;
using Newtonsoft.Json.Linq;

namespace IOXFleetServicesAPI.Tests
{
    public class AccountFundsTests
    {
        [Fact]
        public void HasSufficientFundsCheck_HasSufficientFunds()
        {
            //act
            bool actual = Validations.HasSufficientFundsCheck(1000, 500);

            //assert
            Assert.True(actual, $"{actual} should be true");
        }

        [Fact]
        public void HasSufficientFundsCheck_HasNotSufficientFunds()
        {
            //act
            bool actual = Validations.HasSufficientFundsCheck(500, 1000);

            //assert
            Assert.False(actual, $"{actual} should be false");
        }
    }
}