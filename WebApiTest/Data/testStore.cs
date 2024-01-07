using WebApiTest.Models.Dto;

namespace WebApiTest.Data
{
    public interface IStore
    {
        List<testDto> data();
    }


    public class testStore: IStore
    {
        public List<testDto> testlist = new List<testDto> {
                new testDto { Id=1 ,Name="babi" ,Place="Thannoor"},
                new testDto { Id=2 ,Name="shahal",Place="Parappanangadi" },
                new testDto { Id = 3, Name = "rishwal",Place="Farook" }
            };
        public List<testDto> data() 
        {            
            return testlist;
        }
    }


    public class Managecupling
    {
        IStore _obj = null;
        public Managecupling(IStore WhichObjRun)
        {
            _obj = WhichObjRun;
        }

        public List<testDto> dataProvider()
        {
            return _obj.data();
        }
    }
}
