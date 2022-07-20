using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Domain
{
    public abstract class EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }
        public bool IsDelete { get; private set; }
        public DateTime? CreationDate { get; private set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? DeletionDate { get; private set; }

        protected EntityBase()
        {
            CreationDate = DateTime.Now;
            IsDelete = false;
        }

        public virtual void Delete()
        {
            IsDelete = true;
            DeletionDate = DateTime.Now;
        }
    }
}