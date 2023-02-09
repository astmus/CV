using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IRequestResult
	{
		bool Success { get; }
		string Message { get; }
	}

	public interface IRequestResult<TResponse> : IRequestResult
	{
		TResponse Response { get; }
	}
}
