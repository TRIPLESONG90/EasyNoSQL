using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNoSQL
{
    public class ModelBase
    {
        public string _id { get; set; }
        public ModelBase()
        {
            _id = Guid.NewGuid().ToString();
        }
    }
}
