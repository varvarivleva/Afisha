namespace AfishaApi.Contracts
{
    /// <summary>
    ///  Класс создания бронирования.
    ///  Строка идентификатора "T:AfishaApi.Contracts.BookingRequestDto".
    /// </summary> 
    public class EnterPageResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
