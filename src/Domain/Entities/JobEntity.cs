using SharedKernel.Domain.Seedwork;

namespace Domain.Entities
{
    public class JobEntity : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Views { get; private set; }
        public DateTime Expiration_Date { get; private set; }
        public DateTime Create_Date { get; private set; }
        public DateTime Update_Date { get; private set; }


        public void SetDetails(string name, string description, DateTime expDate)
        {
            Expiration_Date = expDate;
            Name = name;
            Description = description;
            Create_Date = DateTime.UtcNow;
        }

        public void IncreaseViews()
        {
            Views++;
        }

        public void UpdateDateTime()
        {
            Update_Date = DateTime.UtcNow;   
        }
    }
}