using IOXFleetServicesAPI.Helpers;
using Newtonsoft.Json.Linq;

namespace IOXFleetServicesAPI.Tests
{
    public class AccountFundsTests
    {
        [Fact]
        public void HasSufficientFundsCheck_HasSufficientFunds()
        {
            //Function
            Validations validations = new Validations();

            //act
            bool actual = validations.HasSufficientFundsCheck(1000, 500);

            //assert
            Assert.True(actual, $"{actual} should be true");
        }

        [Fact]
        public void HasSufficientFundsCheck_HasNotSufficientFunds()
        {
            //Function
            Validations validations = new Validations();

            //act
            bool actual = validations.HasSufficientFundsCheck(500, 1000);

            //assert
            Assert.False(actual, $"{actual} should be false");
        }
    }
}