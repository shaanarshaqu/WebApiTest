﻿using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;
using WebApiTest.Models.Dto;
using WebApiTest.Data;
using Microsoft.AspNetCore.JsonPatch;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        private readonly IStore _istore;
        public testController (IStore obj)
        {
            _istore= obj;
        }

        //Managecupling loosecuplledstore = new Managecupling(new testStore());

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<testDto>> GetTests()
        {
            return Ok(_istore.data());
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
            var just = _istore.data().FirstOrDefault(x => x.Id == id);
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
        public ActionResult<testDto> CreateTest([FromBody] testDto test)
        {
            //            if (!ModelState.IsValid)
            //              {
            //                return BadRequest();
            //            }
            var list = _istore.data();

            if (list.FirstOrDefault(x => x.Name.ToLower() == test.Name.ToLower()) != null)
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

            test.Id = list[list.Count-1].Id + 1;
            list.Add(test);
            Console.WriteLine($"List count after adding: {list?.Count}");
            return CreatedAtRoute("GetTests", new { id = test.Id }, test);
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public ActionResult<testDto> EditTest(int id, [FromBody] testDto obj)
        {
            if (id == 0)
            { return BadRequest(id); }
            var person = _istore.data().FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return BadRequest();
            }
            person.Name = obj.Name;
            person.Place = obj.Place;

            return NoContent();
        }


        [HttpDelete("{id:int}", Name = "DeleteTest")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteTest(int id)
        {
            var list = _istore.data();
            int index = list.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                return NotFound(id);

            }
            list.RemoveAt(index);
            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "PatchTest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult PatchTest(int id, JsonPatchDocument<testDto> patchDto)
        {
            var list= _istore.data();
            if (patchDto == null || id == 0)
            {
                return BadRequest(id);
            }
            var person = list.FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return BadRequest(id);
            }
            patchDto.ApplyTo(person, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
