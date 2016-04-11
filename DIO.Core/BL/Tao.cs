using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIO.Core.Models;

namespace DIO.Core.BL
{
    public class Tao : Interfaces.ITao
    {
        public Tao()
        {
            DAL.Tao.connectionString = @"Data Source=(LocalDb)\PogiAko;Initial Catalog=dio;Persist Security Info=True;User ID=sa;Password=P@$$w0rd1;";
        }

        public Tao(string connectionString)
        {
            DAL.Tao.connectionString = connectionString;
        }

        public int Create(Models.Tao tao)
        {
            int identity = -1;
            object oIdentity = new object();
            DAL.Tao.Create(tao, out oIdentity);
            identity = Convert.ToInt32(oIdentity);
            return identity;
        }

        public bool Delete(Models.Tao tao)
        {
            return DAL.Tao.Delete(tao);
        }

        public List<Models.Tao> Read()
        {
            return DAL.Tao.GetAll();
        }

        public Models.Tao Read(int id)
        {
            return DAL.Tao.GetItemByID(id);
        }

        public bool Update(Models.Tao tao)
        {
            return DAL.Tao.Update(tao);
        }
    }
}
