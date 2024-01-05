using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;
using WebApiTest.Models.Dto;
using WebApiTest.Data;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
     [ApiController]
    public class testController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<testDto>> GetTests()
        {
            return Ok(testStore.testlist);
        }





        [HttpGet("{id:int}", Name = "GetTests")]
        //       [ProducesResponseType(200,Type=typeof(testDto))]
        //       [ProducesResponseType(404)]
        //       [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<testDto> GetTests(int id)
        {
            if (id == 0) 
            { 
                return BadRequest(); 
            }
            var just = testStore.testlist.FirstOrDefault(x => x.Id == id);
            if (just == null) 
            { 
                return NotFound(); 
            }
            return Ok(just);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<testDto> CreateTest([FromBody]testDto test)
        {
//            if (!ModelState.IsValid)
//              {
//                return BadRequest();
//            }
            if(testStore.testlist.FirstOrDefault(x=>x.Name.ToLower() == test.Name.ToLower())!=null)
            {
                ModelState.AddModelError("CustomError", "user is already exist");
                return BadRequest(ModelState);
            }
            if (test == null) 
            { 
                return BadRequest(test);
            }
            if (test.Id > 0) 
            { 
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            test.Id = testStore.testlist.Count+1;
            testStore.testlist.Add(test);
            return CreatedAtRoute("GetTests", new { id=test.Id },test);
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<testDto> EditTest(int id, testDto obj)
        {
            if (id == 0)
            { return BadRequest(id); }
            var person = testStore.testlist.FirstOrDefault(x => x.Id == id);
            if(person == null) 
            {
                return BadRequest();
            }
            person.Name = obj.Name;

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult <testDto> DeleteTest(int id)
        {
            int index = testStore.testlist.FindIndex(x=>x.Id == id);
            if (index == -1)
            {
                return NotFound(id);
               
            }
            testStore.testlist.RemoveAt(index);
            return NoContent();
        }
    }
}
