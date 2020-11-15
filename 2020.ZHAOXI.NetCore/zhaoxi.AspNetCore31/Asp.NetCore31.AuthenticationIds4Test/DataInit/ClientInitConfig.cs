using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Asp.NetCore31.AuthenticationIds4Test.DataInit
{
    /// <summary>
    /// 客户端模式
    /// </summary>
    public class ClientInitConfig
    {
        /// <summary>
        /// 定义ApiResource   
        /// 这里的资源（Resources）指的就是管理的API
        /// </summary>
        /// <returns>多个ApiResource</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("UserApi", "用户获取API")
            };
        }

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1", "My API"),
                new ApiScope("UserApi", "用户获取API")
            };

        /// <summary>
        /// 定义验证条件的Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "STONE",//客户端惟一标识
                    ClientSecrets = new [] { new Secret("12345".Sha256()) },//客户端密码，进行了加密
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                   
                    //授权方式，客户端认证，只要ClientId+ClientSecrets
                    AllowedScopes = new [] { "UserApi" },//允许访问的资源

                    Claims=new List<ClientClaim>(){
                        new ClientClaim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                        new ClientClaim(IdentityModel.JwtClaimTypes.NickName,"Eleven"),
                        new ClientClaim("eMail","57265177@qq.com")
                    }
                }
            };

        public static IEnumerable<TestUser> Users()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "mail@qq.com",
                    Password = "password"
                }
            };
        }
    }


}
