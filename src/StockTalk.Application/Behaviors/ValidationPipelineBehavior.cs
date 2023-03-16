using System.Linq.Expressions;
using Ardalis.Result;
using FluentValidation;
using MediatR;


namespace StockTalk.Application.Behaviors;

public class ValidationPipelineBehavior<TMessage,TResponse> 
    : IPipelineBehavior<TMessage, TResponse>
    where TMessage : notnull
{
    private readonly Type _tResponseType = typeof(TResponse);
    private readonly Type _tMessageType = typeof(TMessage);
    private readonly IEnumerable<IValidator<TMessage>> _validators;

    public ValidationPipelineBehavior(
        IEnumerable<IValidator<TMessage>> validators)
    {
        this._validators = validators;
    }

    public async Task<TResponse> Handle(TMessage request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_tResponseType.IsAssignableTo(typeof(IResult)))
            return await ErrorReturn(new []{"The IRequest don't use the Result pattern."});
        
        if (!_validators.Any() && _tMessageType.IsAssignableTo(typeof(IRequest)))
            return await ErrorReturn(new []{ $"The {typeof(TMessage).Name} has no validator." });

        ValidationContext<object> validationContext = new(request);

        var validatorsResult = await Task.WhenAll(
            _validators.Select(x
                => x.ValidateAsync(validationContext, cancellationToken)));

        var failureMessages = validatorsResult
            .SelectMany(e => e.Errors)
            .Where(e => e is not null)
            .Select(x => $"{x.PropertyName}: {x.ErrorMessage}")
            .ToArray();

        if (failureMessages.Any())
            return await ErrorReturn(failureMessages);
        
        return await next();
    }

    private static TResponse BuildResultInstance(IEnumerable<string> errors)
    {
        var value = Expression.Constant(errors);
        var errorMethod = typeof(TResponse).GetMethod("Error", new[] {typeof(string[])});
        var calledMethod = Expression.Call(errorMethod ?? throw new InvalidOperationException(), value);
        var expression = Expression.Lambda<Func<TResponse>>(calledMethod);
        var resultError = expression.Compile();

        return resultError();
    }

    private ValueTask<TResponse> ErrorReturn(IEnumerable<string> errorsMessages)
        => ValueTask.FromResult(BuildResultInstance(errorsMessages));
}