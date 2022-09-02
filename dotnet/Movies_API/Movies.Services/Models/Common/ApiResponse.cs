using System.Collections.Generic;

namespace Movies.Services.Models.Common
{
    /// <summary>
    /// Represents the Api response model
    /// </summary>
    public class ApiResponse<T>
    {
        /// <summary>`
        /// 
        /// </summary>
        public ApiResponse()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public ApiResponse(T data)
        {
            Result = data;
        }

        /// <summary>
        /// Represents if the request was successfully
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Errors array
        /// </summary>
        public IList<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Messages array
        /// </summary>
        public IList<string> Messages { get; set; } = new List<string>();

        /// <summary>
        /// The payload of the Response
        /// </summary>
        public T Result { get; set; }
    }
}