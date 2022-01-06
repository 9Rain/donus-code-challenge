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
        private readonly Mock<IConfiguration> _config;

        public TokenServiceTests()
        {
            _config = new();
        }

        [Fact]
        public void GetIdFromToken_WithInvalidClaims_ReturnsNull()
        {
            // Arrange
            var mock = new Mock<ClaimsPrincipal>();
            mock.Setup((m) => m.Claims)
                .Returns(
                    new[] { new Claim(
                        ClaimTypes.Name, RandomGeneratorUtils.GenerateValidNumber(3)
                        )
                    }
                );

            var service = new TokenService(_config.Object);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => service.GetIdFromToken(mock.Object));
        }

        [Fact]
        public void GetIdFromToken_WithValidClaims_ReturnsClaimValue()
        {
            // Arrange
            var claimsPrincipalMock = new Mock<ClaimsPrincipal>();
            claimsPrincipalMock.Setup((m) => m.Claims)
                .Returns(
                    new[] { new Claim(
                        ClaimTypes.NameIdentifier, RandomGeneratorUtils.GenerateValidNumber(3)
                        )
                    }
                );

            var service = new TokenService(_config.Object);

            // Act 
            var result = service.GetIdFromToken(claimsPrincipalMock.Object);

            // Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public void GenerateToken_WithValidNameAndId_ReturnsToken()
        {
            // Arrange
            var configSectionSecretMock = new Mock<IConfigurationSection>();
            configSectionSecretMock.Setup(m => m.Value)
                .Returns(RandomGeneratorUtils.GenerateString(64));

            _config.Setup(config => config.GetSection("AppSettings:Jwt:Secret"))
                .Returns(configSectionSecretMock.Object);

            var configSectionExpirationMock = new Mock<IConfigurationSection>();
            configSectionExpirationMock.Setup(m => m.Value)
                .Returns(RandomGeneratorUtils.GenerateValidNumber(4));

            _config.Setup(config => config.GetSection("AppSettings:Jwt:ExpirationInSeconds"))
                .Returns(configSectionExpirationMock.Object);

            var service = new TokenService(_config.Object);

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
