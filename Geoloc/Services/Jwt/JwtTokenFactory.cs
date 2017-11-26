using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Geoloc.Services.Jwt
{
    public sealed class JwtTokenFactory
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly SecurityKey _securityKey;
        private readonly int _expiryInMinutes;
        private readonly string _subject;
        private readonly IList<Claim> _claims = new List<Claim>();

        public static SecurityKey GetSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public JwtTokenFactory(string issuer, string audience, string key, int expiryInMinutes, string subject)
        {
            _issuer = issuer;
            _audience = audience;
            _securityKey = GetSecurityKey(key);
            _expiryInMinutes = expiryInMinutes;
            _subject = subject;
        }

        public JwtTokenFactory(IConfiguration config, string subject)
        {
            _issuer = config.GetSection("JwtTokens")["Issuer"];
            _audience = config.GetSection("JwtTokens")["Audience"];
            _securityKey = GetSecurityKey(config.GetSection("JwtTokens")["Key"]);
            _expiryInMinutes = int.Parse(config.GetSection("JwtTokens")["Expiry"]);
            _subject = subject;
        }

        public JwtTokenFactory AddClaim(Claim claim)
        {
            _claims.Add(claim);
            return this;
        }

        public JwtTokenFactory AddClaim(string type, string value)
        {
            _claims.Add(new Claim(type, value));
            return this;
        }

        public JwtSecurityToken Build()
        {
            return new JwtSecurityToken(
                _issuer,
                _audience,
                _claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_expiryInMinutes),
                new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256));
        }
    }
}
