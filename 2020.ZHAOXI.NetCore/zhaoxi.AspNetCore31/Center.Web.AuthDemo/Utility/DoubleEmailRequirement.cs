﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Center.Web.AuthDemo.Utility
{
    public class DoubleEmailRequirement : IAuthorizationRequirement
    {
    }

    public class QQMailHandler : AuthorizationHandler<DoubleEmailRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DoubleEmailRequirement requirement)
        {
            if(context.User != null && context.User.HasClaim(c=> c.Type == ClaimTypes.Email))
            {
                var email = context.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;
                if(email.EndsWith("@qq.com", StringComparison.OrdinalIgnoreCase))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //context.Fail();//不设置失败
                }
            }
            return Task.CompletedTask;
        }
    }

    public class ZhaoxiMailHandler : AuthorizationHandler<DoubleEmailRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DoubleEmailRequirement requirement)
        {
            if (context.User != null && context.User.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                var email = context.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;
                if (email.EndsWith("@ZhaoxiEdu.Net", StringComparison.OrdinalIgnoreCase))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}
