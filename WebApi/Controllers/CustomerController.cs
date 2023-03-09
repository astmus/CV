using DataAccess.Response;

using Domain.Customers.Commands;
using Domain.Customers.Queries;
using Domain.Interfaces;

using Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Text.RegularExpressions;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : BaseController
	{
		[HttpPost]
		public async Task<IActionResult> Create(CreateCustomerCommand command)
		{
			var isMatch = Regex.IsMatch(command.Email, @"^([\w-\.\+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
			if (!isMatch)
			{
				return Ok(new Result(false, "wrong email"));
			}
			else
			{
				var result = await Mediator.Send(command);
				return Ok(result);
			}
			
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return Ok(await Mediator.Send(new DeleteCustomerCommand(id)));
		}
		[HttpPut("[action]")]
		public async Task<IActionResult> Update(int id, UpdateCustomerCommand command)
		{
			var isMatch = Regex.IsMatch(command.Email, @"^([\w-\.\+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
			if (!isMatch)
			{
				return Ok(new Result(false,"wrong email"));
			}
			return Ok(await Mediator.Send(command));
		}
		[HttpGet("[action]")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
		public async Task<IActionResult> GetCustomersCount(string name = null, string сompanyName = null, string email = null, string phone = null)
		{
			return Ok(await Mediator.Send(new GetCustomersCountQuery(name, сompanyName, email, phone)));
		}
		
		[HttpGet("[action]")]		
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Customer>))]
		public async Task<IActionResult> GetCustomersPageQuery([FromQuery] GetCustomersPageQuery query)
		{			
			return Ok(await Mediator.Send(query));
		}
	}
}
