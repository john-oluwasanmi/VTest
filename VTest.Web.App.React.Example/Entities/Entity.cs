using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V.Test.Web.App.Entities.Interface;

namespace V.Test.Web.App.Entities
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as IEntity;

            if (item == null)
                return false;

            return this.Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
