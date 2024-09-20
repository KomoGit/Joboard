using SharedKernel.Domain.Seedwork;

namespace Domain.Entities
{
    public class JobEntity : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Views { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }


        public void SetDetails(string name, string description, DateTime expDate)
        {
            ExpirationDate = expDate;
            Name = name;
            Description = description;
            CreateDate = DateTime.UtcNow;
        }

        public void IncreaseViews()
        {
            Views++;
        }

        public void UpdateDateTime()
        {
            UpdateDate = DateTime.UtcNow;   
        }
    }
}