namespace Core.Dtos
{
    public record LockupDto:DtoBase<long>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool Active { get; set; }
    }
}
