using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Models;

namespace Portfolio.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class QuestionsController : ControllerBase
  {
    private readonly QuestionContext _context;

    public QuestionsController(QuestionContext context)
    {
      _context = context;

      if (_context.Questions.Count() == 0)
      {
        _context.Questions.Add(new Question { Content = "Questions 1" });
        _context.SaveChanges();
      }
    }



    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
    // {
    //   return await _context.Questions.ToListAsync();
    // }

    [HttpGet()]
    public async Task<ActionResult<Question>> GetAnswer([FromQuery(Name = "question")] string question)
    {
      System.Diagnostics.Debug.WriteLine(question);
      long id = 1;
      return await _context.Questions.FindAsync(id);
    }

    // [HttpGet("{id}")]
    public async Task<ActionResult<Question>> GetQuestion(long id)
    {
      var question = await _context.Questions.FindAsync(id);

      if (question == null)
      {
        return NotFound();
      }

      return question;
    }

    [HttpPost]
    public async Task<ActionResult<Question>> PostQuestion(Question question)
    {
      _context.Questions.Add(question);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutQuestion(long id, Question question)
    {
      if (id != question.Id)
      {
        return BadRequest();
      }
      _context.Entry(question).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestion(long id)
    {
      var question = await _context.Questions.FindAsync(id);

      if (question == null)
      {
        return NotFound();
      }

      _context.Questions.Remove(question);
      await _context.SaveChangesAsync();

      return NoContent();
    }

  }
}
