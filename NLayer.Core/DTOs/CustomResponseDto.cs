using System.Text.Json.Serialization;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>  // Generic olarak bir <T> datası alsın dinamik olarak
    {
        public T Data { get; set; }  // T Data yı aldım
        [JsonIgnore]
        public int StatusCode { get; set; } // Endpoint lere bir istek yaptığımızda mutlaka durum kodu almamız lazım. Bu bana response ın body sinde değilde sadece kodun içinde lazım o yüzden JsonIgnore diyeceğim.
                                            // Bu sayede StatusCode ları client lara dönmeyecek.Clientlar istek yaprığında direkt sonucu görecekler
        public List<String> Errors { get; set; }  // Hataları tutuyorum
        public static CustomResponseDto<T> Success(int statusCode, T data)   // CustomResponseDto<T> başarılı ve başarısız için ayrı ayrı metodlar üzerinden sonuç dönsün
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data, Errors = null };
        }
        public static CustomResponseDto<T> Success(int statusCode)   // Update işlmei olupta başarılı dönen ve data döndürülmeyen sonuçlar için
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)   // Update işlmei olupta başarılı dönen ve data döndürülmeyen sonuçlar için
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }
        public static CustomResponseDto<T> Fail(int statusCode, string errors)   // Bir tane hata içeren dönüşler için
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { errors } };
        }

    }
}
