using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
		private IMediator mediator;
		protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
	}
}
