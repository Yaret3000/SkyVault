using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    /// <summary>
    /// Dto for CosmosDB credentials
    /// </summary>
    public class CosmosDbCredentialsDto
    {
        public string AccountEndPoint { get; set; }
        public string AccountKey { get; set; }
        public string DatabaseName { get; set; }
    }
}
