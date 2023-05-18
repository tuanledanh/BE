using Newtonsoft.Json;
using System.Net;

namespace MSIA.WebFresher032023.ConnectToDB.Middleware
{
    /// <summary>
    /// Middleware xử lý lỗi và trả về phản hồi lỗi.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// Gọi middleware để xử lý lỗi và trả về phản hồi lỗi.
        /// </summary>
        /// <param name="context">HttpContext đang xử lý</param>
        /// <returns>1 đối tượng là thuộc lớp ErrorResponse chứa thông tin lỗi</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception ex)
            {
                // Hiện đang hiển thị lỗi 500 cho mọi lỗi
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType= "application/json";
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 1,
                    DevMsg = ex.Message.ToString(),
                    UserMsg = ex.Message.ToString(),
                    MoreInfo = ex.Message.ToString(),
                    TraceId = ex.Message.ToString()
                };
                //convert ErrorResponse thành chuỗi JSON và ghi vào phản hồi
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
}
