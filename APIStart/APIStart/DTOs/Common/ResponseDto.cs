using System.Net;

namespace APIStart.DTOs.Common;

public record ResponseDto(HttpStatusCode StatusCode, string Message);