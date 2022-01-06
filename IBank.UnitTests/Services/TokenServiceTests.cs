using System;
using System.Security.Claims;
using IBank.Services.Token;
using IBank.UnitTests.Utils;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace IBank.UnitTests.Services
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _configStub;

        public TokenServiceTests()
        {
            _configStub = new();
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

            var service = new TokenService(_configStub.Object);

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

            var service = new TokenService(_configStub.Object);

            // Act 
            var result = service.GetIdFromToken(claimsPrincipalStub.Object);

            // Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public void GenerateToken_WithValidNameAndId_ReturnsToken()
        {
            // Arrange
            var configSectionSecretStub = new Mock<IConfigurationSection>();
            configSectionSecretStub.Setup(m => m.Value)
                .Returns(RandomGeneratorUtils.GenerateString(64));

            _configStub.Setup(config => config.GetSection("AppSettings:Jwt:Secret"))
                .Returns(configSectionSecretStub.Object);

            var configSectionExpirationStub = new Mock<IConfigurationSection>();
            configSectionExpirationStub.Setup(m => m.Value)
                .Returns(RandomGeneratorUtils.GenerateValidNumber(4));

            _configStub.Setup(config => config.GetSection("AppSettings:Jwt:ExpirationInSeconds"))
                .Returns(configSectionExpirationStub.Object);

            var service = new TokenService(_configStub.Object);

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
