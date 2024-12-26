namespace AfishaApi.Contracts
{
    /// <summary>
    ///  ����� ������ ��������������� �������.
    ///  ������ �������������� "T:AfishaApi.Contracts.RegisterRequestDto".
    /// </summary> 
    public class RegisterRequestDto
    {
        /// <summary>
        /// ����, �������� ����� ������������.
        /// ������ �������������� "F:AfishaApi.Contracts.RegisterRequestDto.Username".
        /// </summary>  
        public string Username { get; set; }
        /// <summary>
        /// ����, �������� ������ ������������.
        /// ������ �������������� "F:AfishaApi.Contracts.RegisterRequestDto.Password".
        /// </summary>  
        public string Password { get; set; }
        /// <summary>
        /// ����, �������� ����������� ����� ������������.
        /// ������ �������������� "F:AfishaApi.Contracts.RegisterRequestDto.Email".
        /// </summary>  
        public string Email { get; set; }
    }
}
