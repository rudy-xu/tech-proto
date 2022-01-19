namespace WebApi.Dtos
{
    public class UserReadDto
    {
        public int ID { set; get; }     //Auto increment

        public string UserName { set; get; }

        // // [Required]
        // [Column("password")]
        // [JsonIgnore]   //???
        // public string Password { set; get; }
        public byte[] PwdHash { set; get; }

        public byte[] PwdSalt { set; get; }
    }
}