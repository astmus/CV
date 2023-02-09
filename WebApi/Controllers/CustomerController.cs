using Domain.Customers.Commands;
using Domain.Customers.Queries;
using Domain.Interfaces;

using Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : BaseController
	{
		[HttpPost]
		public async Task<IActionResult> Create(CreateCustomerCommand command)
		{
			var result = await Mediator.Send(command);
			if (result.Success)
				return Ok(result.Response);
			else
				return BadRequest(result);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return Ok(await Mediator.Send(new DeleteCustomerCommand(id)));
		}
		[HttpPut("[action]")]
		public async Task<IActionResult> Update(int id, UpdateCustomerCommand command)
		{
			if (id != command.CustomerId)
			{
				return BadRequest();
			}
			return Ok(await Mediator.Send(command));
		}
		[HttpGet("[action]")]
		public async Task<int> GetCustomersCount(string name = null, string сompanyName = null, string email = null, string phone = null)
		{
			return await Mediator.Send(new GetCustomersCountQuery(name, сompanyName, email, phone));
		}
		[HttpGet("[action]")]
		public async Task<IEnumerable<Customer>> GetCustomersPage(int page, int pageCount, string name = null, string companyName = null, string email = null, string phone = null,  string sortBy = null, int sortDesc = 0)
		{
			return await Mediator.Send(new GetCustomersPageQuery(name, companyName, email, phone, page, pageCount, sortBy, sortDesc));
		}
	}
}
