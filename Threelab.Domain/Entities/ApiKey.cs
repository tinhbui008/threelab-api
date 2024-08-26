using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Threelab.Domain.Base;

namespace Threelab.Domain.Entities
{
    public class ApiKey : BaseEntity<int>
    {
        public string Key { get; set; }
    }
}