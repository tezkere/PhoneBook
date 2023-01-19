namespace ContactApi.Model.ContactInfo
{
    #region using
    using System.ComponentModel.DataAnnotations;
    
    #endregion

    public class CreateRequest
    {
        [Required]
        [EnumDataType(typeof(Shared.Enums.InfoTypes))]
        public int InfoType { get; set; }

        [Required]
        public string Info { get; set; }

        [Required]
        public Guid ContactId { get; set; }


    }
}
