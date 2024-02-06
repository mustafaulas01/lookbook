using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto
{
    public class ConnectResponseModel
    {
                /// <summary>
        /// Gets or sets model type of response
        /// </summary>
        public int ModelType { get; set; }

        /// <summary>
        /// Gets or sets response status text
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets response http status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets session identifier
        /// </summary>
        public string SessionID { get; set; }
    }
}