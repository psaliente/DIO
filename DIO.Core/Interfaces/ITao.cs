using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIO.Core.Interfaces
{
    public interface ITao
    {
        List<Models.Tao> Read();
        Models.Tao Read(int id);
        int Create(Models.Tao tao);
        bool Update(Models.Tao tao);
        bool Delete(Models.Tao tao);
    }
}
