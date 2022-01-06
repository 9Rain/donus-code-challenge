using System;
using System.Security.Claims;
using IBank.Services.Token;
using IBank.Settings;
using IBank.UnitTests.Utils;
using Moq;
using Xunit;

namespace IBank.UnitTests.Services
{
    public class TokenServiceTests
    {
        private readonly Mock<IJwtSettings> _jwtStub;

        public TokenServiceTests()
        {
            _jwtStub = new();
        }

        [Fact]
        public void GetIdFromToken_WithInvalidClaims_ThrowsNullReferenceException()
        {
            // Arrange
            var claimsPrincipalStub = new Mock<ClaimsPrincipal>();

            claimsPrincipalStub.Setup((m) => m.Claims)
                .Returns(
                    new[] { new Claim(
                        ClaimTypes.Name, RandomGeneratorUtils.GenerateValidNumber(3)
                        )
                    }
                );

            var service = new TokenService(_jwtStub.Object);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => service.GetIdFromToken(claimsPrincipalStub.Object));
        }

        [Fact]
        public void GetIdFromToken_WithValidClaims_ReturnsClaimValue()
        {
            // Arrange
            var claimsPrincipalStub = new Mock<ClaimsPrincipal>();
            claimsPrincipalStub.Setup((m) => m.Claims)
                .Returns(
                    new[] { new Claim(
                        ClaimTypes.NameIdentifier, RandomGeneratorUtils.GenerateValidNumber(3)
                        )
                    }
                );

            var service = new TokenService(_jwtStub.Object);

            // Act 
            var result = service.GetIdFromToken(claimsPrincipalStub.Object);

            // Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public void GenerateToken_WithValidNameAndId_ReturnsToken()
        {
            // Arrange
            _jwtStub.Setup(jwt => jwt.Secret)
                .Returns(RandomGeneratorUtils.GenerateString(64));

            _jwtStub.Setup(jwt => jwt.ExpirationInSeconds)
                .Returns(RandomGeneratorUtils.GenerateValidNumber(4));

            var service = new TokenService(_jwtStub.Object);

            // Act 
            var result = service.GenerateToken(
                RandomGeneratorUtils.GenerateValidNumber(3),
                RandomGeneratorUtils.GenerateString(20)
            );

            // Assert
            Assert.IsType<string>(result);
        }
    }
}
