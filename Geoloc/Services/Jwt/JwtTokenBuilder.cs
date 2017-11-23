using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Geoloc.Services.Jwt
{
    public sealed class JwtTokenBuilder
    {
        private SecurityKey _securityKey;
        private string _subject;
        private string _issuer;
        private string _audience;
        private readonly IList<Claim> _claims = new List<Claim>();
        private int _expiryInMinutes = 5;

        public static SecurityKey GetSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public JwtTokenBuilder AddSecurityKey(string key)
        {
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return this;
        }

        public JwtTokenBuilder AddSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public JwtTokenBuilder AddIssuer(string issuer)
        {
            _issuer = issuer;
            return this;
        }

        public JwtTokenBuilder AddAudience(string audience)
        {
            _audience = audience;
            return this;
        }

        public JwtTokenBuilder AddClaim(string type, string value)
        {
            _claims.Add(new Claim(type, value));
            return this;
        }

        public JwtTokenBuilder AddExpiry(int expiryInMinutes)
        {
            _expiryInMinutes = expiryInMinutes;
            return this;
        }

        public JwtSecurityToken Build()
        {
            EnsureArguments();

            _claims.Add(new Claim(JwtRegisteredClaimNames.Sub, _subject));
            _claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            return new JwtSecurityToken(
                _issuer,
                _audience,
                _claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_expiryInMinutes),
                new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256));
        }

        private void EnsureArguments()
        {
            if (_securityKey == null)
                throw new ArgumentNullException(nameof(_securityKey));

            if (string.IsNullOrEmpty(_subject))
                throw new ArgumentNullException(nameof(_subject));

            if (string.IsNullOrEmpty(_issuer))
                throw new ArgumentNullException(nameof(_issuer));

            if (string.IsNullOrEmpty(_audience))
                throw new ArgumentNullException(nameof(_audience));
        }
    }
}