namespace AuthAPI.Models.Dto
{
    public class RegistrationRequestDto
    {
        public string? Email { get; set; }
        public string Name { get; set; } = default!;
        public string EmpCode { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? Role { get; set; }
        public string? CreatedBy { get; set; }     
        public string? UpdatedBy { get; set; } 

    }
}
