namespace RiotDotNET.Endpoints;
using System.Net;

/// <summary>
/// The response from the endpoint request.
/// </summary>
public class EndpointResponse<TResponse>
{
    internal EndpointResponse(HttpStatusCode? statusCode)
    {
        StatusCode = statusCode;
    }

    internal EndpointResponse(TResponse obj, HttpStatusCode? statusCode = HttpStatusCode.OK)
        : this(statusCode)
    {
        Object = obj;
    }

    internal EndpointResponse(Exception exception, HttpStatusCode? statusCode = null)
        : this(statusCode)
    {
        Exception = exception;
    }

    /// <summary>
    /// <see langword="true"/> if a success response was returned from the request.
    /// </summary>
    public bool Success => StatusCode == HttpStatusCode.OK && Exception == null;

    /// <summary>
    /// The response object.
    /// </summary>
    public TResponse? Object { get; }

    /// <summary>
    /// The response status code.
    /// </summary>
    public HttpStatusCode? StatusCode { get; }

    /// <summary>
    /// The reponse exception, if any.
    /// </summary>
    public Exception? Exception { get; }
}
