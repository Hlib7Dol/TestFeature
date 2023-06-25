using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FeatureConsoleApplication
{
    /// <summary>
    /// Connection string tool
    /// </summary>
    internal class ConnectionStringTool
    {
        #region Private fields

        private readonly char[] PropertySeparator = { ';' };
        private readonly char [] KeyValueSeparator = { '=' };

        private const string EndpointProperty = "endpoint";
        private const string AccessKeyProperty = "accesskey";

        private readonly JwtSecurityTokenHandler _jwtTokenHandler;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for <see cref="ConnectionStringTool"/>
        /// </summary>
        public ConnectionStringTool() 
        {
            _jwtTokenHandler = new();
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Parses connection string to url and access key
        /// </summary>
        /// <param name="connectionString">Connectin string</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if there are duplicate properties in connection string</exception>
        internal (string, string) ParseConnectionString(string connectionString)
        {
            var properties = connectionString.Split(PropertySeparator, StringSplitOptions.RemoveEmptyEntries);
            if (properties.Length > 1)
            {
                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var property in properties)
                {
                    var kvp = property.Split(KeyValueSeparator, 2);
                    if (kvp.Length != 2) continue;

                    var key = kvp[0].Trim();
                    if (dict.ContainsKey(key))
                    {
                        throw new ArgumentException($"Duplicate properties found in connection string: {key}.");
                    }

                    dict.Add(key, kvp[1].Trim());
                }

                if (dict.ContainsKey(EndpointProperty) && dict.ContainsKey(AccessKeyProperty))
                {
                    return (dict[EndpointProperty].TrimEnd('/'), dict[AccessKeyProperty]);
                }
            }

            throw new ArgumentException($"Connection string missing required properties {EndpointProperty} and {AccessKeyProperty}.");
        }

        /// <summary>
        /// Gets hub client url
        /// </summary>
        /// <param name="endpoint">Azure server url</param>
        /// <param name="hubName">Hub name</param>
        /// <returns>Hub client Url</returns>
        internal string GetClientUrl(string endpoint, string hubName) => $"{endpoint}/client/?hub={hubName}";

        /// <summary>
        /// Generated access token for connection to Azure SignalR service
        /// </summary>
        /// <param name="url">Service url</param>
        /// <param name="accessKey">Access key</param>
        /// <returns>Access token</returns>
        internal string GenerateAccessToken(string url, string accessKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = this._jwtTokenHandler.CreateJwtSecurityToken(
                audience: url,
                signingCredentials: credentials);

            return this._jwtTokenHandler.WriteToken(token);
        }

        #endregion
    }
}
