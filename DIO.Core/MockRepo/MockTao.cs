using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIO.Core.Models;

namespace DIO.Core.MockRepo
{
    public class MockTao : Interfaces.ITao
    {
        private List<Models.Tao> _context = new List<Tao>();
        private int _id = 3;

        public Exception ExceptionToThrow { get; set; }

        public MockTao()
        {
            ExceptionToThrow = null;
            _context.Add(new Tao { FirstName = "test1", LastName = "test1", MiddleName = "test1", ID = 1 });
            _context.Add(new Tao { FirstName = "test2", LastName = "test2", MiddleName = "test2", ID = 2 });
            _context.Add(new Tao { FirstName = "test3", LastName = "test3", MiddleName = "test3", ID = 3 });
        }

        public int Create(Tao tao)
        {
            if (ExceptionToThrow != null)
            {
                throw ExceptionToThrow;
            }
            _id++;
            tao.ID = _id;
            _context.Add(tao);
            return _id;
        }

        public bool Delete(Tao tao)
        {
            if (ExceptionToThrow != null)
            {
                throw ExceptionToThrow;
            }
            var taoToDelete = _context.FirstOrDefault(item => item.ID == tao.ID);
            if (taoToDelete != null)
            {
                _context.Remove(taoToDelete);
                return true;
            }
            return false;
        }

        public List<Tao> Read()
        {
            if (ExceptionToThrow != null)
            {
                throw ExceptionToThrow;
            }
            return _context.ToList();
        }

        public Tao Read(int id)
        {
            if (ExceptionToThrow != null)
            {
                throw ExceptionToThrow;
            }
            return _context.FirstOrDefault(tao => tao.ID == id);
        }

        public bool Update(Tao tao)
        {
            if (ExceptionToThrow != null)
            {
                throw ExceptionToThrow;
            }
            var taoToUpdate = _context.FirstOrDefault(item => item.ID == tao.ID);
            if (taoToUpdate != null)
            {
                taoToUpdate.FirstName = tao.FirstName;
                taoToUpdate.LastName = tao.LastName;
                taoToUpdate.MiddleName = tao.MiddleName;
                return true;
            }
            return false;
        }
    }
}
