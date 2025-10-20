using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vives.Services.Model
{
    public class FilteredPageServiceResult<TEntity, TFilter>:PagedServiceResult<TEntity>
    {
        public TFilter? Filter { get; set; }
    }
}
