using WebApiTest.Models.Dto;

namespace WebApiTest.Data
{
    public class testStore
    {
        public static List<testDto> testlist =new List<testDto> {
                new testDto { Id=1 ,Name="babi" ,Place="Thannoor"},
                new testDto { Id=2 ,Name="shahal",Place="Parappanangadi" },
                new testDto { Id = 3, Name = "rishwal",Place="Farook" }
            };
    }
}
