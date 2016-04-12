using DIO.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIO.Core.Models
{
    [SQLTableName("tbl_Persons")]
    public class Tao
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }

        public Tao()
        {

        }

        //public override bool Equals(object obj)
        //{
        //    Models.Tao toCompareWith = obj as Models.Tao;
        //    if (toCompareWith == null)
        //    {
        //        return false;
        //    }
        //    return this.FirstName == toCompareWith.FirstName
        //        && this.ID == toCompareWith.ID
        //        && this.LastName == toCompareWith.LastName
        //        && this.MiddleName == toCompareWith.MiddleName;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}
    }
}
