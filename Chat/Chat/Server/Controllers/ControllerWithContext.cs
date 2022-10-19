using Chat.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//controller base che implementa il context, gli altri context derivano da lui per averlo
namespace Chat.Server.Controllers
{
    [ApiController]
    public class ControllerWithContext : ControllerBase
    {
        protected readonly ChatContext Context;

        public ControllerWithContext(ChatContext context)
        {
            Context = context;
        }

    }
}