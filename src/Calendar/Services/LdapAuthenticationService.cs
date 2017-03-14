using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novell.Directory.Ldap;
using Microsoft.AspNetCore.Hosting;


// Reference: https://nicolas.guelpa.me/blog/2017/02/15/dotnet-core-ldap-authentication.html
namespace Calendar.Services
{
    public class AppUser
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class LdapConfig
    {
        public string Url { get; set; }
        public int Port { get; set; }
        public string BindDn { get; set; }
        public string BindCredentials { get; set; }
        public string SearchBase { get; set; }
        public string SearchFilter { get; set; }
        public string AdminCn { get; set; }
    }

    class LdapAuthenticationService : IAuthenticationService
    {
        private const string MemberOfAttribute = "memberOf";
        private const string DisplayNameAttribute = "displayName";
        private const string SAMAccountNameAttribute = "sAMAccountName";

        private readonly LdapConfig _config;
        private readonly LdapConnection _connection;

        public LdapAuthenticationService(IOptions<LdapConfig> config)
        {
            _config = config.Value;
            _connection = new LdapConnection
            {
                SecureSocketLayer = true
            };
        }

        public AppUser Login(string username, string password)
        {

            try
            {
                //_connection.Connect(_config.Url, LdapConnection.DEFAULT_SSL_PORT);
                _connection.Connect(_config.Url, _config.Port);
                _connection.Bind(_config.BindDn, _config.BindCredentials);
                
            }
            catch (LdapException e)
            {
                throw new Exception("LDAP Server Connection failed." + e.LdapErrorMessage);
            }

            var searchFilter = string.Format(_config.SearchFilter, username);
            var result = _connection.Search(
                _config.SearchBase,
                LdapConnection.SCOPE_SUB,
                searchFilter,
                new[] { MemberOfAttribute, DisplayNameAttribute, SAMAccountNameAttribute },
                false
            );

            try
            {
                var user = result.next();
                if (user != null)
                {
                    _connection.Bind(user.DN, password);
                    if (_connection.Bound)
                    {
                        return new AppUser
                        {
                            DisplayName = user.getAttribute(DisplayNameAttribute).StringValue,
                            Username = user.getAttribute(SAMAccountNameAttribute).StringValue,
                            IsAdmin = user.getAttribute(MemberOfAttribute).StringValueArray.Contains(_config.AdminCn)
                        };
                    }
                }
            }
            catch
            {
                throw new Exception("Login failed.");
            }
            _connection.Disconnect();
            return null;
        }
    }
}
