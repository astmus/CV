using Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace DataAccess.Response
{
	internal record Result (bool Success = true, string Message = null) : IRequestResult;	

	internal record Result<TResponse>(TResponse Response = default, bool Success = true, string Message = null) : IRequestResult<TResponse>;
}
#nullable enable
