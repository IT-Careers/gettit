namespace Gettit.Service.Models
{
    public abstract class MetadataBaseServiceModel : BaseServiceModel
    {
        public GettitUserServiceModel CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public GettitUserServiceModel? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public GettitUserServiceModel? DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
