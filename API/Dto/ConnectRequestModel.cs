using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Dto
{
    public class ConnectRequestModel
    {
       [JsonIgnore]
        public string Url { get; set; }
        /// <summary>
        /// Gets model type of connection request
        /// </summary>
        public int ModelType { get; set; }

        /// <summary>
        /// Gets database name to connect
        /// </summary>
        public string DatabaseName { get; set; }//"DOGO_db"; //"DOGO_V3";

        /// <summary>
        /// Gets user group code
        /// </summary>
        public string UserGroupCode { get; set; } //"v3";

        /// <summary>
        /// Gets username credential
        /// </summary>
        public string UserName { get; set; } //= "ecom"; //"akinon";

        /// <summary>
        /// Gets password credential
        /// </summary>
        public string Password { get; set; } //= "ecom1"; //"Akinon1";
    }
}