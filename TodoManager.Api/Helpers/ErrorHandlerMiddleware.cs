using TodoManager.Common.ErrorModel;
using TodoManager.Common.Exceptions;

namespace TodoManager.Api.Helpers;

public class ErrorHandlerMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ErrorHandlerMiddleware> _logger;

	public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception error)
		{
			context.Response.StatusCode = error switch
			{
				NotFoundException => StatusCodes.Status404NotFound,
				_ => StatusCodes.Status500InternalServerError
			};

			_logger.LogError("Something went wrong: {error}", error);

			await context.Response.WriteAsJsonAsync(new ErrorDetails(context.Response.StatusCode, error?.Message));
		}
	}
}
